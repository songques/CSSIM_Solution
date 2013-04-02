using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary 
{
    #region 当前用户自行添加的图片集合类ClassGifs
    public  class gifCollections : System.Collections.CollectionBase
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

        public void add(IMLibrary.MyPicture tempGif)
        {
            base.InnerList.Add(tempGif);
        }

        public void Romove(IMLibrary.MyPicture tempGif)
        {
            base.InnerList.Remove(tempGif);
        }
    }
    #endregion
}
 
