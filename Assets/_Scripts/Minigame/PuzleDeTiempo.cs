using System;
using System.Collections;
using System.Collections.Generic;
using Cutegame;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cutegame.Minigames
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PuzleDeTiempo : TimedPuzzle
    {
        // Have a list of pieces that move when the game begins.
        // if all pieces have been set into their spots, then win!
        [Header("Ranges")] [Range(5f, 25f)] public float acceptRange = 10f;
        [Range(1f, 25f)] public float spawnAreaRange = 5f;

        [Header("Images")] [Range(1, 5)] public int columns = 3;
        public List<Sprite> listOfSprites = new List<Sprite>();

        [Header("spawns")] 
        public RectTransform puzzleCenter;
        public RectTransform spawnArea;

        [Header("Prefab")] public PuzzlePiece prefab;
        
        private List<PuzzlePiece> ListOfPieces;
        
        private List<PuzzlePiece> completedPieces = new List<PuzzlePiece>();
        public CanvasGroup _canvasGroup;
        
        public override void StartMinigame()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;

            SpawnPieces();
            
            base.StartMinigame();
        }

        private void SpawnPieces()
        {  
            ListOfPieces = new List<PuzzlePiece>();
            for (int i=0; i < listOfSprites.Count; i++)
            {
                Vector2 correctPos = new Vector2();
                correctPos.x = puzzleCenter.localPosition.x + ((-1+i%columns)* puzzleCenter.rect.width);
                correctPos.y = puzzleCenter.localPosition.y - ((-1+i/columns)* puzzleCenter.rect.height);
                
                var p = Instantiate(prefab, transform, false);
                p.transform.localScale = Vector3.one;

                p.Setup(
                    correctPos,
                    (Vector2) spawnArea.position + new Vector2(Random.Range(-spawnAreaRange, spawnAreaRange), Random.Range(-spawnAreaRange, spawnAreaRange)),
                    acceptRange,
                    listOfSprites[i],
                    AddPoint
                    );
                ListOfPieces.Add(p);
            }
        }

        private void AddPoint(PuzzlePiece piece)
        {
            if (!isPlaying) return;
            
            if(!completedPieces.Contains(piece))
                completedPieces.Add(piece);

            if (completedPieces.Count >= ListOfPieces.Count)
                WinGame();
        } 
    }
}