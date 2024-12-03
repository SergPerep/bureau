import containerClient from "./blobServiceClient";
import { Buffer } from "buffer";

export default async function putBlob(file: File){
    let contentLength = file.size;
    // stat(filePath, (err, stats) => {
    //     if (err) throw err;
    //     contentLength = stats.size;
    // })
    const arrayBuffer = await file.arrayBuffer();
    let content: Buffer = Buffer.from(arrayBuffer);
    // readFile(filePath, (err, data) => {
    //     if (err) throw err;
    //     content = data;
    // })
    const blobName = file.name;
    const blockBlobClient = containerClient.getBlockBlobClient(blobName);
    const uploadBlobResponse = await blockBlobClient.upload(content, contentLength);
    console.log(`Upload block blob ${blobName} successfully`, uploadBlobResponse.requestId)
    return uploadBlobResponse;
}