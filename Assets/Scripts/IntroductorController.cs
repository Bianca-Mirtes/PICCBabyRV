using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class IntroductorController : MonoBehaviour
{
    public AnimationClip clip;       // Sua anima��o
    public float frameRate = 30f;    // Frame rate da anima��o

    private PlayableGraph graph;
    private AnimationClipPlayable clipPlayable;
    private float currentFrame = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Cria o PlayableGraph
        graph = PlayableGraph.Create("AnimGraph");
        var output = AnimationPlayableOutput.Create(graph, "Animation", GetComponent<Animator>());

        // Cria o clip playable
        clipPlayable = AnimationClipPlayable.Create(graph, clip);
        clipPlayable.SetApplyFootIK(false);  // Opcional
        clipPlayable.SetApplyPlayableIK(false); // Opcional

        output.SetSourcePlayable(clipPlayable);
        graph.Play();

        // Pausa a anima��o (para controle manual)
        clipPlayable.SetSpeed(0);
    }

    public void NextKeyframe()
    {
        // Avan�a um frame
        currentFrame++;

        float time = currentFrame / frameRate;

        // Se passar do tempo da anima��o, para
        if (time > clip.length)
        {
            time = clip.length;
            FindFirstObjectByType<ControllerUTI>().ProcessRealizarCompleteIntroduction();
        }
        clipPlayable.SetTime(time);
        clipPlayable.SetTime(time); // chamada dupla para for�ar update visual
    }

    void OnDestroy()
    {
        graph.Destroy(); // Sempre destrua o grafo para liberar recursos
    }
}
