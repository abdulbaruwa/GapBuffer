using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using ToroDabEditor;

namespace GapBufferWpfClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder _textString;
        private CustomTextSource _textStore;
        private FontRendering _currentRendering;
        private TextGapBuffer _textBuffer;
        private Document _document;
        private TextLineManager _textLineManager;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            InitializeBuffer();
        }

        private void InitializeBuffer()
        {
            controlRectangle.Focusable = true;
            controlRectangle.Focus();
            if (_textStore == null) _textStore = new CustomTextSource();
            if (_textString == null) _textString = new StringBuilder();
            if(_currentRendering == null) _currentRendering  = new FontRendering();
            if(_textLineManager == null) _textLineManager = new TextLineManager();

            if(_textBuffer == null) _textBuffer = new TextGapBuffer(10);
            if (_document == null) _document = new Document(_textBuffer, _textLineManager);
        }


        private void UpdateFormattedText(string textstring)
        {
            // Make sure all UI is loaded

            int textStorePosition = 0;                //Index into the text of the textsource
            Point linePosition = new Point(0, 0);     //current line

            // Create a DrawingGroup object for storing formatted text.
            textArea = new DrawingGroup();
            DrawingContext dc = textArea.Open();

            // Update the text store.
            _textStore.Text = textstring;
            _textStore.FontRendering = _currentRendering;

            // Create a TextFormatter object.
            TextFormatter formatter = TextFormatter.Create();

            // Format each line of text from the text store and draw it.
            while (textStorePosition < _textStore.Text.Length)
            {
                // Create a textline from the text store using the TextFormatter object.
                using (TextLine myTextLine = formatter.FormatLine(
                    _textStore,
                    textStorePosition,
                    96 * 6,
                    new GenericTextParagraphProperties(_currentRendering),
                    null))
                {
                    // Draw the formatted text into the drawing context.
                    myTextLine.Draw(dc, linePosition, InvertAxes.None);

                    // Update the index position in the text store.
                    textStorePosition += myTextLine.Length;

                    // Update the line position coordinate for the displayed line.
                    linePosition.Y += myTextLine.Height;
                }
            }

            // Persist the drawn text content.
            dc.Close();

            // Display the formatted text in the DrawingGroup object.
            gapBufferDrawingBrush.Drawing = textArea;
        }

        private void controlRectangle_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            //Insert the text into the buffer
            _textBuffer.InsertString(e.Text);
            //Push the text from buffer to screen
            UpdateFormattedText(_textBuffer.GetText());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            InitializeBuffer();
            var someText =
                @"
Now this can be obviated by having the domain service implement 
a number of interfaces, and hand to its clients interfaces that 
only cover the concerns they have. With application service layers this 
naturally tends towards one method 
per interface.";
            _textBuffer.InsertString(someText);
            UpdateFormattedText(_textBuffer.GetText());
        }
    }
}
