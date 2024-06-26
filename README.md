주소와 기능
-
- ~~http://43.201.115.13/~~ 게시판 링크입니다
  - 로그인, 회원가입, 로그아웃, 게시글 생성, 댓글 생성/삭제
- ~~http://43.201.115.13:5002/~~ 관리자 페이지 링크입니다.
  - 유저 리스트 조회, 유저 정지, 게시판 관리
<br/>

- 아래는 일자별로 작업한 내용들입니다.
- 기능이 많이 구현되거나 복잡한 기능이 들어가진 않았습니다. C# 문법과 .NET, MSSQL의 기초적인 구조와 통신방식을 익히게 된 프로젝트입니다. 

2024/04/17
-
- Admin 게시판 관리 추가, 삭제 기능 완료

2024/04/16
-
- Admin 게시판 관리 수정 기능 완료

2024/04/15
-
- Admin 게시판 관리 페이지 추가와 수정 기능을 작업하였다.

2024/04/12
-
- 기존 게시판을 Action에 1:1매칭 되도록 작성하였는데, Admin 쪽에서 게시판 관리를 통해 유동적으로 다루려면 적합한 방식이 아니어서 구조를  Parameter로 게시판 ID를 받아 해당 게시판을 출력하는 방식으로 바꾸었다.
- Admin 페이지에 게시판 관리를 만들기 위해 게시판 목록을 보여주는 View 추가

2024/04/09
-
- Admin 유저가 작성한 글, 댓글을 조회하는 페이지를 Partial View로 변경하였다.
- Admin 쪽에 유저 정지 기능 추가, 정지된 유저가 로그인 할 시 문구가 뜨도록 변경해줌.

2024/04/05
-
- 유저에 Role과 권한을 부여하기 위해 Role부터 추가해주었다.

2024/04/04
-
- 비밀번호를 평문으로 저장하던 것을 salting, 단방향 encryption을 통해 암호화된 문자열로 저장하는 것으로 바꾸었고, 이에 따른 기존 User Verification을 변경하였다.

2024/04/03
-
- 기존에 만들고 있던 Admin 사이트들의 form들은 ajax form으로 바꾸는 작업을 시작하였다.
- Pagination과 검색창을 공통으로 사용하기 위해 Partial View로 바꾸었는데 상당히 고생을 많이했다.
  - 공통으로 사용할 모델 class를 구현하였다
  - SelectListItem을 전달하려고 했는데 form으로 전달되는 데이터다 보니 object가 전달될 수 없었다. 결국 JsonSerializer를 통해 serialize, deserialize를 거쳤다.
- 회원가입 기능을 구현했다.
	 
2024/03/31
-
- Ajax Form을 사용해 보기 위해 일단 댓글 부분을 수정해 보았다.
- Ajax Form을 Admin 사이트에 적용시켜보기 위해 유저 리스트를 PartialView로 분리하였다. 이후 페이지네이션, 검색창도 PartialView로 분리하였다.
  - PartialView로 다 분리를 시키다보니 각 PartialView에 따른 데이터 변경을 조작하려다 보니 상당히 많이 헤매었었다. 결국 Form element를 가져와 해당하는 값을 변경시키는 것으로 마무리 되었다.
  - 검색창 분리를 할때 Class instance를 넘기려 했는데 Form으로 전송되어 정상적으로 데이터를 주고받을 수 없었다. React에서는 컴포넌트간에 어떠한 데이터도 자유롭게 주고받고, 전역 state 관리도 가능했기 때문에 간단하게 생각을 했었다. c# 환경에서 ajax form과 partial view를 사용하기 때문에 제약이 생긴것으로 보인다.

2024/03/30
-
- Admin 사이트 DbContext대신 동적쿼리를 사용해 분기처리하여 검색기능 추가.
- 유저가 작성한 글/댓글 조회할 수 있도록 변경

2024/03/29
-
- Admin 사이트 작성 중.
  - User 관리 페이지 작성 중 유저 리스트 검색을 하려 하는데 SP에서 분기를 나누어 하면 결과를 받을 수 있으나 SP가 길어져 다른 방식으로 DbContext를 사용하는게 있는것으로 보아 시도를 해보려 했다. 
	 - DbContext 파일에서 DB Connection string을 appsettings.json에서 받아와서 사용하려 했으나 DI 문제가 있어 connection string을 raw하게 쓰는 방법으로 돌아왔음.  

2024/03/28 
-
- user role을 추가
  - Identity package 를 통해 user role을 다룰 수 있다고 하였으나 Table 구조나 Package에 필요한 Razor Page 있는것으로 보여 설치 후 제거
  - JWT 토큰을 생성할 때 Claim에 Role을 추가할 수 있는것을 보아 User <> Role 테이블을 만들어 Role 관리를 하면 될 것으로 보임

2024/03/27 
-
- 댓글 삭제 기능 추가, 삭제된 댓글 노출 처리

2024/03/25 
-
- SQL 파일 프로젝트에 추가.
- Board 부분 라우팅 수정
  - [Route] 사용 제거 후 기본 Route 규칙 따르도록 변경

2024/03/23
-
- 댓글 생성 기능 추가.
- 3번째 서버 배포
  - 한글이 ???로 깨지는 현상이 있었다. DB Collation을 변경해주니 해결되었다.
  - 로그인에서 에러가 일어났다. stored procedure에서 select 문에서 id를 가져올때 [id]로 감싸주어 해결하였다.

2024/03/21
-
- 댓글 관련 기능을 시작하려 하니 기존에 PostDao에서 게시물 데이터를 받아올 때 댓글 정보도 같이 받아오고 있었음. 댓글을 받아오는 기능을 CommentDao로 분리하여 Service에서 양쪽에서 게시글, 댓글을 따로 받아오도록 바꿈.
- Route에 대한 깨닳음(?)이 생겨서 전체적으로 라우팅을 손봄. 개발하는 도중 자꾸 AmbiguousRoute? 같은 에러가 자꾸 났지만 이것저것 해보다가 자꾸 뚫려서 Route 관련하여 생각이 잘못 잡히고 있었음. Action에 [Route] Attribute를 걸어주는 것과 Program.cs에서 사용하는 MapControllerRoute를 혼용하고 있었어서 문제가 있었던걸로 생각됨.

2024/03/18
-
- 게시글 수정, 삭제 기능 구현

2024/03/15
-
- JWT Authorize 통과
  - Token을 쿠키에만 저장해두고 Authorize를 통과하지 않아 한참동안 헤멤. 익숙하게 써오던 것인데 API처럼 눈에 시각적으로 보이는게 없어서 계속 방황함. 미들웨어에서 Cookie에 있는 토큰을 Request Header에 넣어주어 Authorize 통과하도록 만듬
- 2차 서버 배포
  - 프로젝트 빌드 후 프로젝트 폴더 내에 bin 폴더에 빌드 결과물이 생성된다. 그 안의 dll 파일을 실행시키면 서버가 작동되게 되는데 여기서 계속 착오를 겪게 되었었다. JWT와 관련한 문제엿는데 appsettings.json파일을 사용하며 문제가 일어났었다. Program.cs 파일에서 builder.Configuration.GetValue<string>("Jwt:Issuer") 이런식으로 설정파일에서 GetValue를 통해 값을 얻어오게 만드는 것이었는데 local에서는 잘 작동되던것이 서버에서는 계속해서 빈 값이 나오기 시작했다.. nginx도 건드려보고 build 설정의 문제가 있는것인가 이것저것 건드려보다가 몇시간의 착오 끝에 dll을 실행시키는 path에 따라 해당 설정 파일을 읽을 수 있냐 없냐가 정해지는 것이었음을 알게 되었다. 서버 실행 스크립트에서 기존에 linux user의 root에서 서버를 실행하던 것을 dll파일이 있는 경로까지 찾아가서 서버를 실행하도록 변경하였다.

2024/03/13
-
- JWT 토큰을 사용한 로그인 구현

2024/03/12
-
- 게시판 리스트 페이지네이션, 컨트롤러/모델 파라미터 class로 변경, 메인페이지 UI 변경

2024/03/11
-
- 프로젝트 웹배포 (AWS 사용)
  - EC2 이미지를 처음에 못보고 Amazon Linux를 설치하였다가 .NET SDK를 설치하지 못하여 RHEL로 바꾸었음.
  - 메모리가 1GB밖에 되지 않아 SQL 설치도 불가하여, RDS를 사용하여 EC2와 붙였음. (나중에 서버를 만지다가 든 생각인데 가상메모리를 늘려줬으면 설치가 가능했을까 생각이 든다)
  - SQL 관리 프로그램으로 ISQL 설치 (Windows의 SSMS에서 쓰던 문법이 ISQL쪽에서는 문제를 일으키는게 많았다.. 주석이나 지원이 안되는 문법이 몇개 있었다.)
  - 인증서 발급 프로그램에서 최소요구사항으로 python3.6을 요구했어서 pyenv를 통해 python3.6 설치함.
  - .NET 로컬 서버(Kestrel)를 띄워놓고 Nginx 리버스 프록시를 사용하여 프로젝트에 접근할 수 있도록 만듬.
  - 추후 도메인 연결하여 SSL/TLS 적용하여야 함.

2024/03/07
-
- 게시판을 여러개로 나누게 되면서 코드를 공통적으로 사용할 수 있기에 refactoring 진행
  - 단순히 게시판 하나를 만들었다가 게시판을 여러개로 늘리면서 공통적으로 코드를 사용할 수 있도록 변경함.
  - MapControllerRoute와 a태그 attribute인 asp-action, asp-controller, asp-route에 대한 이해를 조금이나마 하게 됨.

2024/03/06
-
- stored procedure 결과값을 읽어오는 방식 변경 SqlDataReader -> SqlDataAdapter
  - SqlDataReader는 while 문을 사용하여 read()함수가 끝날때까지 진행을 시킨다. 보기에 뭔가 심적으로 불편하다.. 다른 방식을 찾아낸 것이 SqlDataAdapter인데 DataSet/DataTable/DataRow 에 값을 저장하여 Select문을 통해 (아마 LINQ처럼..?) 데이터를 저장한다.

2024/03/04
-
- 새로운 게시글 작성 기능을 만들어가며 Form의 동작 방식과 모델 Validation 기능에 대한 이해를 함
