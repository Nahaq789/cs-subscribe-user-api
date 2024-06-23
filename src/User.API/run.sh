#!/bin/bash
set -e

dotnet ef database update --project ../User.Infrastructure/User.Infrastructure.csproj
