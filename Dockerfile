# Sets the base image for the build stage. 
# It uses the mcr.microsoft.com/dotnet/sdk:8.0-alpine image, which is a slim image containing the .NET 8.0 SDK for the Alpine Linux distribution.
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build     
# An argument named TARGETARCH that can be passed when building the image. 
# This argument likely specifies the target architecture (e.g., amd64, x64) for which the application is being built.
ARG TARGETARCH
# Copies all files from the current directory (where the Dockerfile resides) into the /source directory inside the container.
COPY . /source
# Sets the working directory within the container to /source/BoardGame. This is likely where your application source code resides.
WORKDIR /source/BoardGame
ARG TARGETARCH
# Uses the RUN instruction to execute a command.
    # --mount=type=cache,id=nuget,target=/root/.nuget/packages creates a named cache volume called nuget and mounts it to the /root/.nuget/packages directory. This helps speed up future builds by reusing downloaded packages.
    # dotnet publish command publishes the application for the target architecture specified by the TARGETARCH argument.
        # -a ${TARGETARCH/amd64/x64} replaces "amd64" with "x64" in the architecture string if TARGETARCH is "amd64". This likely adjusts the output directory based on the architecture.
        # --use-current-runtime uses the .NET runtime already present in the image (the SDK).
        # --self-contained false creates a non-self-contained deployment, meaning it expects a separate .NET runtime to be available at runtime.
        # -o /app sets the output directory for the published application to /app.
RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app

# Creates a new stage named development based on the same .NET SDK image used in the build stage.
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS development
# Copies the source code and sets the working directory.
COPY . /source
WORKDIR /source/BoardGame
# Defines the default command to run when starting a container from the development stage.
CMD dotnet run --no-launch-profile

# Creates the final stage named final based on the mcr.microsoft.com/dotnet/aspnet:8.0-alpine image. This image contains the .NET runtime environment needed to run the application.
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
# Sets the working directory within the final image to /app.
WORKDIR /app

# Copies the published application files (/app) from the build stage (--from=build) to the /app directory in the final image.
COPY --from=build /app .
# Creates a new user named appuser
ARG UID=10001
RUN adduser \
    --disabled-password \
    --gecos "" \
    --home "/nonexistent" \
    --shell "/sbin/nologin" \
    --no-create-home \
    --uid "${UID}" \
    appuser

USER appuser

# ENTRYPOINT specifies the command that will be executed when the container starts. 
# In this case, it runs the dotnet executable with the BoardGame.dll file as an argument. 
# This assumes that the BoardGame.dll file is located within the /app directory.
ENTRYPOINT ["dotnet", "BoardGame.dll"]
