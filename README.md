# C#-sdk
接入优盾钱包集成的C#版SDK

一、说明

1.所有请求接口为http-post接口，传参形式为body形式

2.所有请求接口的参数首先Json序列化为body，根据SignUtil.sign方法进行签名，最终转成4参  (body,sign,timestamp,nonce)，再将四参Json序列化为reqBody，使用reqBody为真实参数进行发送

3.回调接口使用多参，其中body为Json序列化

4.Udun.Api为WebService版Demo，相关业务参数在Web.config种

​      CallbackController.asmx为回调Demo

​      RequestController.asmx为请求Demo

5.Udun.FormDemo.Api为Winform版Demo，相关业务参数在app.config中

config配置参数含义

CallBackUrl ---- 回调地址

MerchantId ---- 商户号

MerchantKey ---- 商户接入Key

Gateway ---- 商户服务host

二、接口

1.申请生成地址

接口 /mch/address/create

| 参数       | 含义     | 值类型 | 说明                                            |
| ---------- | -------- | ------ | ----------------------------------------------- |
| meichantId | 商户号   | string |                                                 |
| coinType   | 币种     | string | 代币使用主币coinType，如USDT-OMNI，使用BTC（0） |
| callUrl    | 回调地址 | string | 用于充币、提币等业务回调使用                    |
| alias      | 别名     | string |                                                 |
| walletId   | 钱包Id   | string | 优盾钱包创建的钱包编号                          |

返回结构ResponseMessage<Address> ，若code = 200，Address不为空

2.提币申请

接口 /mch/withdraw

类为Trade，类结构如下

| 参数         | 含义     | 值类型 | 说明                                                   |
| ------------ | -------- | ------ | ------------------------------------------------------ |
| meichantId   | 商户号   | string |                                                        |
| mainCoinType | 主币种   | string |                                                        |
| coinType     | 币种     | string |                                                        |
| callUrl      | 回调地址 | string | 用于充币、提币等业务回调使用                           |
| amount       | 金额     | string | 实际为BigDecimal，由于C#无BigDecimal类型，无法序列化。 |
| businessId   | 业务编号 | string | 由接入方生成                                           |
| memo         | 业务标识 | string | EOS等系列币的转账业务标识                              |

返回结构为ResponseMessage<string>，若code = 200，则为成功

3.申请代付

接口 /mch/withdraw/proxypay

入参、出参 同 2（提币申请）

4.检测地址是否合法

接口/mch/check/address



| 参数         | 含义   | 值类型 | 说明 |
| ------------ | ------ | ------ | ---- |
| meichantId   | 商户号 | string |      |
| mainCoinType | 主币种 | string |      |
| address      | 地址   | string |      |

返回结构为ResponseMessage<string>，若code = 200，则为成功

5.获取支持币种

接口/mch/support-coins

| 参数        | 含义         | 值类型 | 说明 |
| ----------- | ------------ | ------ | ---- |
| meichantId  | 商户号       | string |      |
| showBalance | 是否显示资金 | bool   |      |

返回结构为ResponseMessage<List<SupportCoin>>，SupportCoin

三、回调接口

1、需由接入方提供，使用

| 参数      | 含义       | 值类型 | 说明           |
| --------- | ---------- | ------ | -------------- |
| timestamp | 毫秒时间戳 | string |                |
| nonce     | 随机值     | string |                |
| sign      | 币种       | string |                |
| body      | 回调结构   | string | 提币等业务回调 |

body结构为类Trade，类结构同 2 