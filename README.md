# Static-Blog-Generator
정적 블로그 생성기 : 개발 중지

## NuGet Package
-   DotLiquid
-   Markdig
-   Newtonsoft.Json

## Directory Structure
Public 폴더의 하위 구조

```
Public
├── _includes
├── _pages
│   └── _index.md
├── _posts
├── _templates
│   ├── _layout.html
│   ├── _page.md
│   └── _post.md
└── _config.json
```

## Static Site Generator Structure

```
.
├── Models
│   ├── TemplateModel.cs
│   └── TemplateVariablesModel.cs
├── Public
├── StaticModels
│   ├── _config.json
│   ├── _layout.html
│   ├── _page.md
│   └── _post.md
├── Generator.cs
├── Manager.cs
├── PageManager.cs
├── PostManager.cs
├── Program.cs
└── TemplateManager.cs
```

#### Models
-   MarkdwonPostModel.cs
    Markdown 파일에 대한 클래스 모델로 해당 파일의 HTML 변환등을 담당한다.

#### Public
정적 사이트 생성기에 의해 관리되는 파일들의 폴더로 Directory Structure를 따르고있다.


#### StaticModels
Public 하위에 생성되는 정적파일로, 정적 사이트 생성기에 의해 관리되는 필수 파일들이다.  
예) `_layout.html`, `_config.json`


#### TemplateManager.cs
/Public/_templates/를 읽어들여 Template에 관련된 처리를 담당하며, `Liquid`를 이곳에서 처리한다.


#### PostManager.cs
/Public/_posts/를 읽어들여 post 목록을 html로 변환한 구조로 생성한다.


#### Generator.cs
