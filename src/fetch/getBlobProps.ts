
import containerClient from './blobServiceClient';

export default async function getBlobProps(blobName: string) {
    const blobClient = containerClient.getBlobClient(blobName);
    const {metadata} = await blobClient.getProperties()
    return { url: blobClient.url, metadata };
}