2024/03/18
- 게시글 수정, 삭제 기능 구현

2024/03/15
- JWT Authorize 통과
  - Token을 쿠키에만 저장해두고 Authorize를 통과하지 않아 한참동안 헤멤. 익숙하게 써오던 것인데 API처럼 눈에 시각적으로 보이는게 없어서 계속 방황함. 미들웨어에서 Cookie에 있는 토큰을 Request Header에 넣어주어 Authorize 통과하도록 만듬 

2024/03/13
- JWT 토큰을 사용한 로그인 구현

2024/03/12
- 게시판 리스트 페이지네이션, 컨트롤러/모델 파라미터 class로 변경, 메인페이지 UI 변경

2024/03/11
- 프로젝트 웹배포 (AWS 사용)
  - EC2 이미지를 처음에 못보고 Amazon Linux를 설치하였다가 .NET SDK를 설치하지 못하여 RHEL로 바꾸었음.
  - 메모리가 1GB밖에 되지 않아 SQL 설치도 불가하여, RDS를 사용하여 EC2와 붙였음. (나중에 서버를 만지다가 든 생각인데 가상메모리를 늘려줬으면 설치가 가능했을까 생각이 든다)
  - SQL 관리 프로그램으로 ISQL 설치 (Windows의 SSMS에서 쓰던 문법이 ISQL쪽에서는 문제를 일으키는게 많았다.. 주석이나 지원이 안되는 문법이 몇개 있었다.)
  - 인증서 발급 프로그램에서 최소요구사항으로 python3.6을 요구했어서 pyenv를 통해 python3.6 설치함.
  - .NET 로컬 서버(Kestrel)를 띄워놓고 Nginx 리버스 프록시를 사용하여 프로젝트에 접근할 수 있도록 만듬.
  - 추후 도메인 연결하여 SSL/TLS 적용하여야 함.

2024/03/07
- 게시판을 여러개로 나누게 되면서 코드를 공통적으로 사용할 수 있기에 refactoring 진행
  - 단순히 게시판 하나를 만들었다가 게시판을 여러개로 늘리면서 공통적으로 코드를 사용할 수 있도록 변경함.
  - MapControllerRoute와 a태그 attribute인 asp-action, asp-controller, asp-route에 대한 이해를 조금이나마 하게 됨.

2024/03/06
- stored procedure 결과값을 읽어오는 방식 변경 SqlDataReader -> SqlDataAdapter
  - SqlDataReader는 while 문을 사용하여 read()함수가 끝날때까지 진행을 시킨다. 보기에 뭔가 심적으로 불편하다.. 다른 방식을 찾아낸 것이 SqlDataAdapter인데 DataSet/DataTable/DataRow 에 값을 저장하여 Select문을 통해 (아마 LINQ처럼..?) 데이터를 저장한다.

2024/03/04
- 새로운 게시글 작성 기능을 만들어가며 Form의 동작 방식과 모델 Validation 기능에 대한 이해를 함
