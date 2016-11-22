FROM microsoft/dotnet:1.0.0-preview2-sdk

WORKDIR /app

ADD . /app

RUN dotnet restore -v minimal \
    && dotnet publish -c Release -o ./ . \
    && rm -rf src/ $HOME/.nuget/

EXPOSE 4000

CMD dotnet web.dll