# 🤖 AI Chatbot — Powered by Claude API

A Windows Forms AI Chatbot application built with **.NET 6** and the **Anthropic Claude API**.
This project was developed as part of an AI-Based Application Development assignment.

---

## 📸 Screenshots

> **

---

## ✨ Features

- 💬 **Multi-turn conversation** — remembers chat history within a session
- ⚡ **Real-time AI responses** via Anthropic Claude API
- 🎨 **Clean Windows Forms UI** with color-coded messages
- ⌨️ **Enter key to send** — just like a real chat app
- 🗑️ **Clear chat** button to reset the conversation
- 🔄 **Status bar** showing when the AI is thinking
- ❌ **Error handling** for network and API failures

---

## 🛠️ Technologies Used

| Technology        | Purpose                          |
|-------------------|----------------------------------|
| .NET 6            | Application framework            |
| Windows Forms     | Desktop UI                       |
| C#                | Programming language             |
| Anthropic Claude API | AI/LLM backend                |
| HttpClient        | REST API communication           |
| System.Text.Json  | JSON serialization               |
| Docker            | Containerization                 |

---

## 🚀 Setup Instructions

### Prerequisites
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) installed
- A free Anthropic API key from [console.anthropic.com](https://console.anthropic.com)

### Step 1 — Clone the Repository
```bash
```

### Step 2 — Add Your API Key
Open `Form1.cs` and replace the placeholder:
```csharp
private const string API_KEY = "YOUR_ANTHROPIC_API_KEY_HERE";
```
with your actual key:
```csharp
private const string API_KEY = "sk-ant-api03-...";
```

### Step 3 — Run the Application
```bash
dotnet run
```

Or open `AIChatbot.csproj` in Visual Studio and press **F5**.

---

## 🐳 Docker Setup

### Build the Docker image
```bash
docker build -t ai-chatbot .
```

### Run the container
```bash
docker run ai-chatbot
```

> **Note:** Windows Forms apps require Windows containers. Enable Windows containers
> in Docker Desktop settings before building.

---

## 📁 Project Structure

```
AIChatbot/
├── Form1.cs              # Main form logic & Claude API calls
├── Form1.Designer.cs     # UI layout (auto-generated)
├── Program.cs            # App entry point
├── AIChatbot.csproj      # Project configuration
├── Dockerfile            # Docker containerization
└── README.md             # This file
```

---

## 🔑 Getting a Free API Key

1. Visit [https://console.anthropic.com](https://console.anthropic.com)
2. Sign up for a free account
3. Go to **API Keys** → **Create Key**
4. Copy the key and paste it into `Form1.cs`

---

## 📚 Assignment Info

- **Subject:** AI-Based Application Development in .NET
- **Project Type:** Mini Project — AI Chatbot
- **Framework:** .NET 6 Windows Forms
- **AI Backend:** Anthropic Claude (claude-haiku-20240307)

---

## 🔮 Future Improvements

- Save/load chat history to file
- Support for image inputs
- Voice input using Windows Speech API
- Settings panel to change AI model or temperature
- Export conversation as PDF

---

## 📄 License

This project is for educational purposes.
