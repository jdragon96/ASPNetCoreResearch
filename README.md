# 목차
0. 솔루션 셋업
1. 프로젝트 구성
2. 용어 정리
2.1. MVC

<br/>
<br/>

# 0. 솔루션 셋업
## 0.1.라이브러리 관리
- 한 프로젝트에서 초기로 다 설치
- 나머지 프로젝트는 [프로젝트 우클릭]-[프로젝트 파일 편집] 클릭 후 라이브러리 관련 내용 붙여넣기
```bash
// 설치
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.4
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.3
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.3

// 삭제 방법
dotnet remove package Microsoft.EntityFrameworkCore
```

- 그 외 방법으로 [솔루션 우클릭]-[솔루션용 Nuget 패키지 관리] 에 들어가 설치된 라이브러리에서 추가 가능

## 0.2. 데이터베이스 초기화
```bash
add-migration "Migration 이름"
update-database
```

<br/>
<br/>

# 1. 프로젝트 구성
| 프로젝트 이름 | 설명 | 비고 | 
|---|---|---|
| ECommerce | ASP.NET Core MVC 기반 프로젝트 | Model,View,Controller 폴더 구분 | 
| ECommerceRazor | ASP.NET Core WebApp 기반 프로젝트 | Pages 폴더에 통합 | 

<br/>
<br/>

-----

<br/>
<br/>

# 용어 정리
## 1. MVC
- Model, View, Controller의 약자
- Model 데이터 저장 역할을 수행
- View는 Model의 데이터를 활용해 화면 랜더링 역할을 수행
- Controller는 View와 Model을 연결해주는 비지니스 로직을 담당

## 2. cshtml

## 3. cshtml에서 화면 라우팅 설정하는법
```html
<!-- WebApp 방식 -->
<div asp-page="/컨트롤러이름/페이지이름" />
<!-- MVC 방식  -->
<div asp-controller="컨트롤러이름" asp-action="액션이름" />
```

## 4. ASP.NET Core 에러 핸들링 방식
1. 서버 사이드 에러 핸들링
    - 
2. 클라이언트 사이드 에러 핸들링
    - `@section` 구분 활용
    - Razor 뷰에서 JS 코드를 실행하는 공간을 의미
    - `_ValidationScriptsPartial`은 Razor에서 Form을 검증하기 위해 활용
    ```html
      @section Scripts
      {
        <partial name="_ValidationScriptsPartial" />
      }
    ```


# 1. ASP.NET Core 도서관 API 서버
## 1.1. Server 프로젝트 만들기
- appsettings.json에 DB Connection 정보 입력
```json
"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=1234;Database=TestDB;"
}
```
- Program.cs에 데이터베이스 의존성 추가
- 시작 프로젝트로 설정
``` C#
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
```

## 1.2. Model 프로젝트 만들기
- Library.Model 배쉬 연결
```bash
add-migration CreateModel
add-migration GenerateEnum
update-database
```

