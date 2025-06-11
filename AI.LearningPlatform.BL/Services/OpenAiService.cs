using Microsoft.Extensions.Logging;
using OpenAI;
using OpenAI.Extensions;
using OpenAI.Managers;
using OpenAI.ObjectModels;
using OpenAI.ObjectModels.RequestModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace AI.LearningPlatform.BL.Services
{
    public class OpenAiService
    {
        private readonly OpenAIService _openAiService;
        private readonly ILogger<OpenAiService> _logger;

        public OpenAiService(string apiKey, ILogger<OpenAiService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                _logger.LogError("OpenAI API key is null or empty.");
                throw new ArgumentNullException(nameof(apiKey), "OpenAI API key cannot be null or empty.");
            }

            // אתחול OpenAIService – שימוש בצורה הפשוטה ביותר
            _openAiService = new OpenAIService(new OpenAiOptions
            {
                ApiKey = apiKey
            });

        }

        public async Task<string> GenerateLessonContentAsync(string category, string subCategory, string prompt)
        {
            if (string.IsNullOrWhiteSpace(category) && string.IsNullOrWhiteSpace(subCategory) && string.IsNullOrWhiteSpace(prompt))
            {
                _logger.LogWarning("No input provided for lesson generation.");
                return "אני זקוק/ה לנושא (קטגוריה/תת-קטגוריה) או הנחיה מפורטת כדי ליצור שיעור.";
            }

            string fullPrompt = $"צור שיעור מפורט עבור פלטפורמת למידה מקוונת.\n" +
                                $"נושא: {category}.\n" +
                                $"תת-נושא: {subCategory}.\n" +
                                $"הנחיה: {prompt}.\n" +
                                $"השיעור צריך לכלול מבוא, גוף, וסיכום, ולהיות מנוסח בשפה ברורה וידידותית.";

            try
            {
                var response = await _openAiService.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
                {
                    Model = Models.Gpt_3_5_Turbo,
                    Temperature = 0.7f,
                    MaxTokens = 1000,
                    Messages = new List<ChatMessage>
                    {
                        ChatMessage.FromSystem("אתה עוזר וירטואלי מומחה ביצירת תכני למידה."),
                        ChatMessage.FromUser(fullPrompt)
                    }
                });

                if (response.Successful && response.Choices?.Count > 0)
                {
                    _logger.LogInformation("Lesson generated successfully.");
                    return response.Choices[0].Message.Content.Trim();
                }

                if (response.Error != null)
                {
                    _logger.LogError($"OpenAI Error: {response.Error.Code} - {response.Error.Message}");
                    return $"שגיאה מ-OpenAI: {response.Error.Message}";
                }

                _logger.LogWarning("OpenAI returned no content.");
                return "התקבלה תגובה מ-OpenAI אך ללא תוכן.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during OpenAI request.");
                return $"שגיאה בלתי צפויה: {ex.Message}";
            }
        }
    }
}
