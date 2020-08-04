/* 
    --------------------------------------------------
    Original code by Code Monkey

    Adapted  and rewritten to my own needs
    --------------------------------------------------
 */

using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SparuvianConnection.Adoptatron.Utils
{
    public class TextWriter : MonoBehaviour {

        private static TextWriter instance;

        private List<TextWriterSingle> textWriterSingleList;

        private void Awake() {
            instance = this;
            textWriterSingleList = new List<TextWriterSingle>();
        }

        public static TextWriterSingle AddWriter_Static(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool visibleCursor, bool removeWriterBeforeAdd, Action onComplete) {
            if (removeWriterBeforeAdd) {
                instance.RemoveWriter(uiText);
            }
            return instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters, visibleCursor, onComplete);
        }

        private TextWriterSingle AddWriter(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool visibleCursor, Action onComplete) {
            TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters, visibleCursor, onComplete);
            textWriterSingleList.Add(textWriterSingle);
            return textWriterSingle;
        }

        public static void RemoveWriter_Static(TMP_Text uiText) {
            instance.RemoveWriter(uiText);
        }

        private void RemoveWriter(TMP_Text uiText) {
            for (int i = 0; i < textWriterSingleList.Count; i++) {
                if (textWriterSingleList[i].GetUIText() == uiText) {
                    textWriterSingleList.RemoveAt(i);
                    i--;
                }
            }
        }

        private void Update() {
            for (int i = 0; i < textWriterSingleList.Count; i++) {
                bool destroyInstance = textWriterSingleList[i].Update();
                if (destroyInstance) {
                    textWriterSingleList.RemoveAt(i);
                    i--;
                }
            }
        }

        /*
     * Represents a single TextWriter instance
     * */
        public class TextWriterSingle {

            private TMP_Text uiText;
            private string textToWrite;
            private int characterIndex;
            private float timePerCharacter;
            private float _timer;
            private bool invisibleCharacters;
            private bool visibleCursor;
            private Action onComplete;

            private bool _startedCursorAnimation = false;
            private float _cursorBlinkingDuration = 1f;
            private bool _currentlyDisplayingCursor = true;
            private char _cursorCharacter = '█';
            // private char _cursorCharacter = '_';

            public TextWriterSingle(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool visibleCursor, Action onComplete) {
                this.uiText = uiText;
                this.textToWrite = textToWrite;
                this.timePerCharacter = timePerCharacter;
                this.invisibleCharacters = invisibleCharacters;
                this.visibleCursor = visibleCursor;
                this.onComplete = onComplete;
                characterIndex = 0;
            }

            // Returns true on complete
            public bool Update() {
                if (!_startedCursorAnimation)
                {
                    _timer -= Time.deltaTime;
                    while (_timer <= 0f) {
                        // Display next character
                        _timer += timePerCharacter;
                        characterIndex++;
                        string text = textToWrite.Substring(0, characterIndex);
                        if (invisibleCharacters) {
                            text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "</color>";
                        }

                        if (visibleCursor)
                        {
                            DrawCursor(text);
                        }
                        else
                        {
                            uiText.text = text;
                        }

                        if (characterIndex >= textToWrite.Length) {
                            // Entire string displayed
                            if (onComplete != null) onComplete();
                            if (visibleCursor)
                            {
                                StartCursorAnimation();
                                return false;
                            }
                            return true;
                        }
                    }
                }
                else
                {
                    _timer += Time.deltaTime;
                    if (_timer > _cursorBlinkingDuration)
                    {
                        ToggleCursor();
                    }
                }

                return false;

            }

            private void DrawCursor(string text)
            {
                int lineCount = uiText.textInfo.lineCount;

                uiText.text = text + _cursorCharacter;
                uiText.ForceMeshUpdate();

                if (lineCount != uiText.textInfo.lineCount)
                {
                    uiText.text = text + ' ' + _cursorCharacter;
                }
            }

            private void ToggleCursor()
            {
                if (_currentlyDisplayingCursor)
                {
                    uiText.text = textToWrite + ' ';
                    _currentlyDisplayingCursor = false;
                }
                else
                {
                    DrawCursor(textToWrite);
                    _currentlyDisplayingCursor = true;
                }
                
                
                _timer = 0;
            }

            private void StartCursorAnimation()
            {
                _startedCursorAnimation = true;
                _timer = 0;
            }

            public TMP_Text GetUIText() {
                return uiText;
            }

            public bool IsActive() {
                return characterIndex < textToWrite.Length;
            }

            public void WriteAllAndDestroy() {
                uiText.text = textToWrite + _cursorCharacter;
                characterIndex = textToWrite.Length;
                if (onComplete != null) onComplete();
                
                if (visibleCursor)
                {
                    StartCursorAnimation();
                }
                else
                {
                    TextWriter.RemoveWriter_Static(uiText);
                }
            }


        }


    }
}
