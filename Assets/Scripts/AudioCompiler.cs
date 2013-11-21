using UnityEngine;
using System.Collections;

public class AudioCompiler : MonoBehaviour {
	
	// Set this sample in the editor.
	public AudioClip sample;	
	
	// Some parameters we can tweak.
	const int frequency = 44100;
	const float trackDuration = 2.0f;
	
	// The PCM data buffer we'll build the track in.	
	private float[] PCMBuffer = new float[ Mathf.CeilToInt( frequency * trackDuration ) * 2 ]; 	
	
	// Our audio clip to hold the track we'll create.
	private AudioClip track;
		
	// MonoBehaviour Start
	void Start() {
		float creationStart = Time.realtimeSinceStartup;
		
		// Now lets copy our sample into our track.
		CompositeSampleOntoTrack( 0.0f, 1.0f );
		CompositeSampleOntoTrack( 0.5f, 0.3f );
		
		CompositeSampleOntoTrack( 1.0f, 0.6f );
		CompositeSampleOntoTrack( 1.125f, 0.3f);
		CompositeSampleOntoTrack( 1.25f, 0.9f );
		CompositeSampleOntoTrack( 1.375f, 0.10f);
		
		CompositeSampleOntoTrack( 1.5f, 0.3f );
		
		float creationEnd = Time.realtimeSinceStartup;
		Debug.Log( "Creating track took " + ( creationEnd - creationStart ) + "s" );
		
		// We have PCM Data. Next step is to turn it into an audio clip.
		track = AudioClip.Create( "Music", Mathf.CeilToInt( frequency * trackDuration ), 2, frequency, false, false );
		track.SetData( PCMBuffer, 0 );
		
		// Now play it.
		camera.GetComponent<AudioSource>().clip = track;
		camera.GetComponent<AudioSource>().Play();
	}
	
	// A utility function to composite our sample onto our track at a given time.
	void CompositeSampleOntoTrack( float time, float volume ) {
		int startSample = Mathf.CeilToInt( time * frequency ) * 2; // 2 channels because we're in stereo
		
		// Read the PCM data out of our sample.
		float[] sampleData = new float[sample.samples];
		sample.GetData( sampleData, 0 );
		
		for( int sampleIndex = 0; sampleIndex < sampleData.Length; sampleIndex++ ) {
			// For each individual sample point in our sample, add it's magnitude to the magnitude in the track.
			int targetIndex = startSample + sampleIndex;
			if( targetIndex >= PCMBuffer.Length ) {
				targetIndex -= PCMBuffer.Length;
			}
			
			PCMBuffer[targetIndex] += sampleData[sampleIndex] * volume;
		}
	}
}
