using UnityEngine;

namespace Assets.Code
{
    public class GameExtensions
    {
        public static AudioSource PlayClipAtPoint(Vector3 position, AudioClip clip)
        {
            var gameObject = new GameObject("One shot audio");
            var source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            gameObject.transform.position = position;

            Object.Destroy(gameObject, clip.length);
            source.Play();

            return source;
        }
    }
}