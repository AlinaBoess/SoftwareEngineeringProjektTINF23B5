#!/bin/bash

echo "Paste your MySQL connection string"
read -r CONNECTION_STRING

CONFIG_PATH="programCode/webApi/appsettings.Development.json"
CORRECT_HASH="66f26de08fe7788f95af1e8cd53fc055ce6b2c0bc74a0ff4d01ee600d4e03334"

HASH=$(echo -n "$CONNECTION_STRING" | sha256sum | awk '{print $1}')

if [ "$HASH" != "$CORRECT_HASH" ]; then
  echo "Incorrect connection string. Aborting."
  exit 1
fi

# Create directory if needed
mkdir -p "$(dirname "$CONFIG_PATH")"

cat <<EOF > "$CONFIG_PATH"
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Jwt": {
    "Key": "ThisIsASecureKeyForJwtTokensussybaka",
    "Issuer": "TestApi",
    "Audience": "TestApi"
  },
  "ConnectionStrings": {
    "DefaultConnection": "$CONNECTION_STRING"
  }
}
EOF

echo "Success"
