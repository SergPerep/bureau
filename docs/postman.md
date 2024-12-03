# Testing connection with postman

The most challenging part of testing connection to Blob storag is authentication. To authenticate with Shared Key we need to generate signature.

When the Azure receives the requests it will check the signature, and if it deems it incorrect, it will refuse the connection.

Look up the version here: 
https://learn.microsoft.com/en-us/rest/api/storageservices/versioning-for-the-azure-storage-services

## Collection variables

| Key | Initial value | Comment |
|---|---|---|
| `accessKey` | 2mEoFQuX7QPtH1uwGLLE... | A constant. Get it from "Access key" tab of Azure storage account. |
| `storageAccount` | bureaustore | A constant. The name of the storage account. |
| `container` | bureau | A constant. The name of the target container. |
| `blobStorageVersion` | 2015-12-11 | A constant. |
| `dateStr` | | Will be generated dynamically by pre-request scripts for each request. |
| `authorizationHeader` | | Will be generated dynamically by pre-request scripts for each request. |


# Get blobs

List container's blobs.

Body is empty

```plain text
# URL
GET https://bureaustore.blob.core.windows.net/bureau?restype=container&comp=list
```

## Headers

| Key | Value |
|---|---|
| x-ms-date | `{{dateStr}}` |
| x-ms-version | `{{blobStorageVersion}}` |
| Authorization | `{{authorizationHeader}}` |

## Pre-request script

```javascript
// For reading blobs

const crypto = require("crypto-js");
const verb = "GET"
const key = pm.collectionVariables.get("key");
const container = pm.collectionVariables.get("container");
const storageAccount = pm.collectionVariables.get("storageAccount");
const dateStr = (new Date()).toUTCString();
const blobStorageVersion = pm.collectionVariables.get("blobStorageVersion");
pm.collectionVariables.set("dateStr", dateStr);

const signature = 
`${verb}\n\n\n\n\n\n\n\n\n\n\n\nx-ms-date:${dateStr}\nx-ms-version:${blobStorageVersion}\n/${storageAccount}/${container}\ncomp:list\nrestype:container`;

let hash = crypto.HmacSHA256(crypto.enc.Utf8.parse(signature), crypto.enc.Base64.parse(key));
hash = crypto.enc.Base64.stringify(hash);

pm.collectionVariables.set("authorizationHeader", `SharedKey ${storageAccount}:${hash}`);
```

# Upload a blob

```plain text
PUT https://bureaustore.blob.core.windows.net/bureau/page_02.jpg
```

body: binary

## Headers

| Key | Value |
|---|---|
| x-ms-blob-type | BlockBlob |
| Authorization | `{{authorizationHeader}}` |
| x-ms-version | `{{blobStorageVersion}}` |
| x-ms-date | `{{dateStr}}` |
| Content-Type | application/octet-stream |
| Content-Length | 273308 |


## Pre-request scrtips

```javascript
const crypto = require("crypto-js");
const verb = "PUT"
const key = pm.collectionVariables.get("key");
const container = pm.collectionVariables.get("container");
const storageAccount = pm.collectionVariables.get("storageAccount");
const blobName = "page_02.jpg";
const contentType = "application/octet-stream";
const contentLength = "273308";
const blobStorageVersion = pm.collectionVariables.get("blobStorageVersion");
const dateStr = (new Date()).toUTCString();
pm.collectionVariables.set("dateStr", dateStr);

const signature = 
`${verb}\n\n\n${contentLength}\n\n${contentType}\n\n\n\n\n\n\nx-ms-blob-type:BlockBlob\nx-ms-date:${dateStr}\nx-ms-version:${blobStorageVersion}\n/${storageAccount}/${container}/${blobName}`;
let hash = crypto.HmacSHA256(crypto.enc.Utf8.parse(signature), crypto.enc.Base64.parse(key));

hash = crypto.enc.Base64.stringify(hash);

pm.collectionVariables.set("authorizationHeader", `SharedKey ${storageAccount}:${hash}`);

```