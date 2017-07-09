using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tdf.CSharp.Helper.KetamaHashHelp
{
    public class KetamaNodeLocator
    {
        private readonly SortedList<long, string> _ketamaNodes;
        // private HashAlgorithm hashAlg;

        public KetamaNodeLocator(List<string> nodes, int nodeCopies)
        {
            _ketamaNodes = new SortedList<long, string>();

            // 对所有节点，生成nCopies个虚拟结点
            foreach (var node in nodes)
            {
                // 每四个虚拟结点为一组
                for (var i = 0; i < nodeCopies / 4; i++)
                {
                    // getKeyForNode方法为这组虚拟结点得到惟一名称 
                    var digest = HashAlgorithm.ComputeMd5(node + i);
                    /* Md5是一个16字节长度的数组，将16字节的数组每四个字节一组，
                     * 分别对应一个虚拟结点，
                     * 这就是为什么上面把虚拟结点四个划分一组的原因
                     */
                    for (var h = 0; h < 4; h++)
                    {
                        var m = HashAlgorithm.Hash(digest, h);
                        _ketamaNodes[m] = node;
                    }
                }
            }
        }

        public string GetPrimary(string k)
        {
            var digest = HashAlgorithm.ComputeMd5(k);
            var rv = GetNodeForKey(HashAlgorithm.Hash(digest, 0));
            return rv;
        }

        protected virtual string GetNodeForKey(long hash)
        {
            // 改进版本，速度提升了10倍；
            var key = hash;
            var pos = 0;
            if (!_ketamaNodes.ContainsKey(key))
            {
                var low = 1;
                var high = _ketamaNodes.Count - 1;
                while (low <= high)
                {
                    var mid = (low + high) / 2;
                    if (key < _ketamaNodes.Keys[mid])
                    {
                        high = mid - 1;
                        pos = high;
                    }
                    else if (key > _ketamaNodes.Keys[mid])
                        low = mid + 1;
                }
            }

            var rv = _ketamaNodes.Values[pos + 1];
            return rv;

            /*
            var key = hash;
            // 如果找到这个节点，直接取节点，返回   
            if (!_ketamaNodes.ContainsKey(key))
            {
                // 得到大于当前key的那个子Map，然后从中取出第一个key，就是大于且离它最近的那个key 说明详见: http://www.javaeye.com/topic/684087
                var tailMap = from coll in _ketamaNodes
                    where coll.Key > hash
                    select new {coll.Key};

                if (tailMap == null || tailMap.Count() == 0)
                    key = _ketamaNodes.FirstOrDefault().Key;
                else
                    key = tailMap.FirstOrDefault().Key;
            }

            var rv = _ketamaNodes[key];
            return rv;
        }
        */



        }


    }
}
