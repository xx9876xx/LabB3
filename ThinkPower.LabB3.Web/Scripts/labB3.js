$(document).ready(function () {

    AnswerListAdjustment();
    $("#submit-button").bind("click", SubmitClick);
    
});

/**確認事件紐 */
function SubmitClick() {

    if (validate()) {
        let url = '/RiskEvaluation/EvaluationRank';    //新網址
        $("#questForm").attr('method', "post");    //設定
        $("#questForm").attr('action', url);
        $("#questForm").submit();    //轉址
    }
}

//問卷檢核
function validate() {

    //基本必填檢核 檢核通過:true , 失敗:false
    let basicValidateCheck = true;

    //其他說明必填檢核 檢核通過:true , 失敗:false
    let haveOtherAnswerCheck = true;

    //重置所有檢核欄位
    $(".validate-text").each(function () {
        $(this).text("");
        $(this).hide();
        $(this).css("display", "none");
    });

    basicValidateCheck = basicValidate();
    haveOtherAnswerCheck = HaveOtherAnswer();

    //計算所有檢核結果並傳
    return (basicValidateCheck && haveOtherAnswerCheck);
}

//必填檢核(填充、單選、複選及特殊檢核)
function basicValidate() {

    let check = true;

    //每個問題都執行的檢核
    $(".question").each(function () {

        //題號
        let questionId = $(this).data("question-id");
        //非必填條件
        let allowNaCondition = ConditionCheck($(this).data("allow-na-condition"));
        //複選變單選條件
        let singleAnswerCondition = ConditionCheck($(this).data("single-answer-condition"));
        //必填 且 非必填條件不存在
        if ($(this).data("need-answer") == "Y" && !allowNaCondition) {
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

                //有多少複選選項被勾選，計算count值
                if ($(this).find(":checkbox").each(function () {
                    if ($(this)[0].checked) {
                        count++;
                    }
                }));

                //當max-multiple-answers為NaN時，給最大值
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
    return check;
}

//其他說明若勾選,填寫欄必填檢核
function HaveOtherAnswer() {

    let check = true;
    //每個問題都執行的檢核
    $(".question").each(function () {
        //題號
        let questionId = $(this).data("question-id");
        //單選或多選檢核
        if ($(this).data("answer-type") == "S" || $(this).data("answer-type") == "M") {
            if ($(this).find(":radio,:checkbox").each(function () {
                if ($(this)[0].checked) {
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
    });
    return check;
}

/**
 * 特殊條件檢核(複選變單選條件、非必填關聯條件)
 * @param {String} conditionString :條件Json字串
 */
function ConditionCheck(conditionString) {
    if (conditionString == "" || conditionString === null || conditionString === undefined)
    {
        return false;
    }
    //json Parse是否為正確字串
    //做物件轉型
    let conditions = conditionString["Conditions"];
    let check = false;
    let questionId = "";
    let answerCodes = "";
    for (let condition of conditions) {
        questionId = condition.QuestionId;
        answerCodes = condition.AnswerCode;
        for (let answerCode of answerCodes)
        {            
            if ($("input[name='" + questionId + "'][value='" + answerCode + "']:checked").val()) {
                check = true;
            }
            else
            {
                check = false;
                break;
            }
        }
    }

    return check;
}

/**
 * 添加必填警示文字
 * @param {Object} ele:題目<div>DOM物件
 * @param {Boolean} check:檢核是否通過
 * @param {String} type:"required","min-multiple","max-multiple","single","other"
 * @param {Number} n:複選條件的最大或最小值
 */
function addValidateText(ele, check, type, n) {
    let message;
    switch (type) {
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
        $(validationId).append(message);
        $(validationId).show();
    }
}

/**調整選項換行 */
function AnswerListAdjustment() {

    const width = $(".question:first ul").width();
    let space = 0;

    $(".answer ul").each(function () {
        space = width;
        $(this).find("li").each(function () {
            space -= $(this).width();
            if (space < 0) {
                $(this).css("display", "block");
                space = width;
            }
        });
    });
}