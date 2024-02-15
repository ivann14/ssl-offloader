#!/bin/sh
set -e

FULL_PATH_TO_CERTIFICATE="/certificate-volume/dev-certificate.pfx"

dotnet dev-certs https --export-path $FULL_PATH_TO_CERTIFICATE -p $CERTIFICATE_PASSWORD
dotnet dev-certs https --clean --import $FULL_PATH_TO_CERTIFICATE -p $CERTIFICATE_PASSWORD

dotnet /App/out/proxy.dll --proxyToUrl $PROXY_TO_URL