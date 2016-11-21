﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LyncLogger
{
    class logRenderer
    {
        private RichTextBox box;
        private string currentUser = null;
        MatchCollection searchMatches;
        private int highlightedIndex = -1;
        private string currentFile;
        private int rowBatchSize = 100;
        private int oldestRowIndex = -1;
        private string[] fileRows;
        private bool entireFileLoaded;

        private static logRenderer instance;

        private logRenderer(RichTextBox box, string currentUser)
        {
            this.box = box;
            this.currentUser = currentUser;
        }

        public static logRenderer getInstance(RichTextBox box, string currentUser)
        {
            if (instance == null)
            {
                instance = new logRenderer(box, currentUser);
            }

            return instance;
        }

        public void processFile(string fileName)
        {
            if (!File.Exists(fileName))
            {
                System.Windows.Forms.MessageBox.Show("File doesn\'t exists");
                return;
            }

            entireFileLoaded = false;
            currentFile = fileName;
            searchMatches = null;
            oldestRowIndex = -1;
            string text = System.IO.File.ReadAllText(@fileName);

            box.Text = "";
            fileRows = text.Split('\n');
            loadRowBatch();

            box.Select(box.Text.Length - 1, 0);
            box.ScrollToCaret();
        }

        public void loadRowBatch()
        {
            if (entireFileLoaded)
            {
                return;
            }

            int currentRow;
            if (oldestRowIndex == -1)
            {
                currentRow = fileRows.Length - 1;
            }
            else
            {
                currentRow = oldestRowIndex;
            }

            int boundry = Math.Max(-1, currentRow - rowBatchSize);
            int i;
            for (i = currentRow; i > boundry; i--)
            {
                this.formatRow(fileRows[i], true);
            }

            if (i == -1)
            {
                entireFileLoaded = true;
            }

            oldestRowIndex = i;
        }

        public void addRowForFile(string row, string fileName){
            if (fileName == currentFile)
            {
                formatRow(row);
            }
        }
        
        public static void onLogConversation(Object source, FileLoggerEventArgs args)
        {
            if (instance == null)
            {
                return;
            }

            instance.addRowForFile(args.message, args.fileName);
        }

        public void formatRow(string text, bool prepend = false)
        {
            Color color = Color.Red;

            if (prepend)
            {
                box.Select(0, 0);
            }
            else
            {
                box.Select(box.Text.Length - 1, 0);
            }

            // match date
            Regex colorTest = new Regex("^(?<date>[0-9]{4}-[0-9]{1,2}-[0-9]{1,2} [0-9]{1,2}:[0-9]{1,2}:[0-9]{1,2})");
            MatchCollection matches = colorTest.Matches(text);
            text = colorTest.Replace(text, "");

            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    addColored(groups["date"].Value, Color.DarkBlue);
                }

                // match user
                Regex nameTest = new Regex(@"^\s*(?<name>[^:]+):");
                matches = nameTest.Matches(text);
                text = nameTest.Replace(text, "");
                foreach (Match match in matches)
                {
                    GroupCollection groups = match.Groups;
                    Color nameColor;
                    string name;

                    if (groups["name"].Value == currentUser)
                    {
                        nameColor = Color.Green;
                        name = "Me";
                    }
                    else
                    {
                        nameColor = Color.Red;
                        name = groups["name"].Value;
                    }

                    addColored(" " + name + ":", nameColor);
                }
            }

            addColored(text + "\n", Color.Black);
        }

        private void addColored(string text, Color color)
        {
            box.SelectionColor = color;
            box.SelectedText = text;
            box.SelectionColor = box.ForeColor;
        }

        public void search(string searchText)
        {
            string text = box.Text;

            Regex nameTest = new Regex(@"(?i)(?<found>"+ searchText + ")");
            searchMatches = nameTest.Matches(text);
            text = nameTest.Replace(text, "");

            if (searchMatches.Count > 0)
            {
                displyFound(searchMatches.Count - 1);
            }
            else
            {
                box.Select(0, box.Text.Length - 1);
                box.Select(box.Text.Length - 1, 0);
                highlightedIndex = -1;
            }
        }

        public void shiftMatch(int shift)
        {
            if (searchMatches == null)
            {
                return;
            }

            int newIndex = highlightedIndex + shift;
            if (newIndex >= searchMatches.Count)
            {
                newIndex = 0; // rewind to first
            }

            if (newIndex < 0)
            {
                newIndex = searchMatches.Count - 1; // go to last
            }

            displyFound(newIndex);
        }

        private void displyFound(int index)
        {
// TODO: clear search at content change and redo it
            highlightedIndex = index;

            box.Select(0, box.Text.Length - 1);
            box.SelectionBackColor = Color.Transparent;

            Match match = searchMatches[index];

            GroupCollection groups = match.Groups;

            box.Select(groups["found"].Index, groups["found"].Length);
            box.ScrollToCaret();
            box.SelectionBackColor = Color.Yellow;
        }
    }
}