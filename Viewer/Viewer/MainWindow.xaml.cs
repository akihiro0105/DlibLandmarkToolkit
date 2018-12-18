using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Viewer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private Rectangle box = null;
        private List<Ellipse> partsList=new List<Ellipse>();

        public MainWindow()
        {
            InitializeComponent();
            // ドラック&ドロップ処理
            this.AllowDrop = true;
            this.PreviewDragOver += (sender, args) =>
            {
                args.Effects = (args.Data.GetDataPresent(DataFormats.FileDrop, true))
                    ? DragDropEffects.Copy
                    : DragDropEffects.None;
                args.Handled = true;
            };
            this.Drop += (sender, args) =>
            {
                var files = args.Data.GetData(DataFormats.FileDrop) as string[];
                startInitialize(files[0]);
            };
            // コマンドライン引数
            if (App.commandFilename!=null)
            {
                startInitialize(App.commandFilename);
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        private void startInitialize(string xmlpath)
        {
            // xmlファイル
            var filepath = Path.GetDirectoryName(xmlpath) + "\\";
            var filename = Path.GetFileName(xmlpath);
            
            // 初期化
            var sw = new Stopwatch();
            var xml = new XMLViewer(filepath + filename);
            var list = xml.GetDatasetImages();
            // 画像切替タイミング
            var intervalTime = 0;
            // 選択画像
            var selectImage = 0;
            // 選択パーツ
            var selectItem = 0;
            // Drag中
            var dragActive = false;
            var dragPoint = new Point();

            // マウスドラッグ操作定義
            ImageGrid.MouseDown += (sender, args) => dragActive = true;
            ImageGrid.MouseMove += (sender, args) =>
            {
                dragPoint = args.GetPosition(ViewImage);
                BoxLabel.Content = "X = " + dragPoint.X + " : Y = " + dragPoint.Y;
                if (dragActive) setPointList(selectItem, list[selectImage], dragPoint);
            };
            ImageGrid.MouseUp += (sender, args) => dragActive = false;
            // キーボード入力動作定義
            this.KeyDown += (sender, args) =>
            {
                sw.Stop();
                if (sw.ElapsedMilliseconds <= intervalTime) sw.Start();
                else
                {
                    sw.Restart();
                    if (args.Key == Key.A|| args.Key == Key.D)
                    {
                        // 前の画像へ
                        if (args.Key == Key.A) selectImage--;
                        // 次の画像へ
                        if (args.Key == Key.D) selectImage++;
                        // 画像更新
                        if (selectImage < 0) selectImage = 0;
                        if (selectImage > list.Count - 1) selectImage = list.Count - 1;
                        setViewImage(list[selectImage], filepath);
                        if (dragActive) setPointList(selectItem, list[selectImage], dragPoint);
                    }
                    // interval調整
                    if (args.Key == Key.W) intervalTime += 10;
                    if (args.Key == Key.S) intervalTime = (intervalTime - 10 < 0) ? 0 : intervalTime - 10;
                }
                ImageCountLabel.Content = selectImage + "/" + list.Count;
                IntervalLabel.Content = intervalTime;
            };
            // 保存ボタン動作定義
            SaveButton.Click += (sender, args) =>
            {
                foreach (var data in list)
                {
                    if (data.box.width < 0)
                    {
                        data.box.width *= -1;
                        data.box.left -= data.box.width;
                    }

                    if (data.box.height < 0)
                    {
                        data.box.height *= -1;
                        data.box.top -= data.box.height;
                    }
                }
                xml.SaveXML(filepath + filename);
            };
            // パーツリスト
            DataList.SelectionChanged += (sender, args) =>
            {
                if (DataList.SelectedIndex == -1) DataList.SelectedIndex = selectItem;
                else selectItem = DataList.SelectedIndex;
            };
            // 初期化完了
            setViewImage(list[selectImage], filepath);
            DataList.SelectedIndex = selectItem;
            sw.Start();
            BoxLabel.Content = filepath + " " + filename;
        }

        /// <summary>
        /// 入力から枠かパーツの情報を更新
        /// </summary>
        /// <param name="selectItem"></param>
        /// <param name="data"></param>
        /// <param name="point"></param>
        private void setPointList(int selectItem, datasetImage data,Point point)
        {
            // box位置
            if (selectItem == 0)
            {
                data.box.top = (int)point.Y;
                data.box.left = (int)point.X;
                setBoxView(data.box.left, data.box.top, data.box.width, data.box.height);
            }
            // boxサイズ
            else if (selectItem == 1)
            {
                data.box.width = (int) point.X - data.box.left;
                data.box.height = (int) point.Y - data.box.top;
                setBoxView(data.box.left, data.box.top, data.box.width, data.box.height);
            }
            // parts位置
            else
            {
                data.box.part[selectItem - 2].x = (int)point.X;
                data.box.part[selectItem - 2].y = (int)point.Y;
                setParts(selectItem - 2, point.X, point.Y);
            }
        }

        /// <summary>
        /// 画像上に物体認識枠を表示
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void setBoxView(double left,double top,double width,double height)
        {
            if (box == null)
            {
                box = new Rectangle();
                box.Stroke = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
                box.HorizontalAlignment = HorizontalAlignment.Left;
                box.VerticalAlignment = VerticalAlignment.Top;
                ImageGrid.Children.Add(box);
            }

            if (width < 0)
            {
                width *= -1;
                left -= width;
            }

            if (height < 0)
            {
                height *= -1;
                top -= height;
            }

            box.Width = width;
            box.Height = height;
            box.Margin = new Thickness(left, top, 0, 0);
        }

        /// <summary>
        /// 画像上にパーツ点を表示
        /// </summary>
        /// <param name="number"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void setParts(int number,double x,double y)
        {
            var size = 10.0;
            if (partsList.Count<=number)
            {
                var parts = new Ellipse();
                parts.Width = parts.Height = size;
                parts.HorizontalAlignment = HorizontalAlignment.Left;
                parts.VerticalAlignment = VerticalAlignment.Top;
                parts.Fill = new SolidColorBrush(Color.FromArgb(127, 0, 0, 255));
                ImageGrid.Children.Add(parts);
                partsList.Add(parts);
            }

            partsList[number].Margin = new Thickness(x - size / 2, y - size / 2, 0, 0);
        }

        /// <summary>
        /// リストと画像を表示
        /// </summary>
        /// <param name="data"></param>
        /// <param name="filepath"></param>
        private void setViewImage(datasetImage data,string filepath)
        {
            // リスト更新
            var listName = new List<string>();
            listName.Add("box point");
            listName.Add("box size");
            if (data.box.part!=null)
            {
                foreach (var t in data.box.part) listName.Add(t.name.ToString());
            }
            DataList.ItemsSource = listName;
            // 表示更新

            ViewImage.Source = new BitmapImage(new Uri(Path.GetFullPath(filepath + data.file)));
            ViewImage.Width = ViewImage.Source.Width;
            ViewImage.Height = ViewImage.Source.Height;

            setBoxView(data.box.left, data.box.top, data.box.width, data.box.height);
            if (data.box.part!=null)
            {
                for (int i = 0; i < data.box.part.Length; i++) setParts(i, data.box.part[i].x, data.box.part[i].y);
            }
        }
    }
}
