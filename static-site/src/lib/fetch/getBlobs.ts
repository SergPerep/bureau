
import containerClient from './blobServiceClient';

export default async function getBlobs() {
    let i = 1;
    let names: string[] = [];
    const blobs = containerClient.listBlobsFlat();
    for await (const blob of blobs) {
        names = [...names, blob.name]
        console.log(`Blob ${i++}: ${blob.name}`);
    }
    return names;
}