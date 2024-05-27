const connectButton = document.getElementById("connectButton");
const sendButton = document.getElementById("sendButton");

let myId = null;
let myRole = null;

// sprawdzenie jakie Id ma zalogowany użytkownik
document.addEventListener("DOMContentLoaded", async () => {
    
    await fetch(`/api/ChatUser/info`)
        .then(response => response.json())
        .then(userdata => { myId = userdata.id; });

    console.log(myId);
});

const messagesList = document.getElementById("messagesList");

function addMessageToChat(message) {
    const newMessage = document.createElement("div");
    const isMyMessage = message.sentByBuyer === (myRole === "buyer");
    newMessage.classList.add("mb-2", "rounded", "px-3", "py-2", 
        isMyMessage ? "bg-primary" : "bg-secondary", 
        "text-white", 
        isMyMessage ? "align-self-start" : "align-self-end");
    newMessage.textContent = message.content;
    messagesList.appendChild(newMessage);
}
connectButton.addEventListener("click", async event => {
    
    const chatId = document.getElementById("chatInput").value;

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chat?chatId=" + chatId)
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ReceiveMessage", (message) => {
        addMessageToChat(message);
    });

    connection.onclose(async () => {
        await start();
    });

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
            connectButton.textContent = "Połączono";
            connectButton.disabled = true;
            connectButton.classList.add("btn-success");
            fetch(`/api/ChatUser/chats/${chatId}`)
                .then(response => response.json())
                .then(chatHistory => {
                    myRole = chatHistory.buyerId === myId ? "buyer" : "seller";
                    chatHistory.messages.forEach(message => {
                        addMessageToChat(message);
                    });
                })
                .catch(error => console.error('Error:', error));
            
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };
    start();
});

sendButton.addEventListener("click", async event => {
    const message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";
    const chatId = document.getElementById("chatInput").value;
    const messageObject = {
        content: message
    };
    const route = `/api/ChatUser/chats/${chatId}`;
    console.log(route);
    await fetch(route, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(messageObject)
    });
});