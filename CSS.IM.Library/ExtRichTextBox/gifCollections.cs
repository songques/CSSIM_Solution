using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library 
{
    #region 当前用户自行添加的图片集合类ClassGifs
    public class gifCollections : System.Collections.CollectionBase, ICloneable ,IDisposable
    {
        public   gifCollections()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public MyPicture this[int index]
        {
            get
            {
                return ((MyPicture)InnerList[index]);
            }
        }

        public void add(CSS.IM.Library.MyPicture tempGif)
        {
            base.InnerList.Add(tempGif);
        }

        public void Romove(CSS.IM.Library.MyPicture tempGif)
        {
            base.InnerList.Remove(tempGif);
        }


        public object Clone()
        {
            gifCollections clone = new gifCollections();
            foreach (CSS.IM.Library.MyPicture item in base.InnerList)
            {
                clone.add(item);
            }
            return clone;
        }

        public void Dispose()
        {
            foreach (CSS.IM.Library.MyPicture item in base.InnerList)
            {
                item.Dispose();
            }
            System.GC.Collect();
        }
    }
    #endregion
}
 
