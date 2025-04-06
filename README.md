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

