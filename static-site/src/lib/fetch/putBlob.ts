import { Buffer } from "buffer";
import { PUBLIC_API_BASE_URL } from "$env/static/public";

export default async function putBlob(file: File) {
    let contentLength = file.size;
    const arrayBuffer = await file.arrayBuffer();
    let content: Buffer = Buffer.from(arrayBuffer);
    const blobName = file.name;
    try {
        const res = await fetch(`${PUBLIC_API_BASE_URL}/PutBlob?blobName=${blobName}`, {
            method: "PUT",
            headers: {
                "Content-Length": contentLength.toString(),
                "Content-Type": "application/octet-stream",
            },
            body: content
        })
        if (!res.ok) console.log("Failed to upload file")
        const data = await res.text();
        return data;
    } catch (error) {
        console.log(`Upload block blob ${blobName} successfully`);
    }
}