## Test environment
The APP UI has been released (using Netlify) with the following URL: https://polite-mooncake-479ce3.netlify.app/
The API has been released with the following URL: https://185.218.126.206:9997/ 

## Certificate

Due to the unavailability of a spare domain, the API lacks a valid SSL certificate. Let's Encrypt doesn't permit the generation and binding of a certificate to an IP address; it only supports domains. 
As a temporary solution, I have generated a certificate that can be imported using OpenSSL

- Import the `Certificates/SynonymAppCA.cer` file into your browser, this will tell the browser that served API from our server is legit/trusted.
  - For Chrome : Open settings search for Manage certificates, import the `Certificates/SynonymAppCA.cer` file and trust it, now open the APP on the folowing link: https://polite-mooncake-479ce3.netlify.app/
