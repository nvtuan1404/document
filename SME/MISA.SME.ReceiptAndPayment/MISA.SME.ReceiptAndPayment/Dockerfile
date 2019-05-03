FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY MISA.SME.ReceiptAndPayment/MISA.SME.ReceiptAndPayment.csproj MISA.SME.ReceiptAndPayment/
RUN dotnet restore MISA.SME.ReceiptAndPayment/MISA.SME.ReceiptAndPayment.csproj
COPY . .
WORKDIR /src/MISA.SME.ReceiptAndPayment
RUN dotnet build MISA.SME.ReceiptAndPayment.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish MISA.SME.ReceiptAndPayment.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MISA.SME.ReceiptAndPayment.dll"]
