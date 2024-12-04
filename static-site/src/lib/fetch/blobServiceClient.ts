import { BlobServiceClient } from "@azure/storage-blob";

import {
    PUBLIC_AZURE_BLOB_STORAGE_ACCOUNT_URL,
    PUBLIC_AZURE_BLOB_STORAGE_ACCOUNT_SAS_TOKEN,
    PUBLIC_AZURE_BLOB_CONTAINER } from "$env/static/public";
if (
    PUBLIC_AZURE_BLOB_STORAGE_ACCOUNT_URL == undefined ||
    PUBLIC_AZURE_BLOB_STORAGE_ACCOUNT_SAS_TOKEN == undefined ||
    PUBLIC_AZURE_BLOB_CONTAINER == undefined
) {
    throw new Error("env variable are not provided");
}

const blobServiceClient = new BlobServiceClient(`${PUBLIC_AZURE_BLOB_STORAGE_ACCOUNT_URL}?${PUBLIC_AZURE_BLOB_STORAGE_ACCOUNT_SAS_TOKEN}`);
const containerClient = blobServiceClient.getContainerClient(PUBLIC_AZURE_BLOB_CONTAINER);
export default containerClient;