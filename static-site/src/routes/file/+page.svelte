<script lang="ts">
	import { browser } from '$app/environment';
	import getBlobProps from '$lib/fetch/getBlobProps';

	let name: string | null = null;

	if (browser) {
		const urlParams = new URLSearchParams(window.location.search);
		name = urlParams.get('name');
	}

	const extension = name?.split('.')[1];
	let blobUrl: string | undefined = $state(undefined);
	let status: string | undefined = $state(undefined);
	$effect(() => {
		if (name !== null)
			getBlobProps(name).then((value) => {
				(blobUrl = value.url), (status = value.metadata?.status);
			});
	});
</script>

<h1>File name: {name}</h1>

{#if extension === 'jpg' && blobUrl !== undefined}
	<img src={blobUrl} alt="comics" />
{/if}
{#if status}
	<p>Status: {status}</p>
{/if}

<style>
	img {
		display: block;
		width: 100%;
		max-width: 50rem;
	}
</style>
