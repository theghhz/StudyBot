#!/bin/bash

echo "[Study Bot API] Iniciando a API, aguarde!"

(cd Api/Api.StudyBot/StudyBot.Api && dotnet run) &
API_PID=$!

echo "[Study Bot API] Aguardando a API iniciar em http://localhost:5469..."

sleep 10

echo "✅ API iniciada com sucesso!"

echo "[Study Bot Discord] Iniciando o bot Discord..."
(cd Bot-Discord && node index.js)

wait $API_PID
