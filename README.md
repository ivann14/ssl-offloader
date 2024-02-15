# SSL Offloader

The SSL Offloader is a proxy service that handles SSL/TLS termination for your applications. It offloads the CPU-intensive encryption and decryption tasks from the application server to the proxy server, improving the performance of the application server.

The SSL Offloader creates a self-signed SSL certificate for development purposes. This certificate is not trusted by default, so you'll need to manually import and trust it on your system.

## Example Folder

The `example` folder contains an example setup for the SSL Offloader. It includes a Docker Compose file that sets up the SSL Offloader and an example application, and a `volumes` directory that is used for sharing the SSL certificate between the SSL Offloader and the application.

To use the example setup:

1. Navigate to the `example` directory.
2. Run `docker-compose up -d` to start the SSL Offloader and the example application.
3. The SSL Offloader will create a self-signed SSL certificate and place it in the `volumes` directory.
4. The example application will use this certificate for SSL/TLS termination.

## Importing and Trusting the Certificate

### Windows with .NET CORE SDK installed
Generate your own certificate via .NET SDK then place it in directory that will be mounted to `/certificate-volume`

```bash
dotnet dev-certs https --export-path $PATH_TO_DIRECTORY/dev-certificate.pfx -p $CERTIFICATE_PASSWORD
dotnet dev-certs https --clean --import $PATH_TO_DIRECTORY/dev-certificate.pfx -p $CERTIFICATE_PASSWORD
```

### Windows

1. Open a Command Prompt as Administrator.
2. Navigate to the directory containing the certificate file.
3. Run the following command:

```bash
$pwd = ConvertTo-SecureString -String "your_certificate_password" -Force -AsPlainText
Import-PfxCertificate -FilePath "path_to_your_certificate.pfx" -CertStoreLocation Cert:\LocalMachine\Root -Password $pwd
```

### Mac OS

1. Open Keychain Access.
2. Go to File > Import Items.
3. Navigate to the location of the certificate file, select it, and click Open.
4. In the Keychains list, click Certificates.
5. Double-click the certificate, expand the Trust section, and under When using this certificate, select Always Trust. Providing Your Own SSL Certificate. If you want to use your own SSL certificate instead of the self-signed certificate, you can do so by placing it on the path /certificate-volume/dev-certificate.pfx on your host computer. This path is mounted as a volume in the Docker container, so the SSL Offloader will use your certificate for SSL/TLS termination.