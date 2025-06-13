using AI.LearningPlatform.BL.NewFolder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AI.LearningPlatform.BL.Configuration;



namespace AI.LearningPlatform.BL.Services
{
    public class OpenAiService : IAiService
    {
        private readonly OpenAIService _openAiService;
        private readonly ILogger<OpenAiService> _logger;

        public OpenAiService(IOptions<OpenAISettings> options, ILogger<OpenAiService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
            if (string.IsNullOrWhiteSpace(settings.ApiKey))
            {
                _logger.LogError("🔑 מפתח API של OpenAI חסר או ריק.");
                throw new ArgumentNullException(nameof(settings.ApiKey), "OpenAI API key cannot be null or empty.");
            }

            _openAiService = new OpenAIService(new OpenAiOptions
            {
                ApiKey = settings.ApiKey
            });
        }

        public async Task<string> GenerateLessonContentAsync(string category, string subCategory, string prompt)
        {
            if (string.IsNullOrWhiteSpace(category) && string.IsNullOrWhiteSpace(subCategory) && string.IsNullOrWhiteSpace(prompt))
            {
                _logger.LogWarning("⚠️ לא התקבלו נתונים ליצירת שיעור.");
                return "אני זקוק/ה לנושא (קטגוריה/תת-קטגוריה) או להנחיה כלשהי כדי ליצור שיעור.";
            }

            string fullPrompt = $"צור שיעור מפורט עבור פלטפורמת למידה.\n" +
                                $"נושא: {category}\n" +
                                $"תת-נושא: {subCategory}\n" +
                                $"הנחיה: {prompt}\n\n" +
                                $"השיעור צריך לכלול:\n" +
                                $"- מבוא\n- גוף\n- סיכום\n" +
                                $"והכול בשפה ברורה וידידותית.";

            return await GetCompletionAsync(fullPrompt);
        }

        public async Task<string> GetCompletionAsync(string prompt)
        {
            try
            {
                var response = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Model = Models.Gpt_3_5_Turbo,
                    Temperature = 0.7f,
                    MaxTokens = 1000,
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("אתה עוזר וירטואלי מומחה ביצירת תכנים."),
                        ChatMessage.FromUser(prompt)
                    }
                });

                if (response.Successful && response.Choices?.Count > 0)
                {
                    return response.Choices[0].Message.Content.Trim();
                }

                if (response.Error != null)
                {
                    _logger.LogError($"❌ שגיאת OpenAI: {response.Error.Code} - {response.Error.Message}");
                    return $"שגיאה: {response.Error.Message}";
                }

                _logger.LogWarning("⚠️ לא התקבלה תגובה מהמודל.");
                return "לא התקבלה תגובה מה-AI.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "🛑 חריגה בעת ניסיון לשלוח שאילתה ל-AI");
                return $"שגיאה פנימית: {ex.Message}";
            }
        }
    }
}
