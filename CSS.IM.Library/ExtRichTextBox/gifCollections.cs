using System;
using System.Collections.Generic;
using System.Text;

namespace CSS.IM.Library 
{
    #region ��ǰ�û�������ӵ�ͼƬ������ClassGifs
    public class gifCollections : System.Collections.CollectionBase, ICloneable ,IDisposable
    {
        public   gifCollections()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
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
 
