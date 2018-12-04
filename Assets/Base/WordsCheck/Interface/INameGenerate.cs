using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordsCheck
{
    public interface INameGenerate
    {
        string GenerateName(); // 生成未经检测的名字
        string GenerateNameValid();// 生成符合检测标准的名字

    }
}
