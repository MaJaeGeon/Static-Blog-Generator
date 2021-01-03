+++
template : _post.md
title : FirstPost
date : 2021-01-03
+++

# Binding

@bind-value 라는 속성을 통해 html 태그에 C# 의 변수 또는 프로퍼티를  
연결하면 해당 html 태그는 C# 변수또는 프로퍼티와 연결된다.  
```cs
<input @bind-value="inputName" />

@code{
    string inputName = "";
}
```

C# 변수 또는 프로퍼티의 값이 변경되었지만 UI 상으로 반영되지  
않을 때는 StateHasChanged() 메서드를  
통해 UI 에 값이 변경되었다는것을 알려줄 수 있다.