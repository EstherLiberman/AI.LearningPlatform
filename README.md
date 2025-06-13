
 🟩 README (English) – Server

```markdown
AI Learning Platform - Backend

This is the server side for the AI-based learning platform.

📁 Project Structure

- **AI.LearningPlatform.BL** – Business logic layer (services, interfaces, configuration)
- **AI.LearningPlatform.DAL** – Data access layer (MongoDB models, repositories, seed)
- **AI.LearningPlatform.Server** – ASP.NET Core Web API with REST controllers

🛠 Technologies Used

- ASP.NET Core Web API
- MongoDB
- Dependency Injection
- RESTful API
- OpenAI Integration
- Swagger UI
- dotenv

 📝 Assumptions

- MongoDB is used as the database
- OpenAI is integrated through a dedicated service
- A Seeder layer is provided for initial data

🖥️ How to Run Locally

1. Make sure .NET 8 and MongoDB are installed
2. Create a `.env` file in the root with the following:
    ```env
    OPENAI_API_KEY=your_openai_key
    MONGO_CONNECTION_STRING=mongodb://localhost:27017
    MONGO_DB_NAME=AiLearningDb
    ```
3. Navigate to `AI.LearningPlatform.Server` folder and run:
    ```bash
    dotnet run
    
    ```4. Visit Swagger UI at:
    [https://localhost:7095/swagger/index.html](https://localhost:7095/swagger/index.html)

