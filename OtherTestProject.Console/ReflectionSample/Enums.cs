using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OtherTestProject.Console.ReflectionSample
{
    /// <summary>
    /// 淘宝订单状态
    /// </summary>
    enum TBOrderStatus
    {
        /// <summary>
        ///没有创建支付宝交易
        /// </summary>
        TRADE_NO_CREATE_PAY,
        /// <summary>
        /// 等待买家付款 
        /// </summary>
        WAIT_BUYER_PAY,
        /// <summary>
        ///等待卖家发货,即:买家已付款 
        /// </summary>
        WAIT_SELLER_SEND_GOODS,
        /// <summary>
        /// 等待买家确认收货,即:卖家已发货
        /// </summary>
        WAIT_BUYER_CONFIRM_GOODS,
        /// <summary>
        /// 买家已签收,货到付款专用
        /// </summary>
        TRADE_BUYER_SIGNED,
        /// <summary>
        ///交易成功 
        /// </summary>
        TRADE_FINISHED,
        /// <summary>
        ///交易关闭 
        /// </summary>
        TRADE_CLOSED,
        /// <summary>
        ///交易被淘宝关闭 
        /// </summary>
        TRADE_CLOSED_BY_TAOBAO
    }
    /// <summary>
    /// 淘宝订单退货状态
    /// </summary>
    enum TBRefundStatus
    {
        /// <summary>
        /// 买家已经申请退款，等待卖家同意
        /// </summary>
        WAIT_SELLER_AGREE,
        /// <summary>
        ///卖家已经同意退款，等待买家退货 
        /// </summary>
        WAIT_BUYER_RETURN_GOODS,
        /// <summary>
        /// 买家已经退货，等待卖家确认收货
        /// </summary>
        WAIT_SELLER_CONFIRM_GOODS,
        /// <summary>
        ///  卖家拒绝退款
        /// </summary>
        SELLER_REFUSE_BUYER,
        /// <summary>
        /// 退款关闭
        /// </summary>
        CLOSED,
        /// <summary>
        /// 退款成功
        /// </summary>
        SUCCESS
    }
    /// <summary>
    /// 淘宝订单货物状态
    /// </summary>
    enum GoodStatus
    {
        /// <summary>
        /// 买家未收到货
        /// </summary>
        BUYER_NOT_RECEIVED,
        /// <summary>
        ///买家已收到货 
        /// </summary>
        BUYER_RECEIVED,
        /// <summary>
        ///买家已退货 
        /// </summary>
        BUYER_RETURNED_GOODS,
    }
    /// <summary>
    /// 淘宝异常类型
    /// </summary>
    enum TBErrorType
    {
        /// <summary>
        ///  更新本地ERP数据失败！
        /// </summary>
        ERR_UPD_ERP,
        /// <summary>
        /// ERP淘宝基本设置失败
        /// </summary>
        ERR_SYS_SET,
        /// <summary>
        /// 连接API错误
        /// </summary>
        ERR_API_WS,
        /// <summary>
        /// 取数据错误
        /// </summary>
        ERR_GET_TB_DATA,
        /// <summary>
        /// 执行ACTION更新淘宝异常
        /// </summary>
        ERR_UPD_TB,
        /// <summary>
        /// 特定异常之外的更新ERP数据异常
        /// </summary>
        ERR_OTHER_ERP,
        /// <summary>
        /// 淘宝商家检查失败
        /// </summary>
        ERR_USR_CHECK,
        /// <summary>
        /// 执行ACTION前判断异常
        /// </summary>
        ERR_CHECK_BEFORE_ACTION
    }

    enum TBActionType
    {
        /// <summary>
        /// 产生报价单
        /// </summary>
        ACT_GET_QC,
        /// <summary>
        /// 产生受订单
        /// </summary>
        ACT_GET_SO,
        /// <summary>
        /// 产生单客户收款单
        /// </summary>
        ACT_GET_RP,
        /// <summary>
        /// 关闭交易
        /// </summary>
        ACT_QC_CLOSE,
        /// <summary>
        /// 更新发货信息
        /// </summary>
        ACT_SA_KD,
        /// <summary>
        /// 延长确认收货时间
        /// </summary>
        ACT_ADD_DAY,
        /// <summary>
        /// 卖家对买家的评价
        /// </summary>
        ACT_SO_EVAL1,
        /// <summary>
        /// 产生退货申请单
        /// </summary>
        ACT_GET_YX,
        /// <summary>
        /// 发表留言
        /// </summary>
        ACT_YX_MSG
    }
}
