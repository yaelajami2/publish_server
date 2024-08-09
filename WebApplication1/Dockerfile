# שלב 1: בניית התמונה (build stage)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# הגדר את ספריית העבודה
WORKDIR /app

# העתק את קובץ התיאור של הפרויקט (.csproj) ואת קובץ הפתרון (אם יש)
COPY *.csproj ./

# התקן את התלויות
RUN dotnet restore

# העתק את שאר הקבצים של הפרויקט
COPY . ./

# בניית האפליקציה
RUN dotnet publish -c Release -o out

# שלב 2: יצירת התמונה הסופית (runtime stage)
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# הגדר את ספריית העבודה
WORKDIR /app

# העתק את האפליקציה מהשלב הקודם
COPY --from=build /app/out .

# הגדר את הפורט שבו האפליקציה תרוץ
EXPOSE 80

# הפקודה שתופעל כשמתחילים את הקונטיינר
ENTRYPOINT ["dotnet", "YourApp.dll"]
