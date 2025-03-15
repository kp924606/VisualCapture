using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCaptureApp.Interface
{
    /// <summary>
    /// 基於"道", 圖像通用行為,建立接口
    /// </summary>
    public interface IScreenshot
    {
        /// <summary>
        /// 開始執行
        /// </summary>
        void Start();

        /// <summary>
        /// 生命或個體被殲滅.
        /// </summary>
        void Annihilation();
    }
}
