using UnityEngine;

public class SoundObject : MonoBehaviour
{
	private AudioSource _source;

	private void Awake()
	{
		_source = GetComponent<AudioSource>();
	}

	public void Play(AudioClip clip, Vector3 position, float spatialBlend, float volume = 1f, bool loop = false)
	{
		transform.position = position;
        _source.spatialBlend = spatialBlend;
		Play(clip, volume, loop);
	}

	public void Play(AudioClip clip, float volume = 1f, bool loop = false)
	{
		_source.clip = clip;
		_source.volume = volume;
		_source.loop = loop;
		
		_source.Play();
	}

    private void Update()
	{
		if(!_source.isPlaying)
			Destroy(gameObject);
	}
}
