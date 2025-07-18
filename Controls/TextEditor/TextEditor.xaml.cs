using System.Windows;
using System.Windows.Controls;

namespace Controls{
    public partial class TextEditor : UserControl{
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextEditor),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text{
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public TextEditor(){
            InitializeComponent();
            // Initialize the textBox if it is defined in XAML with x:Name="textBox"
            textBox = (this.FindName("textBox") as TextBox)!;
            if (textBox == null){
                throw new System.InvalidOperationException("TextBox with x:Name='textBox' was not found in the XAML.");
            }
        }

        // テキストボックスの内容を取得
        public string GetText(){
            return textBox?.Text ?? string.Empty;
        }

        // テキストボックスの内容を設定
        public void SetText(string text){
            if (textBox != null){
                textBox.Text = text;
            }
        }

        // Undo操作
        public void Undo(){
            textBox?.Undo();
        }

        // Redo操作
        public void Redo(){
            textBox?.Redo();
        }

        // 全選択
        public void SelectAll(){
            textBox?.SelectAll();
        }

        // クリア
        public void Clear(){
            textBox?.Clear();
        }
    }
}
