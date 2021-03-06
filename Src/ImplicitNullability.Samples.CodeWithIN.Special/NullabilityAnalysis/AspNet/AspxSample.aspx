﻿<%@ Page Language="C#" %>
<%@ Import Namespace="ImplicitNullability.Samples.CodeWithIN.Special" %>
<%@ Import Namespace="JetBrains.Annotations" %>

<script runat="server">

    [CanBeNull]
    private readonly string _nullString = null;

    private readonly int? _nullableInt = null;

    private readonly string _field = null /*Expect:AssignNullToNotNullAttribute[Flds]*/;

    private string SomeMethod(string a)
    {
        ReSharper.TestValueAnalysis(a, a == null /*Expect:ConditionIsAlwaysTrueOrFalse[MIn]*/);
        return "";
    }

    [CanBeNull]
    private string SomeNullableMethod([CanBeNull] string a, int? b, string optional = null)
    {
        ReSharper.SuppressUnusedWarning(a, b, optional);
        return null;
    }

    private string SomeFunction(out string outParam)
    {
        outParam = null /*Expect:AssignNullToNotNullAttribute[MOut]*/;
        return null /*Expect:AssignNullToNotNullAttribute[MOut]*/;
    }

    public void Consume()
    {
        ReSharper.TestValueAnalysis(_field, _field == null /*Expect:ConditionIsAlwaysTrueOrFalse[Flds]*/);
    }

</script>

<!DOCTYPE html>
<html>
<head>
    <title></title>
</head>
<body>
<%-- Render method --%>
<%= null %>
<%= _nullString %>
<%= _nullableInt %>

<%= SomeMethod(null /*Expect:AssignNullToNotNullAttribute[MIn]*/) %>
<%= SomeNullableMethod(null, null, optional: null) %>
<% // ReSharper disable once NotAccessedVariable
    string outParam;%><%= SomeFunction(out outParam) %>

<%# null %>

<%-- The "Bind" elements are treated as "external code" because they have no declaration (they are represented by the special
     'AspBindMethod: AspImplicitTypeMember' PSI element). Atm it's OK to avoid making an exemption here. --%>
<%# Bind(null /*Expect:AssignNullToNotNullAttribute[MIn && InclGenCode]*/) %>
<%# Bind(null /*Expect:AssignNullToNotNullAttribute[MIn && InclGenCode]*/,
        null /*Expect:AssignNullToNotNullAttribute[MIn && InclGenCode]*/) %>

<%-- Note that the following warnings need external annotations for TemplateControl.Eval() --%>
<%# Eval(null /*Expect:AssignNullToNotNullAttribute*/) %>
<%# Eval(null /*Expect:AssignNullToNotNullAttribute*/, null /*Expect:AssignNullToNotNullAttribute*/) %>
</body>
</html>
