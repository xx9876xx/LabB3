$(document).ready(function () {


    
});


//確認鈕事件
function SubmitClick() {
    let url = '/RiskEvaluation/EvaluationRank';    //新網址
    $("#questForm").attr('method', "post");    //設定
    $("#questForm").attr('action', url);
    $("#questForm").submit();    //轉址
    return true;
}