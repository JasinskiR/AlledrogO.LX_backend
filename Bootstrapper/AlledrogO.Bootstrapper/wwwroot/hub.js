const connectButton = document.getElementById("connectButton");
const sendButton = document.getElementById("sendButton");

let myId = null;
let myRole = null;

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
    // get chatId from user input
    const chatId = document.getElementById("chatInput").value;

    // get myId from server
    await fetch(`http://localhost:5000/api/ChatUser/info`, {
        headers: {
            "Authorization": "Bearer " + document.getElementById("tokenInput").value
        }})
        .then(response => response.json())
        .then(userdata => { myId = userdata.id; })
        .catch(error => console.error('Error:', error));

    // start signalR connection
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5000/chat?chatId=" + chatId)
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
            fetch(`http://localhost:5000/api/ChatUser/chats/${chatId}`, {
                headers: {
                    "Authorization": "Bearer " + document.getElementById("tokenInput").value
                },
            })
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
    const route = `http://localhost:5000/api/ChatUser/chats/${chatId}`;
    console.log(route);
    await fetch(route, {
        method: "PATCH",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + document.getElementById("tokenInput").value
        },
        body: JSON.stringify(messageObject)
    });
});