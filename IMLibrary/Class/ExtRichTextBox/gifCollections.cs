using System;
using System.Collections.Generic;
using System.Text;

namespace IMLibrary 
{
    #region ��ǰ�û�������ӵ�ͼƬ������ClassGifs
    public  class gifCollections : System.Collections.CollectionBase
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
 
