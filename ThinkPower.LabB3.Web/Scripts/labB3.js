$(document).ready(function () {


    
});

//問卷檢核
function validate() {
    //必填檢核 檢核通過:true , 失敗:false
    let check = true;
    let HaveOtherAnswerCheck = true;
    //重置所有檢核欄位
    $(".validate-text").each(function () {
        $(this).text("");
        $(this).hide();
        $(this).css("display", "none");
    });
    HaveOtherAnswerCheck = HaveOtherAnswer();
    //每個問題都執行的檢核
    $(".question").each(function () {
        //題號
        let questionId = $(this).data("question-id");
        //非必填條件
        let allowNaCondition = ConditionCheck($(this).data("allow-na-condition"));
        //複選變單選條件
        let singleAnswerCondition = ConditionCheck($(this).data("single-answer-condition"));

        //必填 且 非必填條件不存在
        if ($(this).data("need-answer") == "Y" && !allowNaCondition)
        {
            //填充題檢核
            if ($(this).data("answer-type") == "F") {
                check = false;
                if ($(this).find(":text").each(function () {
                    if ($(this).val() != "") {
                        check = true;
                    }
                }));
                addValidateText($(this), check, "required");
            }

            //單選題檢核
            if ($(this).data("answer-type") == "S") {
                check = false;
                if ($(this).find(":radio").each(function () {
                    if ($(this)[0].checked) {
                        check = true;
                    }
                }));
                addValidateText($(this), check, "required");
            }

            //複選題檢核
            if ($(this).data("answer-type") == "M") {                
                let minAns = parseInt($(this).data("min-multiple-answers"));
                let maxAns = parseInt($(this).data("max-multiple-answers"));
                let count = 0;
                check = false;

                //有多少複選選項被勾選
                if ($(this).find(":checkbox").each(function () {
                    if ($(this)[0].checked) {
                        count++;
                    }
                }));
                
                //當max-multiple-answers不存在時，給最大值
                if (isNaN(maxAns)) {
                    maxAns = parseInt($(this).find(":checkbox").length);
                }
                //當單選條件發生
                if (singleAnswerCondition && count > 1) {
                    check = false;
                    addValidateText($(this), check, "single");
                    
                }
                //判斷是否再最大選項數與最小選項數之間
                if (count >= minAns && count <= maxAns) {
                    check = true;
                } else if (count < minAns) {
                    check = false;
                    addValidateText($(this), check, "min-multiple", minAns);
                } else if (count > maxAns) {
                    check = false;
                    addValidateText($(this), check, "max-multiple", maxAns);
                }

            }
        }
        
    });

    return (check && HaveOtherAnswerCheck);
    
}

//添加必填警示文字
function addValidateText(ele,check,type,n)
{
    let message;
    switch (type)
    {
        case "required":
            message = "*此題必須填答！";
            break;
            
        case "min-multiple":
            message = "*此題至少須勾選" + n + "個項目!";
            break;

        case "max-multiple":
            message = "*此題至多僅能勾選" + n + "個項目!";
            break;

        case "single":
            message = "*此題僅能勾選1個項目!";
            break;

        case "other":
            message = "*請輸入其他說明文字!";
            break;
    }
    
    if (!check) {
        let validationId = "#" + ele.data("question-id") + "-validation";
        $(validationId).css("display", "inline");
        $(validationId).append("<br/>"+message);
        $(validationId).show();
    }
}

//特殊條件檢核
function ConditionCheck(json) {
    if (json == "" || json == null || json == undefined)
    {
        return false;
    }

    let conditions = json["Conditions"];
    let check = false;
    let questionId, answerCodes;
    for (let condition of conditions) {
        questionId = condition.QuestionId;
        answerCodes = condition.AnswerCode;
        for (let answerCode of answerCodes)
        {            
            if ($("input[name='" + questionId + "'][value='" + answerCode +"']:checked").val())
            {
                console.log("input[name='" + questionId + "'][value='" + answerCode + "']:checked");
                console.log($("input[name='" + questionId + "'][value='" + answerCode + "']:checked").val());
                check = true;
            }
        }
    }

    return check;
}

//其他說明若勾選,填寫欄必填檢核
function HaveOtherAnswer() {
    //每個問題都執行的檢核
    $(".question").each(function () {
        //題號
        let questionId = $(this).data("question-id");
        //其他說明必填檢核
        let check = true;

        //單選或多選檢核
        if ($(this).data("answer-type") == "S" || $(this).data("answer-type") == "M") {
            check = false;
            if ($(this).find(":radio").each(function () {
                if ($(this)[0].checked) {
                    check = true;
                    if ($(this).data("have-other-answer") == "Y") {
                        if ($(":text[name='" + questionId + "']").data("need-other-answer") == "Y") {
                            if ($(":text[name='" + questionId + "']").val() == "") {
                                check = false;
                            }
                        }
                    }
                }
            }));
            if (!check) {
                addValidateText($(this), check, "other");
            }

        }
        return check;
    });
    
}


//確認鈕事件
function SubmitClick() {
    let url = '/RiskEvaluation/EvaluationRank';    //新網址
    $("#questForm").attr('method', "post");    //設定
    $("#questForm").attr('action', url);
    $("#questForm").submit();    //轉址
    return true;
}