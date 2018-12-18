using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace Viewer
{
    class XMLViewer
    {
        private dataset data=null;
        public XMLViewer(string path)
        {
            // xml読み込み
            var fs = new FileStream(path, FileMode.Open);
            // classに変換
            var serializer=new XmlSerializer(typeof(dataset));
            data = (dataset)serializer.Deserialize(fs);
        }

        public List<datasetImage> GetDatasetImages()
        {
            if (data == null) return null;
            var list = new List<datasetImage>();
            for (int i = 0; i < data.images.Length; i++)
            {
                list.Add(data.images[i]);
            }

            return list;
        }

        public void SaveXML(string path)
        {
            var fs=new FileStream(path,FileMode.Create);
            var serializer = new XmlSerializer(typeof(dataset));
            serializer.Serialize(fs, data);
        }
    }
}


// メモ: 生成されたコードは、少なくとも .NET Framework 4.5または .NET Core/Standard 2.0 が必要な可能性があります。
/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class dataset
{

    private string nameField;

    private object commentField;

    private datasetImage[] imagesField;

    /// <remarks/>
    public string name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public object comment
    {
        get
        {
            return this.commentField;
        }
        set
        {
            this.commentField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("image", IsNullable = false)]
    public datasetImage[] images
    {
        get
        {
            return this.imagesField;
        }
        set
        {
            this.imagesField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class datasetImage
{

    private datasetImageBox boxField;

    private string fileField;

    /// <remarks/>
    public datasetImageBox box
    {
        get
        {
            return this.boxField;
        }
        set
        {
            this.boxField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string file
    {
        get
        {
            return this.fileField;
        }
        set
        {
            this.fileField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class datasetImageBox
{

    private datasetImageBoxPart[] partField;

    private int topField;

    private int leftField;

    private int widthField;

    private int heightField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("part")]
    public datasetImageBoxPart[] part
    {
        get
        {
            return this.partField;
        }
        set
        {
            this.partField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int top
    {
        get
        {
            return this.topField;
        }
        set
        {
            this.topField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int left
    {
        get
        {
            return this.leftField;
        }
        set
        {
            this.leftField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int width
    {
        get
        {
            return this.widthField;
        }
        set
        {
            this.widthField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int height
    {
        get
        {
            return this.heightField;
        }
        set
        {
            this.heightField = value;
        }
    }
}

/// <remarks/>
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class datasetImageBoxPart
{

    private byte nameField;

    private int xField;

    private int yField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte name
    {
        get
        {
            return this.nameField;
        }
        set
        {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int x
    {
        get
        {
            return this.xField;
        }
        set
        {
            this.xField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int y
    {
        get
        {
            return this.yField;
        }
        set
        {
            this.yField = value;
        }
    }
}


