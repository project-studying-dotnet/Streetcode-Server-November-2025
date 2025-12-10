∫
kD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Util\IdComparer.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Util 
{ 
public 

class 

IdComparer 
: 
IEqualityComparer /
</ 0
PartnerSourceLink0 A
>A B
{ 
public 
bool 
Equals 
( 
PartnerSourceLink ,
?, -
x. /
,/ 0
PartnerSourceLink1 B
?B C
yD E
)E F
{		 	
if

 
(

 
x

 
==

 
null

 
||

 
y

 
==

  
null

! %
)

% &
{ 
return 
false 
; 
} 
return 
x 
. 
Id 
== 
y 
. 
Id 
;  
} 	
public 
int 
GetHashCode 
( 
[  
DisallowNull  ,
], -
PartnerSourceLink. ?
obj@ C
)C D
{ 	
return 
obj 
. 
Id 
. 
GetHashCode %
(% &
)& '
;' (
} 	
} 
} ∑
vD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Util\DateToStringConverter.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Util 
{ 
public 

class !
DateToStringConverter &
{ 
public 
static 
string 
FromDateToString -
(- .
DateTime. 6
date7 ;
,; <
DateViewPattern= L
patternM T
)T U
{ 	
return		 
pattern		 
switch		 !
{

 
DateViewPattern 
.  
Year  $
=>% '
date( ,
., -
ToString- 5
(5 6
$str6 <
)< =
,= >
DateViewPattern 
.  
	MonthYear  )
=>* ,
date- 1
.1 2
ToString2 :
(: ;
$str; G
)G H
,H I
DateViewPattern 
.  

SeasonYear  *
=>+ -
$". 0
{0 1
	GetSeason1 :
(: ;
date; ?
)? @
}@ A
$strA B
{B C
dateC G
.G H
YearH L
}L M
"M N
,N O
DateViewPattern 
.  
DateMonthYear  -
=>. 0
date1 5
.5 6
ToString6 >
(> ?
$str? M
)M N
,N O
_ 
=> 
$str 
} 
; 
} 	
private 
static 
string 
	GetSeason '
(' (
DateTime( 0
dateTime1 9
)9 :
{ 	
if 
( 
dateTime 
. 
Month 
<  
$num! "
||# %
dateTime& .
.. /
Month/ 4
==5 7
$num8 :
): ;
{ 
return 
$str 
; 
} 
else 
if 
( 
dateTime 
. 
Month #
>=$ &
$num' (
&&) +
dateTime, 4
.4 5
Month5 :
<; <
$num= >
)> ?
{ 
return 
$str 
; 
} 
else 
if 
( 
dateTime 
. 
Month "
>=# %
$num& '
&&( *
dateTime+ 3
.3 4
Month4 9
<: ;
$num< =
)= >
{ 
return 
$str 
; 
}   
else!! 
{"" 
return## 
$str## 
;## 
}$$ 
}%% 	
}&& 
}'' ·Q
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Text\AddTermsToTextService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Text" &
{		 
public

 

class

 !
AddTermsToTextService

 &
:

' (
ITextService

) 5
{ 
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
List 
< 
int 
> 
_buffer !
;! "
private 
readonly 
StringBuilder &
_text' ,
=- .
new/ 2
StringBuilder3 @
(@ A
)A B
;B C
public !
AddTermsToTextService $
($ %
IRepositoryWrapper% 7
repositoryWrapper8 I
)I J
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_buffer 
= 
new 
List 
< 
int "
>" #
(# $
)$ %
;% &
Pattern 
= 
new 
( 
$str +
,+ ,
RegexOptions- 9
.9 :
None: >
,> ?
TimeSpan@ H
.H I
FromMillisecondsI Y
(Y Z
$numZ ^
)^ _
)_ `
;` a
} 	
public 
Regex 
Pattern 
{ 
get "
;" #
private$ +
set, /
;/ 0
}1 2
public 
async 
Task 
< 
string  
>  !
AddTermsTag" -
(- .
string. 4
text5 9
)9 :
{ 	
_text 
. 
Clear 
( 
) 
; 
var 
splittedText 
= 
Pattern &
.& '
Split' ,
(, -
text- 1
)1 2
. 
Where 
( 
x 
=> 
! 
string #
.# $
IsNullOrEmpty$ 1
(1 2
x2 3
)3 4
&&5 7
!8 9
string9 ?
.? @
IsNullOrWhiteSpace@ R
(R S
xS T
)T U
)U V
.V W
ToArrayW ^
(^ _
)_ `
;` a
if!! 
(!! 
splittedText!! 
[!! 
$num!! 
]!! 
.!!  
Contains!!  (
(!!( )
$str!!) -
)!!- .
)!!. /
{"" 
var## 
split## 
=## 
splittedText## (
[##( )
$num##) *
]##* +
.##+ ,
Replace##, 3
(##3 4
$str##4 8
,##8 9
$str##: A
)##A B
;##B C
splittedText$$ 
[$$ 
$num$$ 
]$$ 
=$$  !
split$$" '
;$$' (
}%% 
foreach'' 
('' 
var'' 
word'' 
in''  
splittedText''! -
)''- .
{(( 
if)) 
()) 
word)) 
.)) 
Contains)) !
())! "
$char))" %
)))% &
)))& '
{** 
_text++ 
.++ 
Append++  
(++  !
word++! %
)++% &
;++& '
continue,, 
;,, 
}-- 
var// 
(// 
resultedWord// !
,//! "
extras//# )
)//) *
=//+ ,
	CleanWord//- 6
(//6 7
word//7 ;
)//; <
;//< =
var11 
term11 
=11 
await11  
_repositoryWrapper11! 3
.113 4
TermRepository114 B
.22 "
GetFirstOrDefaultAsync22 +
(22+ ,
t33 
=>33 
t33 
.33 
Title33 $
.33$ %
ToLower33% ,
(33, -
)33- .
.33. /
Equals33/ 5
(335 6
resultedWord336 B
.33B C
ToLower33C J
(33J K
)33K L
)33L M
)33M N
;33N O
if55 
(55 
term55 
==55 
null55  
)55  !
{66 
var77 
buffer77 
=77  
await77! &
AddRelatedAsync77' 6
(776 7
resultedWord777 C
)77C D
;77D E
if88 
(88 
!88 
string88 
.88  
IsNullOrEmpty88  -
(88- .
buffer88. 4
)884 5
)885 6
{99 
resultedWord:: $
=::% &
buffer::' -
;::- .
};; 
}<< 
else== 
{>> 
if?? 
(?? 
!?? 
CheckInBuffer?? &
(??& '
term??' +
.??+ ,
Id??, .
)??. /
)??/ 0
{@@ 
resultedWordAA $
=AA% &#
MarkTermWithDescriptionAA' >
(AA> ?
resultedWordAA? K
,AAK L
termAAM Q
.AAQ R
DescriptionAAR ]
)AA] ^
;AA^ _
AddToBufferBB #
(BB# $
termBB$ (
.BB( )
IdBB) +
)BB+ ,
;BB, -
}CC 
}DD 
_textFF 
.FF 
AppendFF 
(FF 
resultedWordFF )
+FF* +
extrasFF, 2
+FF3 4
$charFF5 8
)FF8 9
;FF9 :
}GG 
CLearBufferII 
(II 
)II 
;II 
returnJJ 
_textJJ 
.JJ 
ToStringJJ !
(JJ! "
)JJ" #
;JJ# $
}KK 	
privateMM 
voidMM 
AddToBufferMM  
(MM  !
intMM! $
keyMM% (
)MM( )
=>MM* ,
_bufferMM- 4
.MM4 5
AddMM5 8
(MM8 9
keyMM9 <
)MM< =
;MM= >
privateOO 
boolOO 
CheckInBufferOO "
(OO" #
intOO# &
keyOO' *
)OO* +
=>OO, .
_bufferOO/ 6
.OO6 7
ContainsOO7 ?
(OO? @
keyOO@ C
)OOC D
;OOD E
privateQQ 
voidQQ 
CLearBufferQQ  
(QQ  !
)QQ! "
=>QQ# %
_bufferQQ& -
.QQ- .
ClearQQ. 3
(QQ3 4
)QQ4 5
;QQ5 6
privateSS 
staticSS 
stringSS #
MarkTermWithDescriptionSS 5
(SS5 6
stringSS6 <
wordSS= A
,SSA B
stringSSC I
descriptionSSJ U
)SSU V
=>SSW Y
$"SSZ \
$strSS\ k
{SSk l
wordSSl p
}SSp q
$strSSq ~
{SS~ 
description	SS ä
}
SSä ã
$str
SSã ú
"
SSú ù
;
SSù û
privateUU 
asyncUU 
TaskUU 
<UU 
stringUU !
>UU! "
AddRelatedAsyncUU# 2
(UU2 3
stringUU3 9
clearedWordUU: E
)UUE F
{VV 	
varWW 
relatedTermWW 
=WW 
awaitWW #
_repositoryWrapperWW$ 6
.WW6 7!
RelatedTermRepositoryWW7 L
.XX "
GetFirstOrDefaultAsyncXX '
(XX' (
rtYY 
=>YY 
rtYY 
.YY 
WordYY 
.YY 
ToLowerYY %
(YY% &
)YY& '
.YY' (
EqualsYY( .
(YY. /
clearedWordYY/ :
.YY: ;
ToLowerYY; B
(YYB C
)YYC D
)YYD E
,YYE F
rtZZ 
=>ZZ 
rtZZ 
.ZZ 
IncludeZZ  
(ZZ  !
rtZZ! #
=>ZZ$ &
rtZZ' )
.ZZ) *
TermZZ* .
)ZZ. /
)ZZ/ 0
;ZZ0 1
if\\ 
(\\ 
relatedTerm\\ 
==\\ 
null\\ #
||\\$ &
relatedTerm\\' 2
.\\2 3
Term\\3 7
==\\8 :
null\\; ?
||\\@ B
CheckInBuffer\\C P
(\\P Q
relatedTerm\\Q \
.\\\ ]
TermId\\] c
)\\c d
)\\d e
{]] 
return^^ 
string^^ 
.^^ 
Empty^^ #
;^^# $
}__ 
AddToBufferaa 
(aa 
relatedTermaa #
.aa# $
TermIdaa$ *
)aa* +
;aa+ ,
returncc #
MarkTermWithDescriptioncc *
(cc* +
clearedWordcc+ 6
,cc6 7
relatedTermcc8 C
.ccC D
TermccD H
.ccH I
DescriptionccI T
)ccT U
;ccU V
}dd 	
privateff 
(ff 
stringff 
_clearedWordff $
,ff$ %
stringff& ,
_extrasff- 4
)ff4 5
	CleanWordff6 ?
(ff? @
stringff@ F
wordffG K
)ffK L
{gg 	
varhh 
clearedWordhh 
=hh 
wordhh "
.hh" #
Splithh# (
(hh( )
$charhh) ,
,hh, -
$charhh. 1
)hh1 2
.hh2 3
Firsthh3 8
(hh8 9
)hh9 :
;hh: ;
varjj 
extrasjj 
=jj 
stringjj 
.jj  
Emptyjj  %
;jj% &
ifll 
(ll 
!ll 
wordll 
.ll 
Equalsll 
(ll 
clearedWordll (
)ll( )
)ll) *
{mm 
extrasnn 
=nn 
newnn 
stringnn #
(nn# $
wordnn$ (
.nn( )
Exceptnn) /
(nn/ 0
clearedWordnn0 ;
)nn; <
.nn< =
ToArraynn= D
(nnD E
)nnE F
)nnF G
;nnG H
}oo 
returnqq 
(qq 
clearedWordqq 
,qq  
extrasqq! '
)qq' (
;qq( )
}rr 	
}ss 
}tt ‘3
{D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Payment\PaymentService.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
Services		 !
.		! "
Payment		" )
{

 
public 

class 
PaymentService 
:  !
IPaymentService" 1
{ 
private 
readonly '
PaymentEnvirovmentVariables 4
_paymentEnvirovment5 H
;H I
private 
readonly 

HttpClient #
_httpClient$ /
;/ 0
public 
PaymentService 
( 
IOptions &
<& ''
PaymentEnvirovmentVariables' B
>B C
paymentEnvirovmentD V
)V W
{ 	
_paymentEnvirovment 
=  !
paymentEnvirovment" 4
.4 5
Value5 :
;: ;
_httpClient 
= 
new 

HttpClient (
(( )
)) *
;* +
_httpClient 
. 
BaseAddress #
=$ %
new& )
Uri* -
(- .
Api. 1
.1 2

Production2 <
)< =
;= >
_httpClient 
. !
DefaultRequestHeaders -
.- .
Clear. 3
(3 4
)4 5
;5 6
_httpClient 
. !
DefaultRequestHeaders -
.- .
Add. 1
(1 2
RequestHeaders2 @
.@ A
XTokenA G
,G H
_paymentEnvirovmentI \
.\ ]
Token] b
)b c
;c d
} 	
public 
async 
Task 
< 
InvoiceInfo %
>% &
CreateInvoiceAsync' 9
(9 :
Invoice: A
invoiceB I
)I J
{ 	
var 
( 
code 
, 
body 
) 
= 
await $
	PostAsync% .
(. /
Api/ 2
.2 3
Merchant3 ;
.; <
Invoice< C
.C D
CreateD J
,J K
invoiceL S
)S T
;T U
return 
code 
switch 
{ 
$num 
=> 
JsonToObject #
<# $
InvoiceInfo$ /
>/ 0
(0 1
body1 5
)5 6
,6 7
$num 
=> 
throw 
new  ,
 InvalidRequestParameterException! A
(A B
JsonToObjectB N
<N O
ErrorO T
>T U
(U V
bodyV Z
)Z [
)[ \
,\ ]
$num 
=> 
throw 
new  !
InvalidTokenException! 6
(6 7
)7 8
,8 9
_   
=>   
throw   
new   !
NotSupportedException   4
(  4 5
)  5 6
}!! 
;!! 
}"" 	
private$$ 
async$$ 
Task$$ 
<$$ 
($$ 
int$$ 
Code$$  $
,$$$ %
string$$& ,
Body$$- 1
)$$1 2
>$$2 3
	PostAsync$$4 =
<$$= >
T$$> ?
>$$? @
($$@ A
string$$A G
url$$H K
,$$K L
T$$M N
data$$O S
)$$S T
{%% 	
var&& 

jsonString&& 
=&&  
JsonConvert&&! ,
.&&, -
SerializeObject&&- <
(&&< =
data&&= A
,&&A B

Formatting&&C M
.&&M N
None&&N R
)&&R S
;&&S T
var'' 
content'' 
='' 
new'' !
StringContent''" /
(''/ 0

jsonString''0 :
,'': ;
Encoding''< D
.''D E
UTF8''E I
,''I J
MediaTypeNames''K Y
.''Y Z
Application''Z e
.''e f
Json''f j
)''j k
;''k l
var(( 
response(( 
=(( 
await(( $
_httpClient((% 0
.((0 1
	PostAsync((1 :
(((: ;
url((; >
,((> ?
content((@ G
)((G H
;((H I
return)) 
()) 
Code** 
:** 
(** 
int** 
)** 
response** '
.**' (

StatusCode**( 2
,**2 3
Body++ 
:++ 
await++ 
response++  (
.++( )
Content++) 0
.++0 1
ReadAsStringAsync++1 B
(++B C
)++C D
)++D E
;++E F
},, 	
private.. 
T.. 
JsonToObject.. 
<.. 
T..  
>..  !
(..! "
string.." (
body..) -
)..- .
{// 	
return00 
JsonConvert00 
.00 
DeserializeObject00 0
<000 1
T001 2
>002 3
(003 4
body004 8
)008 9
;009 :
}11 	
private33 
static33 
class33 
Api33  
{44 	
public55 
const55 
string55 

Production55  *
=55+ ,
$str55- F
;55F G
public77 
static77 
class77 
Merchant77  (
{88 
public99 
static99 
class99 #
Invoice99$ +
{:: 
public;; 
const;;  
string;;! '
Create;;( .
=;;/ 0
$str;;1 O
;;;O P
}<< 
}== 
}>> 	
private@@ 
static@@ 
class@@ 
RequestHeaders@@ +
{AA 	
publicBB 
constBB 
stringBB 
XTokenBB  &
=BB' (
$strBB) 2
;BB2 3
}CC 	
privateEE 
staticEE 
classEE 

ValidationEE '
{FF 	
publicGG 
constGG 
intGG )
MaxStatementTimeSpanInSecondsGG :
=GG; <
$numGG= D
;GGD E
publicHH 
constHH 
intHH 1
%StatementTimeoutBetweenCallsInSecondsHH B
=HHC D
$numHHE G
;HHG H
}II 	
}JJ 
}KK ±
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Payment\PaymentEnvirovmentVariables.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Payment" )
;) *
public 
class '
PaymentEnvirovmentVariables (
{ 
public 

string 
Token 
{ 
get 
; 
set "
;" #
}$ %
} ¬
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Payment\Exceptions\MonobankException.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Payment" )
.) *

Exceptions* 4
{ 
public 

abstract 
class 
MonobankException +
:, -
	Exception. 7
{ 
internal 
MonobankException "
(" #
string# )
message* 1
)1 2
:3 4
base5 9
(9 :
message: A
)A B
{ 	
} 	
internal 
MonobankException "
(" #
string# )
message* 1
,1 2
	Exception3 <
	exception= F
)F G
:H I
baseJ N
(N O
messageO V
,V W
	exceptionX a
)a b
{ 	
} 	
} 
} √
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Payment\Exceptions\InvalidTokenException.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Payment" )
.) *

Exceptions* 4
{ 
public 

class !
InvalidTokenException &
:' (
MonobankException) :
{		 
internal

 !
InvalidTokenException

 &
(

& '
)

' (
:

) *
base

+ /
(

/ 0
$str

0 g
)

g h
{ 	
} 	
} 
} ˘	
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Payment\Exceptions\Error.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Payment" )
.) *

Exceptions* 4
{ 
internal 
class 
Error 
{ 
[ 	
JsonConstructor	 
] 
public 
Error 
( 
string 
errCode #
,# $
string% +
errText, 3
)3 4
{		 	
Code

 
=

 
errCode

 
;

 
Text 
= 
errText 
; 
} 	
[ 	
JsonProperty	 
( 
$str 
)  
]  !
public 
string 
Code 
{ 
get  
;  !
}" #
[ 	
JsonProperty	 
( 
$str 
)  
]  !
public 
string 
Text 
{ 
get  
;  !
}" #
} 
} Õ
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Payment\Exceptions\InvalidRequestParameterException.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Payment" )
.) *

Exceptions* 4
{ 
public 

class ,
 InvalidRequestParameterException 1
:2 3
MonobankException4 E
{ 
internal ,
 InvalidRequestParameterException 1
(1 2
Error2 7
error8 =
)= >
: 
base 
( 
$" 
{ 
error 
. 
Code  
}  !
$str! #
{# $
error$ )
.) *
Text* .
}. /
"/ 0
)0 1
{ 	
} 	
} 
} ¬
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Logging\LoggerService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Logging" )
{ 
public 

class 
LoggerService 
:  
ILoggerService! /
{ 
private 
readonly 
ILogger  
_logger! (
;( )
public

 
LoggerService

 
(

 
ILogger

 $
logger

% +
)

+ ,
{ 	
_logger 
= 
logger 
; 
} 	
public 
void 
LogInformation "
(" #
string# )
msg* -
)- .
{ 	
_logger 
. 
Information 
(  
$"  "
{" #
msg# &
}& '
"' (
)( )
;) *
} 	
public 
void 

LogWarning 
( 
string %
msg& )
)) *
{ 	
_logger 
. 
Warning 
( 
$" 
{ 
msg "
}" #
"# $
)$ %
;% &
} 	
public 
void 
LogTrace 
( 
string #
msg$ '
)' (
{ 	
_logger 
. 
Information 
(  
$"  "
{" #
msg# &
}& '
"' (
)( )
;) *
} 	
public 
void 
LogDebug 
( 
string #
msg$ '
)' (
{ 	
_logger   
.   
Debug   
(   
$"   
{   
msg    
}    !
"  ! "
)  " #
;  # $
}!! 	
public## 
void## 
LogError## 
(## 
object## #
request##$ +
,##+ ,
string##- 3
erroMsg##4 ;
)##; <
{$$ 	
string%% 
requestType%% 
=%%  
request%%! (
.%%( )
GetType%%) 0
(%%0 1
)%%1 2
.%%2 3
ToString%%3 ;
(%%; <
)%%< =
;%%= >
string&& 
requestClass&& 
=&&  !
requestType&&" -
.&&- .
	Substring&&. 7
(&&7 8
requestType&&8 C
.&&C D
LastIndexOf&&D O
(&&O P
$char&&P S
)&&S T
+&&U V
$num&&W X
)&&X Y
;&&Y Z
_logger'' 
.'' 
Error'' 
('' 
$"'' 
{'' 
requestClass'' )
}'') *
$str''* C
{''C D
erroMsg''D K
}''K L
"''L M
)''M N
;''N O
}(( 	
})) 
}** ¶#
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Instagram\InstagramService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
	Instagram" +
{ 
public 

class 
InstagramService !
:" #
IInstagramService$ 5
{		 
private

 
readonly

 

HttpClient

 #
_httpClient

$ /
;

/ 0
private 
readonly )
InstagramEnvirovmentVariables 6
_envirovment7 C
;C D
private 
readonly 
string 
_userId  '
;' (
private 
readonly 
string 
_accessToken  ,
;, -
private 
static 
int 
	postLimit $
=% &
$num' )
;) *
public 
InstagramService 
(  
IOptions  (
<( ))
InstagramEnvirovmentVariables) F
>F G 
instagramEnvirovmentH \
)\ ]
{ 	
_httpClient 
= 
new 

HttpClient (
(( )
)) *
;* +
_envirovment 
=  
instagramEnvirovment /
./ 0
Value0 5
;5 6
_userId 
= 
_envirovment "
." #
InstagramID# .
;. /
_accessToken 
= 
_envirovment '
.' (
InstagramToken( 6
;6 7
} 	
public 
async 
Task 
< 
IEnumerable %
<% &
InstagramPost& 3
>3 4
>4 5
GetPostsAsync6 C
(C D
)D E
{ 	
string 
apiUrl 
= 
$" 
$str :
{: ;
_userId; B
}B C
$str	C è
{
è ê
$num
ê ë
*
í ì
	postLimit
î ù
}
ù û
$str
û ¨
{
¨ ≠
_accessToken
≠ π
}
π ∫
"
∫ ª
;
ª º
HttpResponseMessage 
response  (
=) *
await+ 0
_httpClient1 <
.< =
GetAsync= E
(E F
apiUrlF L
)L M
;M N
response 
. #
EnsureSuccessStatusCode ,
(, -
)- .
;. /
string 
jsonResponse 
=  !
await" '
response( 0
.0 1
Content1 8
.8 9
ReadAsStringAsync9 J
(J K
)K L
;L M
var!! 
jsonOptions!! 
=!! 
new!! !!
JsonSerializerOptions!!" 7
{""  
PropertyNamingPolicy## $
=##% &
JsonNamingPolicy##' 7
.##7 8
	CamelCase##8 A
,##A B
IgnoreNullValues$$  
=$$! "
true$$# '
}%% 
;%% 
var'' 
postResponse'' 
='' 
JsonSerializer'' -
.''- .
Deserialize''. 9
<''9 :!
InstagramPostResponse'': O
>''O P
(''P Q
jsonResponse''Q ]
,''] ^
jsonOptions''_ j
)''j k
;''k l
IEnumerable)) 
<)) 
InstagramPost)) %
>))% &
posts))' ,
=))- . 
RemoveVideoMediaType))/ C
())C D
postResponse))D P
.))P Q
Data))Q U
)))U V
;))V W
return++ 
posts++ 
;++ 
},, 	
public.. 
IEnumerable.. 
<.. 
InstagramPost.. (
>..( ) 
RemoveVideoMediaType..* >
(..> ?
IEnumerable..? J
<..J K
InstagramPost..K X
>..X Y
posts..Z _
).._ `
{// 	
return00 
posts00 
.00 
Where00 
(00 
p00  
=>00! #
p00$ %
.00% &
	MediaType00& /
!=000 2
$str003 :
)00: ;
.00; <
Take00< @
(00@ A
	postLimit00A J
)00J K
;00K L
}11 	
}22 
}33 Á
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Instagram\InstagramEnvirovmentVariables.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
	Instagram" +
;+ ,
public		 

class		 )
InstagramEnvirovmentVariables		 .
{

 
public	 
string 
InstagramID "
{# $
get% (
;( )
set* -
;- .
}/ 0
public	 
string 
InstagramToken %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} ™&
wD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\Email\EmailService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
Email" '
{ 
public 

class 
EmailService 
: 
IEmailService  -
{		 
private

 
readonly

 
EmailConfiguration

 +
_emailConfig

, 8
;

8 9
public 
EmailService 
( 
EmailConfiguration .
emailConfig/ :
): ;
{ 	
_emailConfig 
= 
emailConfig &
;& '
} 	
public 
async 
Task 
< 
bool 
> 
SendEmailAsync  .
(. /
Message/ 6
message7 >
)> ?
{ 	
var 
mailMessage 
= 
CreateEmailMessage 0
(0 1
message1 8
)8 9
;9 :
return 
await 
	SendAsync "
(" #
mailMessage# .
). /
;/ 0
} 	
private 
MimeMessage 
CreateEmailMessage .
(. /
Message/ 6
message7 >
)> ?
{ 	
var 
emailMessage 
= 
new "
MimeMessage# .
(. /
)/ 0
;0 1
emailMessage 
. 
From 
. 
Add !
(! "
new" %
MailboxAddress& 4
(4 5
$str5 7
,7 8
_emailConfig9 E
.E F
FromF J
)J K
)K L
;L M
emailMessage 
. 
To 
. 
AddRange $
($ %
message% ,
., -
To- /
)/ 0
;0 1
emailMessage 
. 
Subject  
=! "
message# *
.* +
Subject+ 2
;2 3
var 
bodyBuilder 
= 
new !
BodyBuilder" -
{   
HtmlBody!! 
=!! 
$str"" +
+"", -
$"## 
$str## 
{## 
message## 
.## 
From## #
}### $
$str##$ )
"##) *
+##+ ,
$"$$ 
$str$$ 
{$$ 
message$$  
.$$  !
Content$$! (
}$$( )
"$$) *
+$$+ ,
$str%% 
}&& 
;&& 
emailMessage(( 
.(( 
Body(( 
=(( 
bodyBuilder((  +
.((+ ,
ToMessageBody((, 9
(((9 :
)((: ;
;((; <
return)) 
emailMessage)) 
;))  
}** 	
private,, 
async,, 
Task,, 
<,, 
bool,, 
>,,  
	SendAsync,,! *
(,,* +
MimeMessage,,+ 6
mailMessage,,7 B
),,B C
{-- 	
using.. 
(.. 
var.. 
client.. 
=.. 
new..  #

SmtpClient..$ .
(... /
)../ 0
)..0 1
{// 
try00 
{11 
await22 
client22  
.22  !
ConnectAsync22! -
(22- .
_emailConfig22. :
.22: ;

SmtpServer22; E
,22E F
_emailConfig22G S
.22S T
Port22T X
,22X Y
true22Z ^
)22^ _
;22_ `
client33 
.33 $
AuthenticationMechanisms33 3
.333 4
Remove334 :
(33: ;
$str33; D
)33D E
;33E F
await44 
client44  
.44  !
AuthenticateAsync44! 2
(442 3
_emailConfig443 ?
.44? @
UserName44@ H
,44H I
_emailConfig44J V
.44V W
Password44W _
)44_ `
;44` a
await66 
client66  
.66  !
	SendAsync66! *
(66* +
mailMessage66+ 6
)666 7
;667 8
return77 
true77 
;77  
}88 
catch99 
{:: 
return<< 
false<<  
;<<  !
}== 
finally>> 
{?? 
await@@ 
client@@  
.@@  !
DisconnectAsync@@! 0
(@@0 1
true@@1 5
)@@5 6
;@@6 7
clientAA 
.AA 
DisposeAA "
(AA" #
)AA# $
;AA$ %
}BB 
}CC 
}DD 	
}EE 
}FF ûã
ÉD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\BlobStorageService\BlobService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
BlobStorageService" 4
;4 5
public		 
class		 
BlobService		 
:		 
IBlobService		 '
{

 
private 
readonly $
BlobEnvironmentVariables -
_envirovment. :
;: ;
private 
readonly 
string 
	_keyCrypt %
;% &
private 
readonly 
string 
	_blobPath %
;% &
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
public 

BlobService 
( 
IOptions 
<  $
BlobEnvironmentVariables  8
>8 9
environment: E
,E F
IRepositoryWrapperG Y
?Y Z
repositoryWrapper[ l
=m n
nullo s
)s t
{ 
_envirovment 
= 
environment "
." #
Value# (
;( )
	_keyCrypt 
= 
_envirovment  
.  !
BlobStoreKey! -
;- .
	_blobPath 
= 
_envirovment  
.  !
BlobStorePath! .
;. /
_repositoryWrapper 
= 
repositoryWrapper .
;. /
} 
public 

MemoryStream +
FindFileInStorageAsMemoryStream 7
(7 8
string8 >
name? C
)C D
{ 
string 
[ 
] 
splitedName 
= 
name #
.# $
Split$ )
() *
$char* -
)- .
;. /
byte 
[ 
] 
decodedBytes 
= 
DecryptFile )
() *
splitedName* 5
[5 6
$num6 7
]7 8
,8 9
splitedName: E
[E F
$numF G
]G H
)H I
;I J
var 
image 
= 
new 
MemoryStream $
($ %
decodedBytes% 1
)1 2
;2 3
return   
image   
;   
}!! 
public## 

string## %
FindFileInStorageAsBase64## +
(##+ ,
string##, 2
name##3 7
)##7 8
{$$ 
string%% 
[%% 
]%% 
splitedName%% 
=%% 
name%% #
.%%# $
Split%%$ )
(%%) *
$char%%* -
)%%- .
;%%. /
byte'' 
['' 
]'' 
decodedBytes'' 
='' 
DecryptFile'' )
('') *
splitedName''* 5
[''5 6
$num''6 7
]''7 8
,''8 9
splitedName'': E
[''E F
$num''F G
]''G H
)''H I
;''I J
string)) 
base64)) 
=)) 
Convert)) 
.))  
ToBase64String))  .
()). /
decodedBytes))/ ;
))); <
;))< =
return++ 
base64++ 
;++ 
},, 
public.. 

string.. 
SaveFileInStorage.. #
(..# $
string..$ *
base64..+ 1
,..1 2
string..3 9
name..: >
,..> ?
string..@ F
	extension..G P
)..P Q
{// 
byte00 
[00 
]00 

imageBytes00 
=00 
Convert00 #
.00# $
FromBase64String00$ 4
(004 5
base64005 ;
)00; <
;00< =
string11 
createdFileName11 
=11  
$"11! #
{11# $
DateTime11$ ,
.11, -
Now11- 0
}110 1
{111 2
name112 6
}116 7
"117 8
.22 
Replace22 
(22 
$str22 
,22 
$str22 
)22 
.33 
Replace33 
(33 
$str33 
,33 
$str33 
)33 
.44 
Replace44 
(44 
$str44 
,44 
$str44 
)44 
;44 
string66 
hashBlobStorageName66 "
=66# $
HashFunction66% 1
(661 2
createdFileName662 A
)66A B
;66B C
	Directory88 
.88 
CreateDirectory88 !
(88! "
	_blobPath88" +
)88+ ,
;88, -
EncryptFile99 
(99 

imageBytes99 
,99 
	extension99  )
,99) *
hashBlobStorageName99+ >
)99> ?
;99? @
return;; 
hashBlobStorageName;; "
;;;" #
}<< 
public>> 

void>> #
SaveFileInStorageBase64>> '
(>>' (
string>>( .
base64>>/ 5
,>>5 6
string>>7 =
name>>> B
,>>B C
string>>D J
	extension>>K T
)>>T U
{?? 
byte@@ 
[@@ 
]@@ 

imageBytes@@ 
=@@ 
Convert@@ #
.@@# $
FromBase64String@@$ 4
(@@4 5
base64@@5 ;
)@@; <
;@@< =
	DirectoryAA 
.AA 
CreateDirectoryAA !
(AA! "
	_blobPathAA" +
)AA+ ,
;AA, -
EncryptFileBB 
(BB 

imageBytesBB 
,BB 
	extensionBB  )
,BB) *
nameBB+ /
)BB/ 0
;BB0 1
}CC 
publicEE 

voidEE 
DeleteFileInStorageEE #
(EE# $
stringEE$ *
nameEE+ /
)EE/ 0
{FF 
FileGG 
.GG 
DeleteGG 
(GG 
$"GG 
{GG 
	_blobPathGG  
}GG  !
{GG! "
nameGG" &
}GG& '
"GG' (
)GG( )
;GG) *
}HH 
publicJJ 

stringJJ 
UpdateFileInStorageJJ %
(JJ% &
stringKK 
previousBlobNameKK 
,KK  
stringLL 
base64FormatLL 
,LL 
stringMM 
newBlobNameMM 
,MM 
stringNN 
	extensionNN 
)NN 
{OO 
DeleteFileInStoragePP 
(PP 
previousBlobNamePP ,
)PP, -
;PP- .
stringRR 
hashBlobStorageNameRR "
=RR# $
SaveFileInStorageRR% 6
(RR6 7
base64FormatSS 
,SS 
newBlobNameTT 
,TT 
	extensionUU 
)UU 
;UU 
returnWW 
hashBlobStorageNameWW "
;WW" #
}XX 
publicZZ 

asyncZZ 
TaskZZ 
CleanBlobStorageZZ &
(ZZ& '
)ZZ' (
{[[ 
var\\ 
base64Files\\ 
=\\ 
GetAllBlobNames\\ )
(\\) *
)\\* +
;\\+ ,
var^^ $
existingImagesInDatabase^^ $
=^^% &
await^^' ,
_repositoryWrapper^^- ?
.^^? @
ImageRepository^^@ O
.^^O P
GetAllAsync^^P [
(^^[ \
)^^\ ]
;^^] ^
var__ $
existingAudiosInDatabase__ $
=__% &
await__' ,
_repositoryWrapper__- ?
.__? @
AudioRepository__@ O
.__O P
GetAllAsync__P [
(__[ \
)__\ ]
;__] ^
Listaa 
<aa 
stringaa 
>aa 
existingMediaaa "
=aa# $
newaa% (
(aa) *
)aa* +
;aa+ ,
existingMediabb 
.bb 
AddRangebb 
(bb $
existingImagesInDatabasebb 7
.bb7 8
Selectbb8 >
(bb> ?
imgbb? B
=>bbC E
imgbbF I
.bbI J
BlobNamebbJ R
)bbR S
)bbS T
;bbT U
existingMediacc 
.cc 
AddRangecc 
(cc $
existingAudiosInDatabasecc 7
.cc7 8
Selectcc8 >
(cc> ?
imgcc? B
=>ccC E
imgccF I
.ccI J
BlobNameccJ R
)ccR S
)ccS T
;ccT U
varee 
filesToRemoveee 
=ee 
base64Filesee '
.ee' (
Exceptee( .
(ee. /
existingMediaee/ <
)ee< =
.ee= >
ToListee> D
(eeD E
)eeE F
;eeF G
foreachgg 
(gg 
vargg 
filegg 
ingg 
filesToRemovegg *
)gg* +
{hh 	
Consoleii 
.ii 
	WriteLineii 
(ii 
$"ii  
$strii  )
{ii) *
fileii* .
}ii. /
$strii/ 2
"ii2 3
)ii3 4
;ii4 5
DeleteFileInStoragejj 
(jj  
filejj  $
)jj$ %
;jj% &
}kk 	
}ll 
privatenn 
IEnumerablenn 
<nn 
stringnn 
>nn 
GetAllBlobNamesnn  /
(nn/ 0
)nn0 1
{oo 
varpp 
pathspp 
=pp 
	Directorypp 
.pp 
EnumerateFilespp ,
(pp, -
	_blobPathpp- 6
)pp6 7
;pp7 8
returnrr 
pathsrr 
.rr 
Selectrr 
(rr 
prr 
=>rr  
Pathrr! %
.rr% &
GetFileNamerr& 1
(rr1 2
prr2 3
)rr3 4
)rr4 5
;rr5 6
}ss 
privateuu 
stringuu 
HashFunctionuu 
(uu  
stringuu  &
createdFileNameuu' 6
)uu6 7
{vv 
usingww 
(ww 
varww 
hashww 
=ww 
SHA256ww  
.ww  !
Createww! '
(ww' (
)ww( )
)ww) *
{xx 	
Encodingyy 
encyy 
=yy 
Encodingyy #
.yy# $
UTF8yy$ (
;yy( )
bytezz 
[zz 
]zz 
resultzz 
=zz 
hashzz  
.zz  !
ComputeHashzz! ,
(zz, -
enczz- 0
.zz0 1
GetByteszz1 9
(zz9 :
createdFileNamezz: I
)zzI J
)zzJ K
;zzK L
return{{ 
Convert{{ 
.{{ 
ToBase64String{{ )
({{) *
result{{* 0
){{0 1
.{{1 2
Replace{{2 9
({{9 :
$char{{: =
,{{= >
$char{{? B
){{B C
;{{C D
}|| 	
}}} 
private 
void 
EncryptFile 
( 
byte !
[! "
]" #

imageBytes$ .
,. /
string0 6
type7 ;
,; <
string= C
nameD H
)H I
{
ÄÄ 
byte
ÅÅ 
[
ÅÅ 
]
ÅÅ 
keyBytes
ÅÅ 
=
ÅÅ 
Encoding
ÅÅ "
.
ÅÅ" #
UTF8
ÅÅ# '
.
ÅÅ' (
GetBytes
ÅÅ( 0
(
ÅÅ0 1
	_keyCrypt
ÅÅ1 :
)
ÅÅ: ;
;
ÅÅ; <
byte
ÉÉ 
[
ÉÉ 
]
ÉÉ 
iv
ÉÉ 
=
ÉÉ 
new
ÉÉ 
byte
ÉÉ 
[
ÉÉ 
$num
ÉÉ 
]
ÉÉ  
;
ÉÉ  !
using
ÑÑ 
(
ÑÑ 
var
ÑÑ 
rng
ÑÑ 
=
ÑÑ 
new
ÑÑ &
RNGCryptoServiceProvider
ÑÑ 5
(
ÑÑ5 6
)
ÑÑ6 7
)
ÑÑ7 8
{
ÖÖ 	
rng
ÜÜ 
.
ÜÜ 
GetBytes
ÜÜ 
(
ÜÜ 
iv
ÜÜ 
)
ÜÜ 
;
ÜÜ 
}
áá 	
byte
ââ 
[
ââ 
]
ââ 
encryptedBytes
ââ 
;
ââ 
using
ää 
(
ää 
Aes
ää 
aes
ää 
=
ää 
Aes
ää 
.
ää 
Create
ää #
(
ää# $
)
ää$ %
)
ää% &
{
ãã 	
aes
åå 
.
åå 
KeySize
åå 
=
åå 
$num
åå 
;
åå 
aes
çç 
.
çç 
Key
çç 
=
çç 
keyBytes
çç 
;
çç 
aes
éé 
.
éé 
IV
éé 
=
éé 
iv
éé 
;
éé 
ICryptoTransform
èè 
	encryptor
èè &
=
èè' (
aes
èè) ,
.
èè, -
CreateEncryptor
èè- <
(
èè< =
)
èè= >
;
èè> ?
encryptedBytes
êê 
=
êê 
	encryptor
êê &
.
êê& '!
TransformFinalBlock
êê' :
(
êê: ;

imageBytes
êê; E
,
êêE F
$num
êêG H
,
êêH I

imageBytes
êêJ T
.
êêT U
Length
êêU [
)
êê[ \
;
êê\ ]
}
ëë 	
byte
ìì 
[
ìì 
]
ìì 
encryptedData
ìì 
=
ìì 
new
ìì "
byte
ìì# '
[
ìì' (
encryptedBytes
ìì( 6
.
ìì6 7
Length
ìì7 =
+
ìì> ?
iv
ìì@ B
.
ììB C
Length
ììC I
]
ììI J
;
ììJ K
Buffer
îî 
.
îî 
	BlockCopy
îî 
(
îî 
iv
îî 
,
îî 
$num
îî 
,
îî 
encryptedData
îî  -
,
îî- .
$num
îî/ 0
,
îî0 1
iv
îî2 4
.
îî4 5
Length
îî5 ;
)
îî; <
;
îî< =
Buffer
ïï 
.
ïï 
	BlockCopy
ïï 
(
ïï 
encryptedBytes
ïï '
,
ïï' (
$num
ïï) *
,
ïï* +
encryptedData
ïï, 9
,
ïï9 :
iv
ïï; =
.
ïï= >
Length
ïï> D
,
ïïD E
encryptedBytes
ïïF T
.
ïïT U
Length
ïïU [
)
ïï[ \
;
ïï\ ]
File
ññ 
.
ññ 
WriteAllBytes
ññ 
(
ññ 
$"
ññ 
{
ññ 
	_blobPath
ññ '
}
ññ' (
{
ññ( )
name
ññ) -
}
ññ- .
$str
ññ. /
{
ññ/ 0
type
ññ0 4
}
ññ4 5
"
ññ5 6
,
ññ6 7
encryptedData
ññ8 E
)
ññE F
;
ññF G
}
óó 
private
ôô 
byte
ôô 
[
ôô 
]
ôô 
DecryptFile
ôô 
(
ôô 
string
ôô %
fileName
ôô& .
,
ôô. /
string
ôô0 6
type
ôô7 ;
)
ôô; <
{
öö 
byte
õõ 
[
õõ 
]
õõ 
encryptedData
õõ 
=
õõ 
File
õõ #
.
õõ# $
ReadAllBytes
õõ$ 0
(
õõ0 1
$"
õõ1 3
{
õõ3 4
	_blobPath
õõ4 =
}
õõ= >
{
õõ> ?
fileName
õõ? G
}
õõG H
$str
õõH I
{
õõI J
type
õõJ N
}
õõN O
"
õõO P
)
õõP Q
;
õõQ R
byte
úú 
[
úú 
]
úú 
keyBytes
úú 
=
úú 
Encoding
úú "
.
úú" #
UTF8
úú# '
.
úú' (
GetBytes
úú( 0
(
úú0 1
	_keyCrypt
úú1 :
)
úú: ;
;
úú; <
byte
ûû 
[
ûû 
]
ûû 
iv
ûû 
=
ûû 
new
ûû 
byte
ûû 
[
ûû 
$num
ûû 
]
ûû  
;
ûû  !
Buffer
üü 
.
üü 
	BlockCopy
üü 
(
üü 
encryptedData
üü &
,
üü& '
$num
üü( )
,
üü) *
iv
üü+ -
,
üü- .
$num
üü/ 0
,
üü0 1
iv
üü2 4
.
üü4 5
Length
üü5 ;
)
üü; <
;
üü< =
byte
°° 
[
°° 
]
°° 
decryptedBytes
°° 
;
°° 
using
¢¢ 
(
¢¢ 
Aes
¢¢ 
aes
¢¢ 
=
¢¢ 
Aes
¢¢ 
.
¢¢ 
Create
¢¢ #
(
¢¢# $
)
¢¢$ %
)
¢¢% &
{
££ 	
aes
§§ 
.
§§ 
KeySize
§§ 
=
§§ 
$num
§§ 
;
§§ 
aes
•• 
.
•• 
Key
•• 
=
•• 
keyBytes
•• 
;
•• 
aes
¶¶ 
.
¶¶ 
IV
¶¶ 
=
¶¶ 
iv
¶¶ 
;
¶¶ 
ICryptoTransform
ßß 
	decryptor
ßß &
=
ßß' (
aes
ßß) ,
.
ßß, -
CreateDecryptor
ßß- <
(
ßß< =
)
ßß= >
;
ßß> ?
decryptedBytes
®® 
=
®® 
	decryptor
®® &
.
®®& '!
TransformFinalBlock
®®' :
(
®®: ;
encryptedData
®®; H
,
®®H I
iv
®®J L
.
®®L M
Length
®®M S
,
®®S T
encryptedData
®®U b
.
®®b c
Length
®®c i
-
®®j k
iv
®®l n
.
®®n o
Length
®®o u
)
®®u v
;
®®v w
}
©© 	
return
´´ 
decryptedBytes
´´ 
;
´´ 
}
¨¨ 
}≠≠ È
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Services\BlobStorageService\BlobEnvirovmentVariables.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Services !
.! "
BlobStorageService" 4
;4 5
public 
class $
BlobEnvironmentVariables %
{ 
public 

string 
BlobStoreKey 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

string 
BlobStorePath 
{  !
get" %
;% &
set' *
;* +
}, -
} ä
µD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Transactions\TransactionLink\GetByStreetcodeId\GetTransactLinkByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Transactions! -
.- .
TransactionLink. =
.= >
GetByStreetcodeId> O
;O P
public 
record .
"GetTransactLinkByStreetcodeIdQuery 0
(0 1
int1 4
StreetcodeId5 A
)A B
:C D
IRequestE M
<M N
ResultN T
<T U
TransactLinkDTOU d
?d e
>e f
>f g
;g hﬂ!
∑D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Transactions\TransactionLink\GetByStreetcodeId\GetTransactLinkByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Transactions! -
.- .
TransactionLink. =
.= >
GetByStreetcodeId> O
;O P
public 
class 0
$GetTransactLinkByStreetcodeIdHandler 1
:2 3
IRequestHandler4 C
<C D.
"GetTransactLinkByStreetcodeIdQueryD f
,f g
Resulth n
<n o
TransactLinkDTOo ~
?~ 
>	 Ä
>
Ä Å
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
0
$GetTransactLinkByStreetcodeIdHandler /
(/ 0
IRepositoryWrapper0 B
repositoryWrapperC T
,T U
IMapperV ]
mapper^ d
,d e
ILoggerServicef t
loggeru {
){ |
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TransactLinkDTO ,
?, -
>- .
>. /
Handle0 6
(6 7.
"GetTransactLinkByStreetcodeIdQuery7 Y
requestZ a
,a b
CancellationTokenc t
cancellationToken	u Ü
)
Ü á
{ 
var 
transactLink 
= 
await  
_repositoryWrapper! 3
.3 4#
TransactLinksRepository4 K
. "
GetFirstOrDefaultAsync #
(# $
f$ %
=>& (
f) *
.* +
StreetcodeId+ 7
==8 :
request; B
.B C
StreetcodeIdC O
)O P
;P Q
if 

( 
transactLink 
is 
null  
)  !
{   	
if!! 
(!! 
await!! 
_repositoryWrapper!! (
.!!( ) 
StreetcodeRepository!!) =
."" "
GetFirstOrDefaultAsync"" '
(""' (
s""( )
=>""* ,
s""- .
."". /
Id""/ 1
==""2 4
request""5 <
.""< =
StreetcodeId""= I
)""I J
==""K M
null""N R
)""R S
{## 
string$$ 
errorMsg$$ 
=$$  !
$"$$" $
$str$$$ W
{$$W X
request$$X _
.$$_ `
StreetcodeId$$` l
}$$l m
$str	$$m î
"
$$î ï
;
$$ï ñ
_logger%% 
.%% 
LogError%%  
(%%  !
request%%! (
,%%( )
errorMsg%%* 2
)%%2 3
;%%3 4
return&& 
Result&& 
.&& 
Fail&& "
(&&" #
new&&# &
Error&&' ,
(&&, -
errorMsg&&- 5
)&&5 6
)&&6 7
;&&7 8
}'' 
}(( 	

NullResult** 
<** 
TransactLinkDTO** "
?**" #
>**# $
result**% +
=**, -
new**. 1

NullResult**2 <
<**< =
TransactLinkDTO**= L
?**L M
>**M N
(**N O
)**O P
;**P Q
result++ 
.++ 
	WithValue++ 
(++ 
_mapper++  
.++  !
Map++! $
<++$ %
TransactLinkDTO++% 4
?++4 5
>++5 6
(++6 7
transactLink++7 C
)++C D
)++D E
;++E F
return,, 
result,, 
;,, 
}-- 
}.. …
°D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Transactions\TransactionLink\GetById\GetTransactLinkByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Transactions! -
.- .
TransactionLink. =
.= >
GetById> E
;E F
public 
record $
GetTransactLinkByIdQuery &
(& '
int' *
Id+ -
)- .
:/ 0
IRequest1 9
<9 :
Result: @
<@ A
TransactLinkDTOA P
>P Q
>Q R
;R S§
£D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Transactions\TransactionLink\GetById\GetTransactLinkByIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Transactions		! -
.		- .
TransactionLink		. =
.		= >
GetById		> E
;		E F
public 
class &
GetTransactLinkByIdHandler '
:( )
IRequestHandler* 9
<9 :$
GetTransactLinkByIdQuery: R
,R S
ResultT Z
<Z [
TransactLinkDTO[ j
>j k
>k l
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
&
GetTransactLinkByIdHandler %
(% &
IRepositoryWrapper& 8
repositoryWrapper9 J
,J K
IMapperL S
mapperT Z
,Z [
ILoggerService\ j
loggerk q
)q r
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TransactLinkDTO ,
>, -
>- .
Handle/ 5
(5 6$
GetTransactLinkByIdQuery6 N
requestO V
,V W
CancellationTokenX i
cancellationTokenj {
){ |
{ 
var 
transactLink 
= 
await  
_repositoryWrapper! 3
.3 4#
TransactLinksRepository4 K
. "
GetFirstOrDefaultAsync #
(# $
f$ %
=>& (
f) *
.* +
Id+ -
==. 0
request1 8
.8 9
Id9 ;
); <
;< =
if 

( 
transactLink 
is 
null  
)  !
{ 	
string 
errorMsg 
= 
$"  
$str  X
{X Y
requestY `
.` a
Ida c
}c d
"d e
;e f
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$  
.$$  !
Map$$! $
<$$$ %
TransactLinkDTO$$% 4
>$$4 5
($$5 6
transactLink$$6 B
)$$B C
)$$C D
;$$D E
}%% 
}&& ø
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Transactions\TransactionLink\GetAll\GetAllTransactLinksQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Transactions! -
.- .
TransactionLink. =
.= >
GetAll> D
;D E
public 
record $
GetAllTransactLinksQuery &
:' (
IRequest) 1
<1 2
Result2 8
<8 9
IEnumerable9 D
<D E
TransactLinkDTOE T
>T U
>U V
>V W
;W XÚ
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Transactions\TransactionLink\GetAll\GetAllTransactLinksHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Transactions		! -
.		- .
TransactionLink		. =
.		= >
GetAll		> D
;		D E
public 
class &
GetAllTransactLinksHandler '
:( )
IRequestHandler* 9
<9 :$
GetAllTransactLinksQuery: R
,R S
ResultT Z
<Z [
IEnumerable[ f
<f g
TransactLinkDTOg v
>v w
>w x
>x y
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
&
GetAllTransactLinksHandler %
(% &
IRepositoryWrapper& 8
repositoryWrapper9 J
,J K
IMapperL S
mapperT Z
,Z [
ILoggerService\ j
loggerk q
)q r
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
TransactLinkDTO) 8
>8 9
>9 :
>: ;
Handle< B
(B C$
GetAllTransactLinksQueryC [
request\ c
,c d
CancellationTokene v
cancellationToken	w à
)
à â
{ 
var 
transactLinks 
= 
await !
_repositoryWrapper" 4
.4 5#
TransactLinksRepository5 L
.L M
GetAllAsyncM X
(X Y
)Y Z
;Z [
if 

( 
transactLinks 
is 
null !
)! "
{ 	
const 
string 
errorMsg !
=" #
$"$ &
$str& F
"F G
;G H
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return   
Result   
.   
Fail   
(   
new   "
Error  # (
(  ( )
errorMsg  ) 1
)  1 2
)  2 3
;  3 4
}!! 	
return## 
Result## 
.## 
Ok## 
(## 
_mapper##  
.##  !
Map##! $
<##$ %
IEnumerable##% 0
<##0 1
TransactLinkDTO##1 @
>##@ A
>##A B
(##B C
transactLinks##C P
)##P Q
)##Q R
;##R S
}$$ 
}%% ·
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Toponyms\GetByStreetcodeId\GetToponymsByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Toponyms! )
.) *
GetByStreetcodeId* ;
;; <
public 
record *
GetToponymsByStreetcodeIdQuery ,
(, -
int- 0
StreetcodeId1 =
)= >
:? @
IRequestA I
<I J
ResultJ P
<P Q
IEnumerableQ \
<\ ]

ToponymDTO] g
>g h
>h i
>i j
;j k⁄$
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Toponyms\GetByStreetcodeId\GetToponymsByStreetcodeIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Toponyms

! )
.

) *
GetByStreetcodeId

* ;
;

; <
public 
class ,
 GetToponymsByStreetcodeIdHandler -
:. /
IRequestHandler0 ?
<? @*
GetToponymsByStreetcodeIdQuery@ ^
,^ _
Result` f
<f g
IEnumerableg r
<r s

ToponymDTOs }
>} ~
>~ 
>	 Ä
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
,
 GetToponymsByStreetcodeIdHandler +
(+ ,
IRepositoryWrapper, >
repositoryWrapper? P
,P Q
IMapperR Y
mapperZ `
,` a
ILoggerServiceb p
loggerq w
)w x
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )

ToponymDTO) 3
>3 4
>4 5
>5 6
Handle7 =
(= >*
GetToponymsByStreetcodeIdQuery> \
request] d
,d e
CancellationTokenf w
cancellationToken	x â
)
â ä
{ 
var 
toponyms 
= 
await 
_repositoryWrapper /
. 
ToponymRepository 
. 
GetAllAsync 
( 
	predicate 
: 
sc 
=>  
sc! #
.# $
Streetcodes$ /
./ 0
Any0 3
(3 4
s4 5
=>6 8
s9 :
.: ;
Id; =
==> @
requestA H
.H I
StreetcodeIdI U
)U V
,V W
include 
: 
scl 
=> 
scl  #
.   
Include   
(   
sc   
=>    "
sc  # %
.  % &

Coordinate  & 0
)  0 1
)  1 2
;  2 3
toponyms!! 
=!! 
toponyms!! 
.!! 

DistinctBy!! &
(!!& '
x!!' (
=>!!) +
x!!, -
.!!- .

StreetName!!. 8
)!!8 9
;!!9 :
if## 

(## 
!## 
toponyms## 
.## 
Any## 
(## 
)## 
)## 
{$$ 	
string%% 
errorMsg%% 
=%% 
$"%%  
$str%%  N
{%%N O
request%%O V
.%%V W
StreetcodeId%%W c
}%%c d
"%%d e
;%%e f
_logger&& 
.&& 
LogError&& 
(&& 
request&& $
,&&$ %
errorMsg&&& .
)&&. /
;&&/ 0
return'' 
Result'' 
.'' 
Fail'' 
('' 
new'' "
Error''# (
(''( )
errorMsg'') 1
)''1 2
)''2 3
;''3 4
}(( 	
var** 

toponymDto** 
=** 
toponyms** !
.**! "
GroupBy**" )
(**) *
x*** +
=>**, .
x**/ 0
.**0 1

StreetName**1 ;
)**; <
.**< =
Select**= C
(**C D
group**D I
=>**J L
group**M R
.**R S
First**S X
(**X Y
)**Y Z
)**Z [
.**[ \
Select**\ b
(**b c
x**c d
=>**e g
_mapper**h o
.**o p
Map**p s
<**s t

ToponymDTO**t ~
>**~ 
(	** Ä
x
**Ä Å
)
**Å Ç
)
**Ç É
;
**É Ñ
return++ 
Result++ 
.++ 
Ok++ 
(++ 

toponymDto++ #
)++# $
;++$ %
},, 
}-- ˆ
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Toponyms\GetById\GetToponymByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Toponyms! )
.) *
GetById* 1
;1 2
public 
record 
GetToponymByIdQuery !
(! "
int" %
Id& (
)( )
:* +
IRequest, 4
<4 5
Result5 ;
<; <

ToponymDTO< F
>F G
>G H
;H I£
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Toponyms\GetById\GetToponymByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Toponyms! )
.) *
GetById* 1
;1 2
public

 
class

 !
GetToponymByIdHandler

 "
:

# $
IRequestHandler

% 4
<

4 5
GetToponymByIdQuery

5 H
,

H I
Result

J P
<

P Q

ToponymDTO

Q [
>

[ \
>

\ ]
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
!
GetToponymByIdHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
IMapperG N
mapperO U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 

ToponymDTO '
>' (
>( )
Handle* 0
(0 1
GetToponymByIdQuery1 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
toponym 
= 
await 
_repositoryWrapper .
.. /
ToponymRepository/ @
. "
GetFirstOrDefaultAsync #
(# $
f$ %
=>& (
f) *
.* +
Id+ -
==. 0
request1 8
.8 9
Id9 ;
); <
;< =
if 

( 
toponym 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  O
{O P
requestP W
.W X
IdX Z
}Z [
"[ \
;\ ]
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return   
Result   
.   
Fail   
(   
new   "
Error  # (
(  ( )
errorMsg  ) 1
)  1 2
)  2 3
;  3 4
}!! 	
return## 
Result## 
.## 
Ok## 
(## 
_mapper##  
.##  !
Map##! $
<##$ %

ToponymDTO##% /
>##/ 0
(##0 1
toponym##1 8
)##8 9
)##9 :
;##: ;
}$$ 
}%% ù
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Toponyms\GetAll\GetAllToponymsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Toponyms! )
.) *
GetAll* 0
;0 1
public 
record 
GetAllToponymsQuery !
(! "$
GetAllToponymsRequestDTO" :
request; B
)B C
: 
IRequest 
< 
Result 
< %
GetAllToponymsResponseDTO /
>/ 0
>0 1
;1 2Ñ#
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Toponyms\GetAll\GetAllToponymsHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Toponyms		! )
.		) *
GetAll		* 0
;		0 1
public 
class !
GetAllToponymsHandler "
:# $
IRequestHandler% 4
<4 5
GetAllToponymsQuery5 H
,H I
Result 

<
 %
GetAllToponymsResponseDTO $
>$ %
>% &
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
!
GetAllToponymsHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
IMapperG N
mapperO U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< %
GetAllToponymsResponseDTO 6
>6 7
>7 8
Handle9 ?
(? @
GetAllToponymsQuery@ S
queryT Y
,Y Z
CancellationToken[ l
cancellationTokenm ~
)~ 
{ 
var 
filterRequest 
= 
query !
.! "
request" )
;) *
var 
toponyms 
= 
_repositoryWrapper )
.) *
ToponymRepository* ;
. 
FindAll 
( 
) 
; 
if   

(   
filterRequest   
.   
Title   
is    "
not  # &
null  ' +
)  + ,
{!! 	)
FindStreetcodesWithMatchTitle"" )
("") *
ref""* -
toponyms"". 6
,""6 7
filterRequest""8 E
.""E F
Title""F K
)""K L
;""L M
}## 	
var'' 
toponymDtos'' 
='' 
_mapper'' !
.''! "
Map''" %
<''% &
IEnumerable''& 1
<''1 2

ToponymDTO''2 <
>''< =
>''= >
(''> ?
toponyms''? G
.''G H
AsEnumerable''H T
(''T U
)''U V
)''V W
;''W X
var)) 
response)) 
=)) 
new)) %
GetAllToponymsResponseDTO)) 4
{** 	
Pages++ 
=++ 
$num++ 
,++ 
Toponyms,, 
=,, 
toponymDtos,, "
}-- 	
;--	 

return// 
Result// 
.// 
Ok// 
(// 
response// !
)//! "
;//" #
}00 
private22 
void22 )
FindStreetcodesWithMatchTitle22 .
(22. /
ref33 

IQueryable33 
<33 
Toponym33 
>33 
toponyms33  (
,33( )
string44 
title44 
)44 
{55 
toponyms66 
=66 
toponyms66 
.66 
Where66 !
(66! "
s66" #
=>66$ &
s66' (
.66( )

StreetName66) 3
.77 
ToLower77 
(77 
)77 
.88 
Contains88 
(88 
title88 
.99 
ToLower99 
(99 
)99 
)99 
)99 
.:: 
GroupBy:: 
(:: 
s:: 
=>:: 
s:: 
.:: 

StreetName:: &
)::& '
.;; 
Select;; 
(;; 
g;; 
=>;; 
g;; 
.;; 
First;;  
(;;  !
);;! "
);;" #
;;;# $
}<< 
}KK ¶
ØD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\TimelineItem\GetByStreetcodeId\GetTimelineItemsByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Timeline! )
.) *
TimelineItem* 6
.6 7
GetByStreetcodeId7 H
;H I
public 
record /
#GetTimelineItemsByStreetcodeIdQuery 1
(1 2
int2 5
StreetcodeId6 B
)B C
:D E
IRequestF N
<N O
ResultO U
<U V
IEnumerableV a
<a b
TimelineItemDTOb q
>q r
>r s
>s t
;t uà 
±D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\TimelineItem\GetByStreetcodeId\GetTimelineItemsByStreetcodeIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Timeline

! )
.

) *
TimelineItem

* 6
.

6 7
GetByStreetcodeId

7 H
;

H I
public 
class 1
%GetTimelineItemsByStreetcodeIdHandler 2
:3 4
IRequestHandler5 D
<D E/
#GetTimelineItemsByStreetcodeIdQueryE h
,h i
Resultj p
<p q
IEnumerableq |
<| }
TimelineItemDTO	} å
>
å ç
>
ç é
>
é è
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
1
%GetTimelineItemsByStreetcodeIdHandler 0
(0 1
IRepositoryWrapper1 C
repositoryWrapperD U
,U V
IMapperW ^
mapper_ e
,e f
ILoggerServiceg u
loggerv |
)| }
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
TimelineItemDTO) 8
>8 9
>9 :
>: ;
Handle< B
(B C/
#GetTimelineItemsByStreetcodeIdQueryC f
requestg n
,n o
CancellationToken	p Å
cancellationToken
Ç ì
)
ì î
{ 
var 
timelineItems 
= 
await !
_repositoryWrapper" 4
.4 5
TimelineRepository5 G
. 
GetAllAsync 
( 
	predicate 
: 
f 
=> 
f  !
.! "
StreetcodeId" .
==/ 1
request2 9
.9 :
StreetcodeId: F
,F G
include 
: 
ti 
=> 
ti !
. 
Include 
( 
til  
=>! #
til$ '
.' (&
HistoricalContextTimelines( B
)B C
.   
ThenInclude   $
(  $ %
x  % &
=>  ' )
x  * +
.  + ,
HistoricalContext  , =
)  = >
!  > ?
)  ? @
;  @ A
if"" 

("" 
timelineItems"" 
is"" 
null"" !
)""! "
{## 	
string$$ 
errorMsg$$ 
=$$ 
$"$$  
$str$$  T
{$$T U
request$$U \
.$$\ ]
StreetcodeId$$] i
}$$i j
"$$j k
;$$k l
_logger%% 
.%% 
LogError%% 
(%% 
request%% $
,%%$ %
errorMsg%%& .
)%%. /
;%%/ 0
return&& 
Result&& 
.&& 
Fail&& 
(&& 
new&& "
Error&&# (
(&&( )
errorMsg&&) 1
)&&1 2
)&&2 3
;&&3 4
}'' 	
return)) 
Result)) 
.)) 
Ok)) 
()) 
_mapper))  
.))  !
Map))! $
<))$ %
IEnumerable))% 0
<))0 1
TimelineItemDTO))1 @
>))@ A
>))A B
())B C
timelineItems))C P
)))P Q
)))Q R
;))R S
}** 
}++ ª
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\TimelineItem\GetById\GetTimelineItemByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Timeline! )
.) *
TimelineItem* 6
.6 7
GetById7 >
;> ?
public 
record $
GetTimelineItemByIdQuery &
(& '
int' *
Id+ -
)- .
:/ 0
IRequest1 9
<9 :
Result: @
<@ A
TimelineItemDTOA P
>P Q
>Q R
;R Só
úD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\TimelineItem\GetById\GetTimelineItemByIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Timeline		! )
.		) *
TimelineItem		* 6
.		6 7
GetById		7 >
;		> ?
public 
class &
GetTimelineItemByIdHandler '
:( )
IRequestHandler* 9
<9 :$
GetTimelineItemByIdQuery: R
,R S
ResultT Z
<Z [
TimelineItemDTO[ j
>j k
>k l
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
&
GetTimelineItemByIdHandler %
(% &
IRepositoryWrapper& 8
repositoryWrapper9 J
,J K
IMapperL S
mapperT Z
,Z [
ILoggerService\ j
loggerk q
)q r
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TimelineItemDTO ,
>, -
>- .
Handle/ 5
(5 6$
GetTimelineItemByIdQuery6 N
requestO V
,V W
CancellationTokenX i
cancellationTokenj {
){ |
{ 
var 
timelineItem 
= 
await  
_repositoryWrapper! 3
.3 4
TimelineRepository4 F
. "
GetFirstOrDefaultAsync #
(# $
	predicate 
: 
ti 
=>  
true! %
,% &
include 
: 
ti 
=> 
ti !
. 
Include 
( 
til  
=>! #
til$ '
.' (&
HistoricalContextTimelines( B
)B C
. 
ThenInclude $
($ %
x% &
=>' )
x* +
.+ ,
HistoricalContext, =
)= >
!> ?
)? @
;@ A
if!! 

(!! 
timelineItem!! 
is!! 
null!!  
)!!  !
{"" 	
string## 
errorMsg## 
=## 
$"##  
$str##  S
{##S T
request##T [
.##[ \
Id##\ ^
}##^ _
"##_ `
;##` a
_logger$$ 
.$$ 
LogError$$ 
($$ 
request$$ $
,$$$ %
errorMsg$$& .
)$$. /
;$$/ 0
return%% 
Result%% 
.%% 
Fail%% 
(%% 
new%% "
Error%%# (
(%%( )
errorMsg%%) 1
)%%1 2
)%%2 3
;%%3 4
}&& 	
return(( 
Result(( 
.(( 
Ok(( 
((( 
_mapper((  
.((  !
Map((! $
<(($ %
TimelineItemDTO((% 4
>((4 5
(((5 6
timelineItem((6 B
)((B C
)((C D
;((D E
})) 
}** ±
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\TimelineItem\GetAll\GetAllTimelineItemsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Timeline! )
.) *
TimelineItem* 6
.6 7
GetAll7 =
;= >
public 
record $
GetAllTimelineItemsQuery &
:' (
IRequest) 1
<1 2
Result2 8
<8 9
IEnumerable9 D
<D E
TimelineItemDTOE T
>T U
>U V
>V W
;W Xè
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\TimelineItem\GetAll\GetAllTimelineItemsHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Timeline! )
.) *
TimelineItem* 6
.6 7
GetAll7 =
;= >
public 
class &
GetAllTimelineItemsHandler '
:( )
IRequestHandler* 9
<9 :$
GetAllTimelineItemsQuery: R
,R S
ResultT Z
<Z [
IEnumerable[ f
<f g
TimelineItemDTOg v
>v w
>w x
>x y
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
&
GetAllTimelineItemsHandler %
(% &
IRepositoryWrapper& 8
repositoryWrapper9 J
,J K
IMapperL S
mapperT Z
,Z [
ILoggerService\ j
loggerk q
)q r
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
TimelineItemDTO) 8
>8 9
>9 :
>: ;
Handle< B
(B C$
GetAllTimelineItemsQueryC [
request\ c
,c d
CancellationTokene v
cancellationToken	w à
)
à â
{ 
var 
timelineItems 
= 
await !
_repositoryWrapper" 4
. 
TimelineRepository 
.  
GetAllAsync  +
(+ ,
include 
: 
ti 
=> 
ti !
. 
Include 
( 
til 
=> !
til" %
.% &&
HistoricalContextTimelines& @
)@ A
.   
ThenInclude    
(    !
x  ! "
=>  # %
x  & '
.  ' (
HistoricalContext  ( 9
)  9 :
!  : ;
)  ; <
;  < =
if"" 

("" 
timelineItems"" 
is"" 
null"" !
)""! "
{## 	
const$$ 
string$$ 
errorMsg$$ !
=$$" #
$"$$$ &
$str$$& B
"$$B C
;$$C D
_logger%% 
.%% 
LogError%% 
(%% 
request%% $
,%%$ %
errorMsg%%& .
)%%. /
;%%/ 0
return&& 
Result&& 
.&& 
Fail&& 
(&& 
new&& "
Error&&# (
(&&( )
errorMsg&&) 1
)&&1 2
)&&2 3
;&&3 4
}'' 	
return)) 
Result)) 
.)) 
Ok)) 
()) 
_mapper))  
.))  !
Map))! $
<))$ %
IEnumerable))% 0
<))0 1
TimelineItemDTO))1 @
>))@ A
>))A B
())B C
timelineItems))C P
)))P Q
)))Q R
;))R S
}** 
}++ ’
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\HistoricalContext\GetAll\GetAllHistoricalContextQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Timeline! )
.) *
HistoricalContext* ;
.; <
GetAll< B
{ 
public 

record (
GetAllHistoricalContextQuery .
:/ 0
IRequest1 9
<9 :
Result: @
<@ A
IEnumerableA L
<L M 
HistoricalContextDTOM a
>a b
>b c
>c d
;d e
}  
§D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Timeline\HistoricalContext\GetAll\GetAllHistoricalContextHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Timeline		! )
.		) *
HistoricalContext		* ;
.		; <
GetAll		< B
{

 
public 

class *
GetAllHistoricalContextHandler /
:0 1
IRequestHandler2 A
<A B(
GetAllHistoricalContextQueryB ^
,^ _
Result` f
<f g
IEnumerableg r
<r s!
HistoricalContextDTO	s á
>
á à
>
à â
>
â ä
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public *
GetAllHistoricalContextHandler -
(- .
IRepositoryWrapper. @
repositoryWrapperA R
,R S
IMapperT [
mapper\ b
,b c
ILoggerServiced r
loggers y
)y z
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, - 
HistoricalContextDTO- A
>A B
>B C
>C D
HandleE K
(K L(
GetAllHistoricalContextQueryL h
requesti p
,p q
CancellationToken	r É
cancellationToken
Ñ ï
)
ï ñ
{ 	
var "
historicalContextItems &
=' (
await) .
_repositoryWrapper/ A
. '
HistoricalContextRepository ,
. 
GetAllAsync 
( 
) 
; 
if 
( "
historicalContextItems &
is' )
null* .
). /
{ 
const   
string   
errorMsg   %
=  & '
$"  ( *
$str  * M
"  M N
;  N O
_logger!! 
.!! 
LogError!!  
(!!  !
request!!! (
,!!( )
errorMsg!!* 2
)!!2 3
;!!3 4
return"" 
Result"" 
."" 
Fail"" "
(""" #
new""# &
Error""' ,
("", -
errorMsg""- 5
)""5 6
)""6 7
;""7 8
}## 
return%% 
Result%% 
.%% 
Ok%% 
(%% 
_mapper%% $
.%%$ %
Map%%% (
<%%( )
IEnumerable%%) 4
<%%4 5 
HistoricalContextDTO%%5 I
>%%I J
>%%J K
(%%K L"
historicalContextItems%%L b
)%%b c
)%%c d
;%%d e
}&& 	
}'' 
}(( ∂
îD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\TeamMembersLinks\GetAll\GetAllTeamLinkQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
TeamMembersLinks& 6
.6 7
GetAll7 =
{ 
public 

record 
GetAllTeamLinkQuery %
:& '
IRequest( 0
<0 1
Result1 7
<7 8
IEnumerable8 C
<C D
TeamMemberLinkDTOD U
>U V
>V W
>W X
;X Y
} —
ñD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\TeamMembersLinks\GetAll\GetAllTeamLinkHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Team		! %
.		% &
TeamMembersLinks		& 6
.		6 7
GetAll		7 =
{

 
public 

class !
GetAllTeamLinkHandler &
:' (
IRequestHandler) 8
<8 9
GetAllTeamLinkQuery9 L
,L M
ResultN T
<T U
IEnumerableU `
<` a
TeamMemberLinkDTOa r
>r s
>s t
>t u
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public !
GetAllTeamLinkHandler $
($ %
IRepositoryWrapper% 7
repositoryWrapper8 I
,I J
IMapperK R
mapperS Y
,Y Z
ILoggerService[ i
loggerj p
)p q
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
TeamMemberLinkDTO- >
>> ?
>? @
>@ A
HandleB H
(H I
GetAllTeamLinkQueryI \
request] d
,d e
CancellationTokenf w
cancellationToken	x â
)
â ä
{ 	
var 
	teamLinks 
= 
await !
_repositoryWrapper" 4
. 
TeamLinkRepository #
. 
GetAllAsync 
( 
) 
; 
if 
( 
	teamLinks 
is 
null !
)! "
{ 
const   
string   
errorMsg   %
=  & '
$"  ( *
$str  * D
"  D E
;  E F
_logger!! 
.!! 
LogError!!  
(!!  !
request!!! (
,!!( )
errorMsg!!* 2
)!!2 3
;!!3 4
return"" 
Result"" 
."" 
Fail"" "
(""" #
new""# &
Error""' ,
("", -
errorMsg""- 5
)""5 6
)""6 7
;""7 8
}## 
return%% 
Result%% 
.%% 
Ok%% 
(%% 
_mapper%% $
.%%$ %
Map%%% (
<%%( )
IEnumerable%%) 4
<%%4 5
TeamMemberLinkDTO%%5 F
>%%F G
>%%G H
(%%H I
	teamLinks%%I R
)%%R S
)%%S T
;%%T U
}&& 	
}'' 
}(( ‘
îD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\TeamMembersLinks\Create\CreateTeamLinkQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
TeamMembersLinks& 6
.6 7
Create7 =
{ 
public 

record 
CreateTeamLinkQuery %
(% &
TeamMemberLinkDTO& 7

teamMember8 B
)B C
:D E
IRequestF N
<N O
ResultO U
<U V
TeamMemberLinkDTOV g
>g h
>h i
;i j
} ∂,
ñD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\TeamMembersLinks\Create\CreateTeamLinkHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Team		! %
.		% &
TeamMembersLinks		& 6
.		6 7
Create		7 =
{

 
public 

class !
CreateTeamLinkHandler &
:' (
IRequestHandler) 8
<8 9
CreateTeamLinkQuery9 L
,L M
ResultN T
<T U
TeamMemberLinkDTOU f
>f g
>g h
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public !
CreateTeamLinkHandler $
($ %
IMapper% ,
mapper- 3
,3 4
IRepositoryWrapper5 G

repositoryH R
,R S
ILoggerServiceT b
loggerc i
)i j
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
TeamMemberLinkDTO! 2
>2 3
>3 4
Handle5 ;
(; <
CreateTeamLinkQuery< O
requestP W
,W X
CancellationTokenY j
cancellationTokenk |
)| }
{ 	
var 
teamMemberLink 
=  
_mapper! (
.( )
Map) ,
<, -
DAL- 0
.0 1
Entities1 9
.9 :
Team: >
.> ?
TeamMemberLink? M
>M N
(N O
requestO V
.V W

teamMemberW a
)a b
;b c
if 
( 
teamMemberLink 
is !
null" &
)& '
{ 
const 
string 
errorMsg %
=& '
$str( J
;J K
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return   
Result   
.   
Fail   "
(  " #
new  # &
Error  ' ,
(  , -
errorMsg  - 5
)  5 6
)  6 7
;  7 8
}!! 
var## 
createdTeamLink## 
=##  !
await##" '
_repository##( 3
.##3 4
TeamLinkRepository##4 F
.##F G
CreateAsync##G R
(##R S
teamMemberLink##S a
)##a b
;##b c
if%% 
(%% 
createdTeamLink%% 
is%%  "
null%%# '
)%%' (
{&& 
const'' 
string'' 
errorMsg'' %
=''& '
$str''( A
;''A B
_logger(( 
.(( 
LogError((  
(((  !
request((! (
,((( )
errorMsg((* 2
)((2 3
;((3 4
return)) 
Result)) 
.)) 
Fail)) "
())" #
new))# &
Error))' ,
()), -
errorMsg))- 5
)))5 6
)))6 7
;))7 8
}** 
var,, 
resultIsSuccess,, 
=,,  !
await,," '
_repository,,( 3
.,,3 4
SaveChangesAsync,,4 D
(,,D E
),,E F
>,,G H
$num,,I J
;,,J K
if.. 
(.. 
!.. 
resultIsSuccess..  
)..  !
{// 
const00 
string00 
errorMsg00 %
=00& '
$str00( A
;00A B
_logger11 
.11 
LogError11  
(11  !
request11! (
,11( )
errorMsg11* 2
)112 3
;113 4
return22 
Result22 
.22 
Fail22 "
(22" #
new22# &
Error22' ,
(22, -
errorMsg22- 5
)225 6
)226 7
;227 8
}33 
var55 
createdTeamLinkDTO55 "
=55# $
_mapper55% ,
.55, -
Map55- 0
<550 1
TeamMemberLinkDTO551 B
>55B C
(55C D
createdTeamLink55D S
)55S T
;55T U
if77 
(77 
createdTeamLinkDTO77 !
!=77" $
null77% )
)77) *
{88 
return99 
Result99 
.99 
Ok99  
(99  !
createdTeamLinkDTO99! 3
)993 4
;994 5
}:: 
else;; 
{<< 
const== 
string== 
errorMsg== %
===& '
$str==( I
;==I J
_logger>> 
.>> 
LogError>>  
(>>  !
request>>! (
,>>( )
errorMsg>>* 2
)>>2 3
;>>3 4
return?? 
Result?? 
.?? 
Fail?? "
(??" #
new??# &
Error??' ,
(??, -
errorMsg??- 5
)??5 6
)??6 7
;??7 8
}@@ 
}AA 	
}BB 
}CC ¢
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\Position\GetAll\GetAllPositionsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
Position& .
.. /
GetAll/ 5
{ 
public 

record  
GetAllPositionsQuery &
:' (
IRequest) 1
<1 2
Result2 8
<8 9
IEnumerable9 D
<D E
PositionDTOE P
>P Q
>Q R
>R S
;S T
} ¥
èD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\Position\GetAll\GetAllPositionsHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Team

! %
.

% &
Position

& .
.

. /
GetAll

/ 5
{ 
public 

class "
GetAllPositionsHandler '
:( )
IRequestHandler* 9
<9 : 
GetAllPositionsQuery: N
,N O
ResultP V
<V W
IEnumerableW b
<b c
PositionDTOc n
>n o
>o p
>p q
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public "
GetAllPositionsHandler %
(% &
IRepositoryWrapper& 8
repositoryWrapper9 J
,J K
IMapperL S
mapperT Z
,Z [
ILoggerService\ j
loggerk q
)q r
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
PositionDTO- 8
>8 9
>9 :
>: ;
Handle< B
(B C 
GetAllPositionsQueryC W
requestX _
,_ `
CancellationTokena r
cancellationToken	s Ñ
)
Ñ Ö
{ 	
var 
	positions 
= 
await !
_repositoryWrapper" 4
. 
PositionRepository #
. 
GetAllAsync 
( 
) 
; 
if 
( 
	positions 
is 
null !
)! "
{   
const!! 
string!! 
errorMsg!! %
=!!& '
$"!!( *
$str!!* C
"!!C D
;!!D E
_logger"" 
."" 
LogError""  
(""  !
request""! (
,""( )
errorMsg""* 2
)""2 3
;""3 4
return## 
Result## 
.## 
Fail## "
(##" #
new### &
Error##' ,
(##, -
errorMsg##- 5
)##5 6
)##6 7
;##7 8
}$$ 
return&& 
Result&& 
.&& 
Ok&& 
(&& 
_mapper&& $
.&&$ %
Map&&% (
<&&( )
IEnumerable&&) 4
<&&4 5
PositionDTO&&5 @
>&&@ A
>&&A B
(&&B C
	positions&&C L
)&&L M
)&&M N
;&&N O
}'' 	
}(( 
})) ë
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\Position\Create\CreatePositionQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
Create& ,
{ 
public 

record 
CreatePositionQuery %
(% &
PositionDTO& 1
position2 :
): ;
:< =
IRequest> F
<F G
ResultG M
<M N
PositionDTON Y
>Y Z
>Z [
;[ \
} ø
éD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\Position\Create\CreatePositionHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Team		! %
.		% &
Create		& ,
{

 
public 

class !
CreatePositionHandler &
:' (
IRequestHandler) 8
<8 9
CreatePositionQuery9 L
,L M
ResultN T
<T U
PositionDTOU `
>` a
>a b
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public !
CreatePositionHandler $
($ %
IMapper% ,
mapper- 3
,3 4
IRepositoryWrapper5 G

repositoryH R
,R S
ILoggerServiceT b
loggerc i
)i j
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
PositionDTO! ,
>, -
>- .
Handle/ 5
(5 6
CreatePositionQuery6 I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 	
var 
newPosition 
= 
await #
_repository$ /
./ 0
PositionRepository0 B
.B C
CreateAsyncC N
(N O
newO R
	PositionsS \
(\ ]
)] ^
{ 
Position 
= 
request "
." #
position# +
.+ ,
Position, 4
} 
) 
; 
try 
{   
await!! 
_repository!! !
.!!! "
SaveChangesAsync!!" 2
(!!2 3
)!!3 4
;!!4 5
}"" 
catch## 
(## 
	Exception## 
ex## 
)##  
{$$ 
_logger%% 
.%% 
LogError%%  
(%%  !
request%%! (
,%%( )
ex%%* ,
.%%, -
Message%%- 4
)%%4 5
;%%5 6
return&& 
Result&& 
.&& 
Fail&& "
(&&" #
ex&&# %
.&&% &
Message&&& -
)&&- .
;&&. /
}'' 
return)) 
Result)) 
.)) 
Ok)) 
()) 
_mapper)) $
.))$ %
Map))% (
<))( )
PositionDTO))) 4
>))4 5
())5 6
newPosition))6 A
)))A B
)))B C
;))C D
}** 	
}++ 
},, ¯
ÅD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\GetById\GetByIdTeamQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
GetById& -
{ 
public 

record 
GetByIdTeamQuery "
(" #
int# &
Id' )
)) *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =
TeamMemberDTO= J
>J K
>K L
;L M
}		 ‘
ÉD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\GetById\GetByIdTeamHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
GetById& -
{ 
public 

class 
GetByIdTeamHandler #
:$ %
IRequestHandler& 5
<5 6
GetByIdTeamQuery6 F
,F G
ResultH N
<N O
TeamMemberDTOO \
>\ ]
>] ^
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
GetByIdTeamHandler !
(! "
IRepositoryWrapper" 4
repositoryWrapper5 F
,F G
IMapperH O
mapperP V
,V W
ILoggerServiceX f
loggerg m
)m n
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
TeamMemberDTO! .
>. /
>/ 0
Handle1 7
(7 8
GetByIdTeamQuery8 H
requestI P
,P Q
CancellationTokenR c
cancellationTokend u
)u v
{ 	
var 
team 
= 
await 
_repositoryWrapper /
. 
TeamRepository 
. #
GetSingleOrDefaultAsync (
(( )
	predicate 
: 
p  
=>! #
p$ %
.% &
Id& (
==) +
request, 3
.3 4
Id4 6
,6 7
include   
:   
x   
=>   !
x  " #
.  # $
Include  $ +
(  + ,
x  , -
=>  . 0
x  1 2
.  2 3
TeamMemberLinks  3 B
)  B C
.!! 
Include!! 
(!! 
x!! 
=>!! !
x!!" #
.!!# $
	Positions!!$ -
)!!- .
)!!. /
;!!/ 0
if## 
(## 
team## 
is## 
null## 
)## 
{$$ 
string%% 
errorMsg%% 
=%%  !
$"%%" $
$str%%$ P
{%%P Q
request%%Q X
.%%X Y
Id%%Y [
}%%[ \
"%%\ ]
;%%] ^
_logger&& 
.&& 
LogError&&  
(&&  !
request&&! (
,&&( )
errorMsg&&* 2
)&&2 3
;&&3 4
return'' 
Result'' 
.'' 
Fail'' "
(''" #
new''# &
Error''' ,
('', -
errorMsg''- 5
)''5 6
)''6 7
;''7 8
}(( 
return** 
Result** 
.** 
Ok** 
(** 
_mapper** $
.**$ %
Map**% (
<**( )
TeamMemberDTO**) 6
>**6 7
(**7 8
team**8 <
)**< =
)**= >
;**> ?
}++ 	
},, 
}-- Î
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\GetAll\GetAllTeamQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
GetAll& ,
{ 
public 

record 
GetAllTeamQuery !
:" #
IRequest$ ,
<, -
Result- 3
<3 4
IEnumerable4 ?
<? @
TeamMemberDTO@ M
>M N
>N O
>O P
;P Q
} ‰
ÅD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\GetAll\GetAllTeamHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Team		! %
.		% &
GetAll		& ,
{

 
public 

class 
GetAllTeamHandler "
:# $
IRequestHandler% 4
<4 5
GetAllTeamQuery5 D
,D E
ResultF L
<L M
IEnumerableM X
<X Y
TeamMemberDTOY f
>f g
>g h
>h i
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
GetAllTeamHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
IMapperG N
mapperO U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
TeamMemberDTO- :
>: ;
>; <
>< =
Handle> D
(D E
GetAllTeamQueryE T
requestU \
,\ ]
CancellationToken^ o
cancellationToken	p Å
)
Å Ç
{ 	
var 
team 
= 
await 
_repositoryWrapper /
. 
TeamRepository 
. 
GetAllAsync 
( 
include $
:$ %
x& '
=>( *
x+ ,
., -
Include- 4
(4 5
x5 6
=>7 9
x: ;
.; <
	Positions< E
)E F
.F G
IncludeG N
(N O
xO P
=>Q S
xT U
.U V
TeamMemberLinksV e
)e f
)f g
;g h
if 
( 
team 
is 
null 
) 
{ 
const   
string   
errorMsg   %
=  & '
$"  ( *
$str  * >
"  > ?
;  ? @
_logger!! 
.!! 
LogError!!  
(!!  !
request!!! (
,!!( )
errorMsg!!* 2
)!!2 3
;!!3 4
return"" 
Result"" 
."" 
Fail"" "
(""" #
new""# &
Error""' ,
("", -
errorMsg""- 5
)""5 6
)""6 7
;""7 8
}## 
return%% 
Result%% 
.%% 
Ok%% 
(%% 
_mapper%% $
.%%$ %
Map%%% (
<%%( )
IEnumerable%%) 4
<%%4 5
TeamMemberDTO%%5 B
>%%B C
>%%C D
(%%D E
team%%E I
)%%I J
)%%J K
;%%K L
}&& 	
}'' 
}(( ¯
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\GetAllMain\GetAllMainTeamQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Team! %
.% &
GetAll& ,
{ 
public 

record 
GetAllMainTeamQuery %
:& '
IRequest( 0
<0 1
Result1 7
<7 8
IEnumerable8 C
<C D
TeamMemberDTOD Q
>Q R
>R S
>S T
;T U
} ç
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Team\GetAllMain\GetAllMainTeamHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Team		! %
.		% &
GetAll		& ,
{

 
public 

class !
GetAllMainTeamHandler &
:' (
IRequestHandler) 8
<8 9
GetAllMainTeamQuery9 L
,L M
ResultN T
<T U
IEnumerableU `
<` a
TeamMemberDTOa n
>n o
>o p
>p q
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public !
GetAllMainTeamHandler $
($ %
IRepositoryWrapper% 7
repositoryWrapper8 I
,I J
IMapperK R
mapperS Y
,Y Z
ILoggerService[ i
loggerj p
)p q
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
TeamMemberDTO- :
>: ;
>; <
>< =
Handle> D
(D E
GetAllMainTeamQueryE X
requestY `
,` a
CancellationTokenb s
cancellationToken	t Ö
)
Ö Ü
{ 	
var 
team 
= 
await 
_repositoryWrapper /
. 
TeamRepository 
. 
GetAllAsync 
( 
include $
:$ %
x& '
=>( *
x+ ,
., -
Where- 2
(2 3
x3 4
=>5 7
x8 9
.9 :
IsMain: @
)@ A
.A B
IncludeB I
(I J
xJ K
=>L N
xO P
.P Q
	PositionsQ Z
)Z [
.[ \
Include\ c
(c d
xd e
=>f h
xi j
.j k
TeamMemberLinksk z
)z {
){ |
;| }
if 
( 
team 
is 
null 
) 
{ 
const   
string   
errorMsg   %
=  & '
$"  ( *
$str  * >
"  > ?
;  ? @
_logger!! 
.!! 
LogError!!  
(!!  !
request!!! (
,!!( )
errorMsg!!* 2
)!!2 3
;!!3 4
return"" 
Result"" 
."" 
Fail"" "
(""" #
new""# &
Error""' ,
("", -
errorMsg""- 5
)""5 6
)""6 7
;""7 8
}## 
return%% 
Result%% 
.%% 
Ok%% 
(%% 
_mapper%% $
.%%$ %
Map%%% (
<%%( )
IEnumerable%%) 4
<%%4 5
TeamMemberDTO%%5 B
>%%B C
>%%C D
(%%D E
team%%E I
)%%I J
)%%J K
;%%K L
}&& 	
}'' 
}(( Ë
°D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetParsed\GetParsedTextForAdminPreviewCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Text, 0
.0 1
	GetParsed1 :
{ 
public 

record /
#GetParsedTextForAdminPreviewCommand 5
(5 6
string6 <
textToParse= H
)H I
:J K
IRequestL T
<T U
ResultU [
<[ \
string\ b
>b c
>c d
{ 
} 
}		 ‰
ûD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetParsed\GetParsedTextAdminPreviewHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Text, 0
.0 1
	GetParsed1 :
{ 
public 

class ,
 GetParsedTextAdminPreviewHandler 1
:2 3
IRequestHandler4 C
<C D/
#GetParsedTextForAdminPreviewCommandD g
,g h
Resulti o
<o p
stringp v
>v w
>w x
{		 
private

 
readonly

 
ITextService

 %
_textService

& 2
;

2 3
public ,
 GetParsedTextAdminPreviewHandler /
(/ 0
ITextService0 <
textService= H
)H I
{ 	
_textService 
= 
textService &
;& '
} 	
public 
async 
Task 
< 
Result  
<  !
string! '
>' (
>( )
Handle* 0
(0 1/
#GetParsedTextForAdminPreviewCommand1 T
requestU \
,\ ]
CancellationToken^ o
cancellationToken	p Å
)
Å Ç
{ 	
string 
? 

parsedText 
=  
await! &
_textService' 3
.3 4
AddTermsTag4 ?
(? @
request@ G
.G H
textToParseH S
)S T
;T U
return 

parsedText 
==  
null! %
?& '
Result( .
.. /
Fail/ 3
(3 4
new4 7
Error8 =
(= >
$str> `
)` a
)a b
:c d
Resulte k
.k l
Okl n
(n o

parsedTexto y
)y z
;z {
} 	
} 
} ÿ
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetByStreetcodeId\GetTextByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Text, 0
.0 1
GetByStreetcodeId1 B
;B C
public 
record &
GetTextByStreetcodeIdQuery (
(( )
int) ,
StreetcodeId- 9
)9 :
:; <
IRequest= E
<E F
ResultF L
<L M
TextDTOM T
?T U
>U V
>V W
;W Xê&
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetByStreetcodeId\GetTextByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Text, 0
.0 1
GetByStreetcodeId1 B
;B C
public 
class (
GetTextByStreetcodeIdHandler )
:* +
IRequestHandler, ;
<; <&
GetTextByStreetcodeIdQuery< V
,V W
ResultX ^
<^ _
TextDTO_ f
?f g
>g h
>h i
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ITextService !
_textService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
(
GetTextByStreetcodeIdHandler '
(' (
IRepositoryWrapper( :
repositoryWrapper; L
,L M
IMapperN U
mapperV \
,\ ]
ITextService^ j
textServicek v
,v w
ILoggerService	x Ü
logger
á ç
)
ç é
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_textService 
= 
textService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TextDTO $
?$ %
>% &
>& '
Handle( .
(. /&
GetTextByStreetcodeIdQuery/ I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 
var 
text 
= 
await 
_repositoryWrapper +
.+ ,
TextRepository, :
. "
GetFirstOrDefaultAsync #
(# $
text$ (
=>) +
text, 0
.0 1
StreetcodeId1 =
==> @
requestA H
.H I
StreetcodeIdI U
)U V
;V W
if!! 

(!! 
text!! 
is!! 
null!! 
)!! 
{"" 	
if## 
(## 
await## 
_repositoryWrapper## (
.##( ) 
StreetcodeRepository##) =
.$$ "
GetFirstOrDefaultAsync$$ (
($$( )
s$$) *
=>$$+ -
s$$. /
.$$/ 0
Id$$0 2
==$$3 5
request$$6 =
.$$= >
StreetcodeId$$> J
)$$J K
==$$L N
null$$O S
)$$S T
{%% 
string&& 
errorMsg&& 
=&&  !
$"&&" $
$str&&$ W
{&&W X
request&&X _
.&&_ `
StreetcodeId&&` l
}&&l m
$str	&&m î
"
&&î ï
;
&&ï ñ
_logger'' 
.'' 
LogError''  
(''  !
request''! (
,''( )
errorMsg''* 2
)''2 3
;''3 4
return(( 
Result(( 
.(( 
Fail(( "
(((" #
new((# &
Error((' ,
(((, -
errorMsg((- 5
)((5 6
)((6 7
;((7 8
})) 
}** 	

NullResult,, 
<,, 
TextDTO,, 
?,, 
>,, 
result,, #
=,,$ %
new,,& )

NullResult,,* 4
<,,4 5
TextDTO,,5 <
?,,< =
>,,= >
(,,> ?
),,? @
;,,@ A
if-- 

(-- 
text-- 
!=-- 
null-- 
)-- 
{.. 	
text// 
.// 
TextContent// 
=// 
await// $
_textService//% 1
.//1 2
AddTermsTag//2 =
(//= >
text//> B
?//B C
.//C D
TextContent//D O
??//P R
$str//S U
)//U V
;//V W
result00 
.00 
	WithValue00 
(00 
_mapper00 $
.00$ %
Map00% (
<00( )
TextDTO00) 0
?000 1
>001 2
(002 3
text003 7
)007 8
)008 9
;009 :
}11 	
return33 
result33 
;33 
}44 
}55 ó
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetById\GetTextByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Text, 0
.0 1
GetById1 8
;8 9
public 
record 
GetTextByIdQuery 
( 
int "
Id# %
)% &
:' (
IRequest) 1
<1 2
Result2 8
<8 9
TextDTO9 @
>@ A
>A B
;B C©
éD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetById\GetTextByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Text, 0
.0 1
GetById1 8
;8 9
public

 
class

 
GetTextByIdHandler

 
:

  !
IRequestHandler

" 1
<

1 2
GetTextByIdQuery

2 B
,

B C
Result

D J
<

J K
TextDTO

K R
>

R S
>

S T
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetTextByIdHandler 
( 
IRepositoryWrapper 0
repositoryWrapper1 B
,B C
IMapperD K
mapperL R
,R S
ILoggerServiceT b
loggerc i
)i j
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TextDTO $
>$ %
>% &
Handle' -
(- .
GetTextByIdQuery. >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 
var 
text 
= 
await 
_repositoryWrapper +
.+ ,
TextRepository, :
.: ;"
GetFirstOrDefaultAsync; Q
(Q R
fR S
=>T V
fW X
.X Y
IdY [
==\ ^
request_ f
.f g
Idg i
)i j
;j k
if 

( 
text 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  L
{L M
requestM T
.T U
IdU W
}W X
"X Y
;Y Z
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
}   	
return"" 
Result"" 
."" 
Ok"" 
("" 
_mapper""  
.""  !
Map""! $
<""$ %
TextDTO""% ,
>"", -
(""- .
text"". 2
)""2 3
)""3 4
;""4 5
}## 
}$$ ç
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetAll\GetAllTextsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Text, 0
.0 1
GetAll1 7
;7 8
public 
record 
GetAllTextsQuery 
:  
IRequest! )
<) *
Result* 0
<0 1
IEnumerable1 <
<< =
TextDTO= D
>D E
>E F
>F G
;G HÙ
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Text\GetAll\GetAllTextsHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !

Streetcode

! +
.

+ ,
Text

, 0
.

0 1
GetAll

1 7
;

7 8
public 
class 
GetAllTextsHandler 
:  !
IRequestHandler" 1
<1 2
GetAllTextsQuery2 B
,B C
ResultD J
<J K
IEnumerableK V
<V W
TextDTOW ^
>^ _
>_ `
>` a
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAllTextsHandler 
( 
IRepositoryWrapper 0
repositoryWrapper1 B
,B C
IMapperD K
mapperL R
,R S
ILoggerServiceT b
loggerc i
)i j
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
TextDTO) 0
>0 1
>1 2
>2 3
Handle4 :
(: ;
GetAllTextsQuery; K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 
var 
texts 
= 
await 
_repositoryWrapper ,
., -
TextRepository- ;
.; <
GetAllAsync< G
(G H
)H I
;I J
if 

( 
texts 
is 
null 
) 
{ 	
const 
string 
errorMsg !
=" #
$"$ &
$str& :
": ;
;; <
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$  
.$$  !
Map$$! $
<$$$ %
IEnumerable$$% 0
<$$0 1
TextDTO$$1 8
>$$8 9
>$$9 :
($$: ;
texts$$; @
)$$@ A
)$$A B
;$$B C
}%% 
}&& ó
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Term\GetById\GetTermByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Term, 0
.0 1
GetById1 8
;8 9
public 
record 
GetTermByIdQuery 
( 
int "
Id# %
)% &
:' (
IRequest) 1
<1 2
Result2 8
<8 9
TermDTO9 @
>@ A
>A B
;B C©
éD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Term\GetById\GetTermByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Term, 0
.0 1
GetById1 8
;8 9
public

 
class

 
GetTermByIdHandler

 
:

  !
IRequestHandler

" 1
<

1 2
GetTermByIdQuery

2 B
,

B C
Result

D J
<

J K
TermDTO

K R
>

R S
>

S T
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetTermByIdHandler 
( 
IRepositoryWrapper 0
repositoryWrapper1 B
,B C
IMapperD K
mapperL R
,R S
ILoggerServiceT b
loggerc i
)i j
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TermDTO $
>$ %
>% &
Handle' -
(- .
GetTermByIdQuery. >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 
var 
term 
= 
await 
_repositoryWrapper +
.+ ,
TermRepository, :
.: ;"
GetFirstOrDefaultAsync; Q
(Q R
fR S
=>T V
fW X
.X Y
IdY [
==\ ^
request_ f
.f g
Idg i
)i j
;j k
if 

( 
term 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  L
{L M
requestM T
.T U
IdU W
}W X
"X Y
;Y Z
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
}   	
return"" 
Result"" 
."" 
Ok"" 
("" 
_mapper""  
.""  !
Map""! $
<""$ %
TermDTO""% ,
>"", -
(""- .
term"". 2
)""2 3
)""3 4
;""4 5
}## 
}$$ ö
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Term\GetAll\GetAllTermsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Term, 0
.0 1
GetAll1 7
{ 
public 

record 
GetAllTermsQuery "
:# $
IRequest% -
<- .
Result. 4
<4 5
IEnumerable5 @
<@ A
TermDTOA H
>H I
>I J
>J K
;K L
} Ö
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Term\GetAll\GetAllTermsHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !

Streetcode

! +
.

+ ,
Term

, 0
.

0 1
GetAll

1 7
{ 
public 

class 
GetAllTermsHandler #
:$ %
IRequestHandler& 5
<5 6
GetAllTermsQuery6 F
,F G
ResultH N
<N O
IEnumerableO Z
<Z [
TermDTO[ b
>b c
>c d
>d e
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
GetAllTermsHandler !
(! "
IRepositoryWrapper" 4
repositoryWrapper5 F
,F G
IMapperH O
mapperP V
,V W
ILoggerServiceX f
loggerg m
)m n
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
TermDTO- 4
>4 5
>5 6
>6 7
Handle8 >
(> ?
GetAllTermsQuery? O
requestP W
,W X
CancellationTokenY j
cancellationTokenk |
)| }
{ 	
var 
terms 
= 
await 
_repositoryWrapper 0
.0 1
TermRepository1 ?
.? @
GetAllAsync@ K
(K L
)L M
;M N
if 
( 
terms 
is 
null 
) 
{ 
const 
string 
errorMsg %
=& '
$"( *
$str* >
"> ?
;? @
_logger   
.   
LogError    
(    !
request  ! (
,  ( )
errorMsg  * 2
)  2 3
;  3 4
return!! 
Result!! 
.!! 
Fail!! "
(!!" #
new!!# &
Error!!' ,
(!!, -
errorMsg!!- 5
)!!5 6
)!!6 7
;!!7 8
}"" 
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$ $
.$$$ %
Map$$% (
<$$( )
IEnumerable$$) 4
<$$4 5
TermDTO$$5 <
>$$< =
>$$= >
($$> ?
terms$$? D
)$$D E
)$$E F
;$$F G
}%% 	
}&& 
}'' Í
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetShortById\GetStreetcodeShortByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetShortById7 C
{ 
public 

record '
GetStreetcodeShortByIdQuery -
(- .
int. 1
id2 4
)4 5
:6 7
IRequest8 @
<@ A
ResultA G
<G H
StreetcodeShortDTOH Z
>Z [
>[ \
{ 
}		 
}

 ù
§D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetShortById\GetStreetcodeShortByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetShortById7 C
{		 
public

 

class

 )
GetStreetcodeShortByIdHandler

 .
:

/ 0
IRequestHandler

1 @
<

@ A'
GetStreetcodeShortByIdQuery

A \
,

\ ]
Result

^ d
<

d e
StreetcodeShortDTO

e w
>

w x
>

x y
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public )
GetStreetcodeShortByIdHandler ,
(, -
IMapper- 4
mapper5 ;
,; <
IRepositoryWrapper= O

repositoryP Z
,Z [
ILoggerService\ j
loggerk q
)q r
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
StreetcodeShortDTO! 3
>3 4
>4 5
Handle6 <
(< ='
GetStreetcodeShortByIdQuery= X
requestY `
,` a
CancellationTokenb s
cancellationToken	t Ö
)
Ö Ü
{ 	
var 

streetcode 
= 
await "
_repository# .
.. / 
StreetcodeRepository/ C
.C D"
GetFirstOrDefaultAsyncD Z
(Z [
st[ ]
=>^ `
sta c
.c d
Idd f
==g i
requestj q
.q r
idr t
)t u
;u v
if 
( 

streetcode 
== 
null "
)" #
{ 
const 
string 
errorMsg %
=& '
$str( F
;F G
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return 
Result 
. 
Fail "
(" #
new# &
Error' ,
(, -
errorMsg- 5
)5 6
)6 7
;7 8
}   
var"" 
streetcodeShortDTO"" "
=""# $
_mapper""% ,
."", -
Map""- 0
<""0 1
StreetcodeShortDTO""1 C
>""C D
(""D E

streetcode""E O
)""O P
;""P Q
if$$ 
($$ 
streetcodeShortDTO$$ !
==$$" $
null$$% )
)$$) *
{%% 
const&& 
string&& 
errorMsg&& %
=&&& '
$str&&( K
;&&K L
_logger'' 
.'' 
LogError''  
(''  !
request''! (
,''( )
errorMsg''* 2
)''2 3
;''3 4
return(( 
Result(( 
.(( 
Fail(( "
(((" #
new((# &
Error((' ,
(((, -
errorMsg((- 5
)((5 6
)((6 7
;((7 8
})) 
return++ 
Result++ 
.++ 
Ok++ 
(++ 
streetcodeShortDTO++ /
)++/ 0
;++0 1
},, 	
}-- 
}.. ˇ
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetCount\GetStreetcodesCountQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetCount7 ?
{ 
public 

record $
GetStreetcodesCountQuery *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =
int= @
>@ A
>A B
;B C
} ”
úD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetCount\GetStreetcodesCountHander.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetCount7 ?
{ 
public 

class %
GetStreetcodesCountHander *
:+ ,
IRequestHandler- <
<< =$
GetStreetcodesCountQuery= U
,U V
Result 
< 
int 
> 
> 
{ 
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public %
GetStreetcodesCountHander (
(( )
IRepositoryWrapper) ;
repositoryWrapper< M
,M N
IMapperO V
mapperW ]
,] ^
ILoggerService_ m
loggern t
)t u
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
int! $
>$ %
>% &
Handle' -
(- .$
GetStreetcodesCountQuery. F
requestG N
,N O
CancellationTokenP a
cancellationTokenb s
)s t
{ 	
var 
streetcodes 
= 
await #
_repositoryWrapper$ 6
.6 7 
StreetcodeRepository7 K
.K L
GetAllAsyncL W
(W X
)X Y
;Y Z
if   
(   
streetcodes   
!=   
null   #
)  # $
{!! 
return"" 
Result"" 
."" 
Ok""  
(""  !
streetcodes""! ,
."", -
Count""- 2
(""2 3
)""3 4
)""4 5
;""5 6
}## 
const%% 
string%% 
errorMsg%% !
=%%" #
$str%%$ >
;%%> ?
_logger&& 
.&& 
LogError&& 
(&& 
request&& $
,&&$ %
errorMsg&&& .
)&&. /
;&&/ 0
return'' 
Result'' 
.'' 
Fail'' 
('' 
errorMsg'' '
)''' (
;''( )
}(( 	
})) 
}** Ü
∏D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetByTransliterationUrl\GetStreetcodeByTransliterationUrlQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7#
GetByTransliterationUrl7 N
{ 
public 

record 2
&GetStreetcodeByTransliterationUrlQuery 8
(8 9
string9 ?
url@ C
)C D
:E F
IRequestG O
<O P
ResultP V
<V W
StreetcodeDTOW d
>d e
>e f
;f g
} „"
∫D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetByTransliterationUrl\GetStreetcodeByTransliterationUrlHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !

Streetcode

! +
.

+ ,

Streetcode

, 6
.

6 7#
GetByTransliterationUrl

7 N
{ 
public 
class	 4
(GetStreetcodeByTransliterationUrlHandler 7
:8 9
IRequestHandler: I
<I J2
&GetStreetcodeByTransliterationUrlQueryJ p
,p q
Resultr x
<x y
StreetcodeDTO	y Ü
>
Ü á
>
á à
{ 
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 4
(GetStreetcodeByTransliterationUrlHandler 7
(7 8
IRepositoryWrapper8 J

repositoryK U
,U V
IMapperW ^
mapper_ e
,e f
ILoggerServiceg u
loggerv |
)| }
{ 	
_repository 
= 

repository $
;$ %
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
StreetcodeDTO! .
>. /
>/ 0
Handle1 7
(7 82
&GetStreetcodeByTransliterationUrlQuery8 ^
request_ f
,f g
CancellationTokenh y
cancellationToken	z ã
)
ã å
{ 	
var 

streetcode 
= 
await "
_repository# .
.. / 
StreetcodeRepository/ C
. "
GetFirstOrDefaultAsync '
(' (
	predicate 
: 
st !
=>" $
st% '
.' (
TransliterationUrl( :
==; =
request> E
.E F
urlF I
)I J
;J K
if 
( 

streetcode 
== 
null "
)" #
{   
string!! 
errorMsg!! 
=!!  !
$"!!" $
$str!!$ S
{!!S T
request!!T [
.!![ \
url!!\ _
}!!_ `
"!!` a
;!!a b
_logger"" 
."" 
LogError""  
(""  !
request""! (
,""( )
errorMsg""* 2
)""2 3
;""3 4
return## 
new## 
Error##  
(##  !
errorMsg##! )
)##) *
;##* +
}$$ 
var&& 

tagIndexed&& 
=&& 
await&& "
_repository&&# .
.&&. /(
StreetcodeTagIndexRepository&&/ K
.'', -
GetAllAsync''- 8
(''8 9
t((0 1
=>((2 4
t((5 6
.((6 7
StreetcodeId((7 C
==((D F

streetcode((G Q
.((Q R
Id((R T
,((T U
include))0 7
:))7 8
q))9 :
=>)); =
q))> ?
.))? @
Include))@ G
())G H
ti))H J
=>))K M
ti))N P
.))P Q
Tag))Q T
)))T U
)))U V
;))V W
var++ 
streetcodeDTO++ 
=++ 
_mapper++  '
.++' (
Map++( +
<+++ ,
StreetcodeDTO++, 9
>++9 :
(++: ;

streetcode++; E
)++E F
;++F G
streetcodeDTO,, 
.,, 
Tags,, 
=,,  
_mapper,,! (
.,,( )
Map,,) ,
<,,, -
List,,- 1
<,,1 2
StreetcodeTagDTO,,2 B
>,,B C
>,,C D
(,,D E

tagIndexed,,E O
),,O P
;,,P Q
return-- 
Result-- 
.-- 
Ok-- 
(-- 
streetcodeDTO-- *
)--* +
;--+ ,
}.. 	
}// 
}00 ƒ
ûD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetByIndex\GetStreetcodeByIndexQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7

GetByIndex7 A
;A B
public 
record %
GetStreetcodeByIndexQuery '
(' (
int( +
Index, 1
)1 2
:3 4
IRequest5 =
<= >
Result> D
<D E
StreetcodeDTOE R
>R S
>S T
;T Uø
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetByIndex\GetStreetcodeByIndexHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !

Streetcode		! +
.		+ ,

Streetcode		, 6
.		6 7

GetByIndex		7 A
;		A B
public 
class '
GetStreetcodeByIndexHandler (
:) *
IRequestHandler+ :
<: ;%
GetStreetcodeByIndexQuery; T
,T U
ResultV \
<\ ]
StreetcodeDTO] j
>j k
>k l
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
'
GetStreetcodeByIndexHandler &
(& '
IRepositoryWrapper' 9
repositoryWrapper: K
,K L
IMapperM T
mapperU [
,[ \
ILoggerService] k
loggerl r
)r s
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
StreetcodeDTO *
>* +
>+ ,
Handle- 3
(3 4%
GetStreetcodeByIndexQuery4 M
requestN U
,U V
CancellationTokenW h
cancellationTokeni z
)z {
{ 
var 

streetcode 
= 
await 
_repositoryWrapper 1
.1 2 
StreetcodeRepository2 F
.F G"
GetFirstOrDefaultAsyncG ]
(] ^
	predicate 
: 
st 
=> 
st 
.  
Index  %
==& (
request) 0
.0 1
Index1 6
,6 7
include 
: 
source 
=> 
source %
.% &
Include& -
(- .
l. /
=>0 2
l3 4
.4 5
Tags5 9
)9 :
): ;
;; <
if 

( 

streetcode 
is 
null 
) 
{ 	
string   
errorMsg   
=   
$"    
$str    U
{  U V
request  V ]
.  ] ^
Index  ^ c
}  c d
"  d e
;  e f
_logger!! 
.!! 
LogError!! 
(!! 
request!! $
,!!$ %
errorMsg!!& .
)!!. /
;!!/ 0
return"" 
Result"" 
."" 
Fail"" 
("" 
new"" "
Error""# (
(""( )
errorMsg"") 1
)""1 2
)""2 3
;""3 4
}## 	
return%% 
Result%% 
.%% 
Ok%% 
(%% 
_mapper%%  
.%%  !
Map%%! $
<%%$ %
StreetcodeDTO%%% 2
>%%2 3
(%%3 4

streetcode%%4 >
)%%> ?
)%%? @
;%%@ A
}&& 
}'' µ
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetById\GetStreetcodeByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetById7 >
;> ?
public 
record "
GetStreetcodeByIdQuery $
($ %
int% (
Id) +
)+ ,
:- .
IRequest/ 7
<7 8
Result8 >
<> ?
StreetcodeDTO? L
>L M
>M N
;N O¬"
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetById\GetStreetcodeByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetById7 >
;> ?
public 
class $
GetStreetcodeByIdHandler %
:& '
IRequestHandler( 7
<7 8"
GetStreetcodeByIdQuery8 N
,N O
ResultP V
<V W
StreetcodeDTOW d
>d e
>e f
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
$
GetStreetcodeByIdHandler #
(# $
IRepositoryWrapper$ 6
repositoryWrapper7 H
,H I
IMapperJ Q
mapperR X
,X Y
ILoggerServiceZ h
loggeri o
)o p
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
StreetcodeDTO *
>* +
>+ ,
Handle- 3
(3 4"
GetStreetcodeByIdQuery4 J
requestK R
,R S
CancellationTokenT e
cancellationTokenf w
)w x
{ 
var 

streetcode 
= 
await 
_repositoryWrapper 1
.1 2 
StreetcodeRepository2 F
.F G"
GetFirstOrDefaultAsyncG ]
(] ^
	predicate 
: 
st 
=> 
st 
.  
Id  "
==# %
request& -
.- .
Id. 0
)0 1
;1 2
if 

( 

streetcode 
is 
null 
) 
{   	
string!! 
errorMsg!! 
=!! 
$"!!  
$str!!  R
{!!R S
request!!S Z
.!!Z [
Id!![ ]
}!!] ^
"!!^ _
;!!_ `
_logger"" 
."" 
LogError"" 
("" 
request"" $
,""$ %
errorMsg""& .
)"". /
;""/ 0
return## 
Result## 
.## 
Fail## 
(## 
new## "
Error### (
(##( )
errorMsg##) 1
)##1 2
)##2 3
;##3 4
}$$ 	
var&& 

tagIndexed&& 
=&& 
await&& 
_repositoryWrapper&& 1
.&&1 2(
StreetcodeTagIndexRepository&&2 N
.''( )
GetAllAsync'') 4
(''4 5
t((, -
=>((. 0
t((1 2
.((2 3
StreetcodeId((3 ?
==((@ B
request((C J
.((J K
Id((K M
,((M N
include)), 3
:))3 4
q))5 6
=>))7 9
q)): ;
.)); <
Include))< C
())C D
ti))D F
=>))G I
ti))J L
.))L M
Tag))M P
)))P Q
)))Q R
;))R S
var** 
streetcodeDto** 
=** 
_mapper** #
.**# $
Map**$ '
<**' (
StreetcodeDTO**( 5
>**5 6
(**6 7

streetcode**7 A
)**A B
;**B C
streetcodeDto++ 
.++ 
Tags++ 
=++ 
_mapper++ $
.++$ %
Map++% (
<++( )
List++) -
<++- .
StreetcodeTagDTO++. >
>++> ?
>++? @
(++@ A

tagIndexed++A K
)++K L
;++L M
return-- 
Result-- 
.-- 
Ok-- 
(-- 
streetcodeDto-- &
)--& '
;--' (
}.. 
}// ≈
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetByFilter\GetStreetcodeByFilterQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetByFilter7 B
{ 
public 

record 
class &
GetStreetcodeByFilterQuery 2
(2 3&
StreetcodeFilterRequestDTO3 M
FilterN T
)T U
:V W
IRequestX `
<` a
Resulta g
<g h
Listh l
<l m&
StreetcodeFilterResultDTO	m Ü
>
Ü á
>
á à
>
à â
;
â ä
}		 í
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetByFilter\GetStreetcodeByFilterHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetByFilter7 B
{ 
public 

class (
GetStreetcodeByFilterHandler -
:. /
IRequestHandler0 ?
<? @&
GetStreetcodeByFilterQuery@ Z
,Z [
Result\ b
<b c
Listc g
<g h&
StreetcodeFilterResultDTO	h Å
>
Å Ç
>
Ç É
>
É Ñ
{ 
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public (
GetStreetcodeByFilterHandler +
(+ ,
IRepositoryWrapper, >
repositoryWrapper? P
,P Q
ILoggerServiceR `
loggera g
)g h
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &%
StreetcodeFilterResultDTO& ?
>? @
>@ A
>A B
HandleC I
(I J&
GetStreetcodeByFilterQueryJ d
requeste l
,l m
CancellationTokenn 
cancellationToken
Ä ë
)
ë í
{ 	
string 
searchQuery 
=  
request! (
.( )
Filter) /
./ 0
SearchQuery0 ;
;; <
var 
results 
= 
new 
List "
<" #%
StreetcodeFilterResultDTO# <
>< =
(= >
)> ?
;? @
var   
streetcodes   
=   
await   #
_repositoryWrapper  $ 6
.  6 7 
StreetcodeRepository  7 K
.  K L
GetAllAsync  L W
(  W X
	predicate!! 
:!! 
x!! 
=>!!  
("" 	
x""	 

.""
 
Status"" 
=="" 
DAL"" 
."" 
Enums"" 
."" 
StreetcodeStatus"" /
.""/ 0
	Published""0 9
)""9 :
&&""; =
(## 	
x##	 

.##
 
Title## 
.## 
Contains## 
(## 
searchQuery## %
)##% &
||##' )
($$ 	
x$$	 

.$$
 
Alias$$ 
!=$$ 
null$$ 
&&$$ 
x$$ 
.$$ 
Alias$$ #
.$$# $
Contains$$$ ,
($$, -
searchQuery$$- 8
)$$8 9
)$$9 :
||$$; =
x%% 	
.%%	 

Teaser%%
 
.%% 
Contains%% 
(%% 
searchQuery%% %
)%%% &
)%%& '
)%%' (
;%%( )
foreach'' 
('' 
var'' 

streetcode'' #
in''$ &
streetcodes''' 2
)''2 3
{(( 
if)) 
()) 

streetcode)) 
.)) 
Title)) $
.))$ %
Contains))% -
())- .
searchQuery)). 9
,))9 :
StringComparison)); K
.))K L
OrdinalIgnoreCase))L ]
)))] ^
)))^ _
{** 
results++ 
.++ 
Add++ 
(++  
CreateFilterResult++  2
(++2 3

streetcode++3 =
,++= >

streetcode++? I
.++I J
Title++J O
)++O P
)++P Q
;++Q R
continue,, 
;,, 
}-- 
if// 
(// 
!// 
string// 
.// 
IsNullOrEmpty// )
(//) *

streetcode//* 4
.//4 5
Alias//5 :
)//: ;
&&//< >

streetcode//? I
.//I J
Alias//J O
.//O P
Contains//P X
(//X Y
searchQuery//Y d
,//d e
StringComparison//f v
.//v w
OrdinalIgnoreCase	//w à
)
//à â
)
//â ä
{00 
results11 
.11 
Add11 
(11  
CreateFilterResult11  2
(112 3

streetcode113 =
,11= >

streetcode11? I
.11I J
Alias11J O
)11O P
)11P Q
;11Q R
continue22 
;22 
}33 
if55 
(55 

streetcode55 
.55 
Teaser55 %
.55% &
Contains55& .
(55. /
searchQuery55/ :
,55: ;
StringComparison55< L
.55L M
OrdinalIgnoreCase55M ^
)55^ _
)55_ `
{66 
results77 
.77 
Add77 
(77  
CreateFilterResult77  2
(772 3

streetcode773 =
,77= >

streetcode77? I
.77I J
Teaser77J P
)77P Q
)77Q R
;77R S
continue88 
;88 
}99 
if;; 
(;; 

streetcode;; 
.;; 
TransliterationUrl;; 1
.;;1 2
Contains;;2 :
(;;: ;
searchQuery;;; F
,;;F G
StringComparison;;H X
.;;X Y
OrdinalIgnoreCase;;Y j
);;j k
);;k l
{<< 
results== 
.== 
Add== 
(==  
CreateFilterResult==  2
(==2 3

streetcode==3 =
,=== >

streetcode==? I
.==I J
TransliterationUrl==J \
)==\ ]
)==] ^
;==^ _
}>> 
}?? 
foreachAA 
(AA 
varAA 
textAA 
inAA  
awaitAA! &
_repositoryWrapperAA' 9
.AA9 :
TextRepositoryAA: H
.AAH I
GetAllAsyncAAI T
(AAT U
includeBB 
:BB 
iBB 
=>BB 
iBB 
.BB 
IncludeBB 
(BB 
xBB 
=>BB  
xBB! "
.BB" #

StreetcodeBB# -
)BB- .
,BB. /
	predicateCC 
:CC 
xCC 
=>CC 
xCC 
.CC 

StreetcodeCC  
.CC  !
StatusCC! '
==CC( *
DALCC+ .
.CC. /
EnumsCC/ 4
.CC4 5
StreetcodeStatusCC5 E
.CCE F
	PublishedCCF O
)CCO P
)CCP Q
{DD 
ifEE 
(EE 
textEE 
.EE 
TitleEE 
.EE 
ContainsEE '
(EE' (
searchQueryEE( 3
,EE3 4
StringComparisonEE5 E
.EEE F
OrdinalIgnoreCaseEEF W
)EEW X
)EEX Y
{FF 
resultsGG 
.GG 
AddGG 
(GG  
CreateFilterResultGG  2
(GG2 3
textGG3 7
.GG7 8

StreetcodeGG8 B
,GGB C
textGGD H
.GGH I
TitleGGI N
,GGN O
$strGGP W
,GGW X
$strGGY _
)GG_ `
)GG` a
;GGa b
continueHH 
;HH 
}II 
ifKK 
(KK 
!KK 
stringKK 
.KK 
IsNullOrEmptyKK )
(KK) *
textKK* .
.KK. /
TextContentKK/ :
)KK: ;
&&KK< >
textKK? C
.KKC D
TextContentKKD O
.KKO P
ContainsKKP X
(KKX Y
searchQueryKKY d
,KKd e
StringComparisonKKf v
.KKv w
OrdinalIgnoreCase	KKw à
)
KKà â
)
KKâ ä
{LL 
resultsMM 
.MM 
AddMM 
(MM  
CreateFilterResultMM  2
(MM2 3
textMM3 7
.MM7 8

StreetcodeMM8 B
,MMB C
textMMD H
.MMH I
TextContentMMI T
,MMT U
$strMMV ]
,MM] ^
$strMM_ e
)MMe f
)MMf g
;MMg h
}NN 
}OO 
foreachQQ 
(QQ 
varQQ 
factQQ 
inQQ  
awaitQQ! &
_repositoryWrapperQQ' 9
.QQ9 :
FactRepositoryQQ: H
.QQH I
GetAllAsyncQQI T
(QQT U
includeRR 
:RR 
iRR 
=>RR 
iRR 
.RR 
IncludeRR 
(RR 
xRR 
=>RR  
xRR! "
.RR" #

StreetcodeRR# -
)RR- .
,RR. /
	predicateSS 
:SS 
xSS 
=>SS 
xSS 
.SS 

StreetcodeSS  
.SS  !
StatusSS! '
==SS( *
DALSS+ .
.SS. /
EnumsSS/ 4
.SS4 5
StreetcodeStatusSS5 E
.SSE F
	PublishedSSF O
)SSO P
)SSP Q
{TT 
ifUU 
(UU 
factUU 
.UU 
TitleUU 
.UU 
ContainsUU '
(UU' (
searchQueryUU( 3
,UU3 4
StringComparisonUU5 E
.UUE F
OrdinalIgnoreCaseUUF W
)UUW X
||UUY [
factUU\ `
.UU` a
FactContentUUa l
.UUl m
ContainsUUm u
(UUu v
searchQuery	UUv Å
,
UUÅ Ç
StringComparison
UUÉ ì
.
UUì î
OrdinalIgnoreCase
UUî •
)
UU• ¶
)
UU¶ ß
{VV 
resultsWW 
.WW 
AddWW 
(WW  
CreateFilterResultWW  2
(WW2 3
factWW3 7
.WW7 8

StreetcodeWW8 B
,WWB C
factWWD H
.WWH I
TitleWWI N
,WWN O
$strWWP [
,WW[ \
$strWW] h
)WWh i
)WWi j
;WWj k
}XX 
}YY 
foreach[[ 
([[ 
var[[ 
timelineItem[[ %
in[[& (
await[[) .
_repositoryWrapper[[/ A
.[[A B
TimelineRepository[[B T
.[[T U
GetAllAsync[[U `
([[` a
include\\ 
:\\ 
i\\ 
=>\\ 
i\\ 
.\\  
Include\\  '
(\\' (
x\\( )
=>\\* ,
x\\- .
.\\. /

Streetcode\\/ 9
)\\9 :
,\\: ;
	predicate]] 
:]] 
x]] 
=>]] 
x]]  !
.]]! "

Streetcode]]" ,
.]], -
Status]]- 3
==]]4 6
DAL]]7 :
.]]: ;
Enums]]; @
.]]@ A
StreetcodeStatus]]A Q
.]]Q R
	Published]]R [
)]][ \
)]]\ ]
{^^ 
if__ 
(__ 
timelineItem__  
.__  !
Title__! &
.__& '
Contains__' /
(__/ 0
searchQuery__0 ;
,__; <
StringComparison__= M
.__M N
OrdinalIgnoreCase__N _
)___ `
||`` 
(`` 
!`` 
string`` 
.``  
IsNullOrEmpty``  -
(``- .
timelineItem``. :
.``: ;
Description``; F
)``F G
&&``H J
timelineItem``K W
.``W X
Description``X c
.``c d
Contains``d l
(``l m
searchQuery``m x
,``x y
StringComparison	``z ä
.
``ä ã
OrdinalIgnoreCase
``ã ú
)
``ú ù
)
``ù û
)
``û ü
{aa 
resultsbb 
.bb 
Addbb 
(bb  
CreateFilterResultbb  2
(bb2 3
timelineItembb3 ?
.bb? @

Streetcodebb@ J
,bbJ K
timelineItembbL X
.bbX Y
TitlebbY ^
,bb^ _
$strbb` l
,bbl m
$strbbn x
)bbx y
)bby z
;bbz {
}cc 
}dd 
foreachff 
(ff 
varff 
streetcodeArtff &
inff' )
awaitff* /
_repositoryWrapperff0 B
.ffB C
ArtRepositoryffC P
.ffP Q
GetAllAsyncffQ \
(ff\ ]
includegg 
:gg 
igg 
=>gg 
igg 
.gg 
Includegg #
(gg# $
xgg$ %
=>gg& (
xgg) *
.gg* +
StreetcodeArtsgg+ 9
)gg9 :
,gg: ;
	predicatehh 
:hh 
xhh 
=>hh 
xhh 
.hh 
StreetcodeArtshh ,
.hh, -
Anyhh- 0
(hh0 1
arthh1 4
=>hh5 7
arthh8 ;
.hh; <

Streetcodehh< F
!=hhG I
nullhhJ N
&&hhO Q
arthhR U
.hhU V

StreetcodehhV `
.hh` a
Statushha g
==hhh j
DALhhk n
.hhn o
Enumshho t
.hht u
StreetcodeStatus	hhu Ö
.
hhÖ Ü
	Published
hhÜ è
)
hhè ê
)
hhê ë
)
hhë í
{ii 
ifjj 
(jj 
!jj 
stringjj 
.jj 
IsNullOrEmptyjj )
(jj) *
streetcodeArtjj* 7
.jj7 8
Descriptionjj8 C
)jjC D
&&jjE G
streetcodeArtjjH U
.jjU V
DescriptionjjV a
.jja b
Containsjjb j
(jjj k
searchQueryjjk v
,jjv w
StringComparison	jjx à
.
jjà â
OrdinalIgnoreCase
jjâ ö
)
jjö õ
)
jjõ ú
{kk 
streetcodeArtll !
.ll! "
StreetcodeArtsll" 0
.ll0 1
ForEachll1 8
(ll8 9
artll9 <
=>ll= ?
{mm 
ifnn 
(nn 
artnn 
.nn  

Streetcodenn  *
==nn+ -
nullnn. 2
)nn2 3
{oo 
returnpp "
;pp" #
}qq 
resultsss 
.ss  
Addss  #
(ss# $
CreateFilterResultss$ 6
(ss6 7
artss7 :
.ss: ;

Streetcodess; E
,ssE F
streetcodeArtssG T
.ssT U
DescriptionssU `
,ss` a
$strssb o
,sso p
$strssq ~
)ss~ 
)	ss Ä
;
ssÄ Å
}tt 
)tt 
;tt 
continueuu 
;uu 
}vv 
}ww 
returnyy 
resultsyy 
;yy 
}zz 	
private|| %
StreetcodeFilterResultDTO|| )
CreateFilterResult||* <
(||< =
StreetcodeContent||= N

streetcode||O Y
,||Y Z
string||[ a
content||b i
,||i j
string||k q
?||q r

sourceName||s }
=||~ 
null
||Ä Ñ
,
||Ñ Ö
string
||Ü å
?
||å ç
	blockName
||é ó
=
||ò ô
null
||ö û
)
||û ü
{}} 	
return~~ 
new~~ %
StreetcodeFilterResultDTO~~ 0
{ 
StreetcodeId
ÄÄ 
=
ÄÄ 

streetcode
ÄÄ )
.
ÄÄ) *
Id
ÄÄ* ,
,
ÄÄ, -*
StreetcodeTransliterationUrl
ÅÅ ,
=
ÅÅ- .

streetcode
ÅÅ/ 9
.
ÅÅ9 : 
TransliterationUrl
ÅÅ: L
,
ÅÅL M
StreetcodeIndex
ÇÇ 
=
ÇÇ  !

streetcode
ÇÇ" ,
.
ÇÇ, -
Index
ÇÇ- 2
,
ÇÇ2 3
	BlockName
ÉÉ 
=
ÉÉ 
	blockName
ÉÉ %
,
ÉÉ% &
Content
ÑÑ 
=
ÑÑ 
content
ÑÑ !
,
ÑÑ! "

SourceName
ÖÖ 
=
ÖÖ 

sourceName
ÖÖ '
,
ÖÖ' (
}
ÜÜ 
;
ÜÜ 
}
áá 	
}
àà 
}ââ ﬂ
óD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAll\GetAllStreetcodesQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetAll7 =
;= >
public 
record "
GetAllStreetcodesQuery $
($ %'
GetAllStreetcodesRequestDTO% @
requestA H
)H I
: 
IRequest 
< 
Result 
< (
GetAllStreetcodesResponseDTO 2
>2 3
>3 4
;4 5‡R
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAll\GetAllStreetcodesHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !

Streetcode

! +
.

+ ,

Streetcode

, 6
.

6 7
GetAll

7 =
;

= >
public 
class $
GetAllStreetcodesHandler %
:& '
IRequestHandler( 7
<7 8"
GetAllStreetcodesQuery8 N
,N O
ResultP V
<V W(
GetAllStreetcodesResponseDTOW s
>s t
>t u
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
$
GetAllStreetcodesHandler #
(# $
IRepositoryWrapper$ 6
repositoryWrapper7 H
,H I
IMapperJ Q
mapperR X
,X Y
ILoggerServiceZ h
loggeri o
)o p
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< (
GetAllStreetcodesResponseDTO 9
>9 :
>: ;
Handle< B
(B C"
GetAllStreetcodesQueryC Y
queryZ _
,_ `
CancellationTokena r
cancellationToken	s Ñ
)
Ñ Ö
{ 
var 
filterRequest 
= 
query !
.! "
request" )
;) *
var 
streetcodes 
= 
_repositoryWrapper ,
., - 
StreetcodeRepository- A
. 
FindAll 
( 
) 
; 
if   

(   
filterRequest   
.   
Title   
is    "
not  # &
null  ' +
)  + ,
{!! 	)
FindStreetcodesWithMatchTitle"" )
("") *
ref""* -
streetcodes"". 9
,""9 :
filterRequest""; H
.""H I
Title""I N
)""N O
;""O P
}## 	
if%% 

(%% 
filterRequest%% 
.%% 
Sort%% 
is%% !
not%%" %
null%%& *
)%%* +
{&& 	!
FindSortedStreetcodes'' !
(''! "
ref''" %
streetcodes''& 1
,''1 2
filterRequest''3 @
.''@ A
Sort''A E
)''E F
;''F G
}(( 	
if** 

(** 
filterRequest** 
.** 
Filter**  
is**! #
not**$ '
null**( ,
)**, -
{++ 	#
FindFilteredStreetcodes,, #
(,,# $
ref,,$ '
streetcodes,,( 3
,,,3 4
filterRequest,,5 B
.,,B C
Filter,,C I
),,I J
;,,J K
}-- 	
int// 
pagesAmount// 
=// 
ApplyPagination// )
(//) *
ref//* -
streetcodes//. 9
,//9 :
filterRequest//; H
.//H I
Amount//I O
,//O P
filterRequest//Q ^
.//^ _
Page//_ c
)//c d
;//d e
var11 
streetcodeDtos11 
=11 
_mapper11 $
.11$ %
Map11% (
<11( )
IEnumerable11) 4
<114 5
StreetcodeDTO115 B
>11B C
>11C D
(11D E
streetcodes11E P
.11P Q
AsEnumerable11Q ]
(11] ^
)11^ _
)11_ `
;11` a
var33 
response33 
=33 
new33 (
GetAllStreetcodesResponseDTO33 7
{44 	
Pages55 
=55 
pagesAmount55 
,55  
Streetcodes66 
=66 
streetcodeDtos66 (
}77 	
;77	 

return99 
Result99 
.99 
Ok99 
(99 
response99 !
)99! "
;99" #
}:: 
private<< 
void<< )
FindStreetcodesWithMatchTitle<< .
(<<. /
ref== 

IQueryable== 
<== 
StreetcodeContent== (
>==( )
streetcodes==* 5
,==5 6
string>> 
title>> 
)>> 
{?? 
streetcodes@@ 
=@@ 
streetcodes@@ !
.@@! "
Where@@" '
(@@' (
s@@( )
=>@@* ,
s@@- .
.@@. /
Title@@/ 4
.AA 
ToLowerAA 
(AA 
)AA 
.BB 
ContainsBB 
(BB 
titleBB 
.CC 
ToLowerCC 
(CC 
)CC 
)CC 
||CC 
sCC 
.CC 
IndexCC "
.DD 
ToStringDD 
(DD 
)DD 
==DD 
titleDD  
)DD  !
;DD! "
}EE 
privateGG 
voidGG #
FindFilteredStreetcodesGG (
(GG( )
refHH 

IQueryableHH 
<HH 
StreetcodeContentHH (
>HH( )
streetcodesHH* 5
,HH5 6
stringII 
filterII 
)II 
{JJ 
varKK 
filterParamsKK 
=KK 
filterKK !
.KK! "
SplitKK" '
(KK' (
$charKK( +
)KK+ ,
;KK, -
varLL 
filterColumnLL 
=LL 
filterParamsLL '
[LL' (
$numLL( )
]LL) *
;LL* +
varMM 
filterValueMM 
=MM 
filterParamsMM &
[MM& '
$numMM' (
]MM( )
;MM) *
streetcodesOO 
=OO 
streetcodesOO !
.PP 
AsEnumerablePP 
(PP 
)PP 
.QQ 
WhereQQ 
(QQ 
sQQ 
=>QQ 
filterValueQQ #
.QQ# $
ContainsQQ$ ,
(QQ, -
sQQ- .
.QQ. /
StatusQQ/ 5
.QQ5 6
ToStringQQ6 >
(QQ> ?
)QQ? @
)QQ@ A
)QQA B
.RR 
AsQueryableRR 
(RR 
)RR 
;RR 
}SS 
privateUU 
voidUU !
FindSortedStreetcodesUU &
(UU& '
refVV 

IQueryableVV 
<VV 
StreetcodeContentVV (
>VV( )
streetcodesVV* 5
,VV5 6
stringWW 
sortWW 
)WW 
{XX 
varYY 
sortedRecordsYY 
=YY 
streetcodesYY '
;YY' (
var[[ 

sortColumn[[ 
=[[ 
sort[[ 
.[[ 
Trim[[ "
([[" #
)[[# $
;[[$ %
var\\ 
sortDirection\\ 
=\\ 
$str\\ !
;\\! "
if^^ 

(^^ 

sortColumn^^ 
.^^ 

StartsWith^^ !
(^^! "
$str^^" %
)^^% &
)^^& '
{__ 	
sortDirection`` 
=`` 
$str`` "
;``" #

sortColumnaa 
=aa 

sortColumnaa #
.aa# $
	Substringaa$ -
(aa- .
$numaa. /
)aa/ 0
;aa0 1
}bb 	
vardd 
typedd 
=dd 
typeofdd 
(dd 
StreetcodeContentdd +
)dd+ ,
;dd, -
varee 
	parameteree 
=ee 

Expressionee "
.ee" #
	Parameteree# ,
(ee, -
typeee- 1
,ee1 2
$stree3 6
)ee6 7
;ee7 8
varff 
propertyff 
=ff 

Expressionff !
.ff! "
Propertyff" *
(ff* +
	parameterff+ 4
,ff4 5

sortColumnff6 @
)ff@ A
;ffA B
vargg 
lambdagg 
=gg 

Expressiongg 
.gg  
Lambdagg  &
(gg& '
propertygg' /
,gg/ 0
	parametergg1 :
)gg: ;
;gg; <
streetcodesii 
=ii 
sortDirectionii #
switchii$ *
{jj 	
$strkk 
=>kk 
	Queryablekk 
.kk 
OrderBykk &
(kk& '
sortedRecordskk' 4
,kk4 5
(kk6 7
dynamickk7 >
)kk> ?
lambdakk? E
)kkE F
,kkF G
$strll 
=>ll 
	Queryablell 
.ll  
OrderByDescendingll  1
(ll1 2
sortedRecordsll2 ?
,ll? @
(llA B
dynamicllB I
)llI J
lambdallJ P
)llP Q
,llQ R
_mm 
=>mm 
sortedRecordsmm 
,mm 
}nn 	
;nn	 

}oo 
privateqq 
intqq 
ApplyPaginationqq 
(qq  
refrr 

IQueryablerr 
<rr 
StreetcodeContentrr (
>rr( )
streetcodesrr* 5
,rr5 6
intss 
amountss 
,ss 
inttt 
pagett 
)tt 
{uu 
varvv 

totalPagesvv 
=vv 
(vv 
intvv 
)vv 
Mathvv "
.vv" #
Ceilingvv# *
(vv* +
streetcodesvv+ 6
.vv6 7
Countvv7 <
(vv< =
)vv= >
/vv? @
(vvA B
doublevvB H
)vvH I
amountvvI O
)vvO P
;vvP Q
streetcodesxx 
=xx 
streetcodesxx !
.yy 
Skipyy 
(yy 
(yy 
pageyy 
-yy 
$numyy 
)yy 
*yy 
amountyy %
)yy% &
.zz 
Takezz 
(zz 
amountzz 
)zz 
;zz 
return|| 

totalPages|| 
;|| 
}}} 
}~~ —
°D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAllShort\GetAllStreetcodesShortQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetAllShort7 B
{ 
public 

record '
GetAllStreetcodesShortQuery -
:. /
IRequest0 8
<8 9
Result9 ?
<? @
IEnumerable@ K
<K L
StreetcodeShortDTOL ^
>^ _
>_ `
>` a
;a b
} Ø
£D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAllShort\GetAllStreetcodesShortHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetAllShort7 B
{		 
public

 

class

 )
GetAllStreetcodesShortHandler

 .
:

/ 0
IRequestHandler

1 @
<

@ A'
GetAllStreetcodesShortQuery

A \
,

\ ]
Result 
< 
IEnumerable 
< 
StreetcodeShortDTO -
>- .
>. /
>/ 0
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public )
GetAllStreetcodesShortHandler ,
(, -
IRepositoryWrapper- ?
repositoryWrapper@ Q
,Q R
IMapperS Z
mapper[ a
,a b
ILoggerServicec q
loggerr x
)x y
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
StreetcodeShortDTO- ?
>? @
>@ A
>A B
HandleC I
(I J'
GetAllStreetcodesShortQueryJ e
requestf m
,m n
CancellationToken	o Ä
cancellationToken
Å í
)
í ì
{ 	
var 
streetcodes 
= 
await #
_repositoryWrapper$ 6
.6 7 
StreetcodeRepository7 K
.K L
GetAllAsyncL W
(W X
)X Y
;Y Z
if 
( 
streetcodes 
!= 
null #
)# $
{ 
return 
Result 
. 
Ok  
(  !
_mapper! (
.( )
Map) ,
<, -
IEnumerable- 8
<8 9
StreetcodeShortDTO9 K
>K L
>L M
(M N
streetcodesN Y
)Y Z
)Z [
;[ \
} 
const   
string   
errorMsg   !
=  " #
$str  $ >
;  > ?
_logger!! 
.!! 
LogError!! 
(!! 
request!! $
,!!$ %
errorMsg!!& .
)!!. /
;!!/ 0
return"" 
Result"" 
."" 
Fail"" 
("" 
errorMsg"" '
)""' (
;""( )
}## 	
}$$ 
}%% Î
ßD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAllMainPage\GetAllStreetcodesMainPageQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7%
GetAllStreetcodesMainPage7 P
{ 
public 

record *
GetAllStreetcodesMainPageQuery 0
:1 2
IRequest3 ;
<; <
Result< B
<B C
IEnumerableC N
<N O!
StreetcodeMainPageDTOO d
>d e
>e f
>f g
;g h
} À
•D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAllCatalog\GetAllStreetcodesCatalogQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7
GetAllCatalog7 D
{ 
public 
record	 )
GetAllStreetcodesCatalogQuery -
(- .
int. 1
page2 6
,6 7
int8 ;
count< A
)A B
:C D
IRequestE M
<M N
ResultN T
<T U
IEnumerableU `
<` a
RelatedFigureDTOa q
>q r
>r s
>s t
;t u
} Ò
©D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAllMainPage\GetAllStreetcodesMainPageHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !

Streetcode

! +
.

+ ,

Streetcode

, 6
.

6 7
GetAllMainPage

7 E
{ 
public 

class ,
 GetAllStreetcodesMainPageHandler 1
:2 3
IRequestHandler4 C
<C D*
GetAllStreetcodesMainPageQueryD b
,b c
Result 
< 
IEnumerable 
< !
StreetcodeMainPageDTO 0
>0 1
>1 2
>2 3
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public ,
 GetAllStreetcodesMainPageHandler /
(/ 0
IRepositoryWrapper0 B
repositoryWrapperC T
,T U
IMapperV ]
mapper^ d
,d e
ILoggerServicef t
loggeru {
){ |
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -!
StreetcodeMainPageDTO- B
>B C
>C D
>D E
HandleF L
(L M*
GetAllStreetcodesMainPageQueryM k
requestl s
,s t
CancellationToken	u Ü
cancellationToken
á ò
)
ò ô
{ 	
var 
streetcodes 
= 
await #
_repositoryWrapper$ 6
.6 7 
StreetcodeRepository7 K
.K L
GetAllAsyncL W
(W X
	predicate 
: 
sc 
=>  
sc! #
.# $
Status$ *
==+ -
DAL. 1
.1 2
Enums2 7
.7 8
StreetcodeStatus8 H
.H I
	PublishedI R
,R S
include 
: 
src 
=> 
src  #
.# $
Include$ +
(+ ,
item, 0
=>1 3
item4 8
.8 9
Text9 =
)= >
.> ?
Include? F
(F G
itemG K
=>L N
itemO S
.S T
ImagesT Z
)Z [
)[ \
;\ ]
if   
(   
streetcodes   
!=   
null   #
)  # $
{!! 
return"" 
Result"" 
."" 
Ok""  
(""  !
_mapper""! (
.""( )
Map"") ,
<"", -
IEnumerable""- 8
<""8 9!
StreetcodeMainPageDTO""9 N
>""N O
>""O P
(""P Q
streetcodes""Q \
)""\ ]
)""] ^
;""^ _
}## 
const%% 
string%% 
errorMsg%% !
=%%" #
$str%%$ >
;%%> ?
_logger&& 
.&& 
LogError&& 
(&& 
request&& $
,&&$ %
errorMsg&&& .
)&&. /
;&&/ 0
return'' 
Result'' 
.'' 
Fail'' 
('' 
errorMsg'' '
)''' (
;''( )
}(( 	
})) 
}** √"
ßD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\GetAllCatalog\GetAllStreetcodesCatalogHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !

Streetcode		! +
.		+ ,

Streetcode		, 6
.		6 7
GetAllCatalog		7 D
{

 
public 
class	 +
GetAllStreetcodesCatalogHandler .
:/ 0
IRequestHandler1 @
<@ A)
GetAllStreetcodesCatalogQueryA ^
,^ _
Result 
< 
IEnumerable 
< 
RelatedFigureDTO +
>+ ,
>, -
>- .
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public +
GetAllStreetcodesCatalogHandler .
(. /
IRepositoryWrapper/ A
repositoryWrapperB S
,S T
IMapperU \
mapper] c
,c d
ILoggerServicee s
loggert z
)z {
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
RelatedFigureDTO- =
>= >
>> ?
>? @
HandleA G
(G H)
GetAllStreetcodesCatalogQueryH e
requestf m
,m n
CancellationToken	o Ä
cancellationToken
Å í
)
í ì
{ 	
var 
streetcodes 
= 
await #
_repositoryWrapper$ 6
.6 7 
StreetcodeRepository7 K
.K L
GetAllAsyncL W
(W X
	predicate 
: 
sc 
=>  
sc! #
.# $
Status$ *
==+ -
DAL. 1
.1 2
Enums2 7
.7 8
StreetcodeStatus8 H
.H I
	PublishedI R
,R S
include 
: 
src 
=> 
src  #
.# $
Include$ +
(+ ,
item, 0
=>1 3
item4 8
.8 9
Tags9 =
)= >
.> ?
Include? F
(F G
itemG K
=>L N
itemO S
.S T
ImagesT Z
)Z [
)[ \
;\ ]
if 
( 
streetcodes 
!= 
null #
)# $
{   
var!! 
skipped!! 
=!! 
streetcodes!! )
.!!) *
Skip!!* .
(!!. /
(!!/ 0
request!!0 7
.!!7 8
page!!8 <
-!!= >
$num!!? @
)!!@ A
*!!B C
request!!D K
.!!K L
count!!L Q
)!!Q R
.!!R S
Take!!S W
(!!W X
request!!X _
.!!_ `
count!!` e
)!!e f
;!!f g
return"" 
Result"" 
."" 
Ok""  
(""  !
_mapper""! (
.""( )
Map"") ,
<"", -
IEnumerable""- 8
<""8 9
RelatedFigureDTO""9 I
>""I J
>""J K
(""K L
skipped""L S
)""S T
)""T U
;""U V
}## 
const%% 
string%% 
errorMsg%% !
=%%" #
$"%%$ &
$str%%& ?
"%%? @
;%%@ A
_logger&& 
.&& 
LogError&& 
(&& 
request&& $
,&&$ %
errorMsg&&& .
)&&. /
;&&/ 0
return'' 
Result'' 
.'' 
Fail'' 
('' 
errorMsg'' '
)''' (
;''( )
}(( 	
})) 
}** ˇ!
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\DeleteSoft\DeleteSoftStreetcodeHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7

DeleteSoft7 A
;A B
public 
class '
DeleteSoftStreetcodeHandler (
:) *
IRequestHandler+ :
<: ;'
DeleteSoftStreetcodeCommand; V
,V W
ResultX ^
<^ _
Unit_ c
>c d
>d e
{		 
private

 
readonly

 
IRepositoryWrapper

 '
_repositoryWrapper

( :
;

: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
'
DeleteSoftStreetcodeHandler &
(& '
IRepositoryWrapper' 9
repositoryWrapper: K
,K L
ILoggerServiceM [
logger\ b
)b c
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +'
DeleteSoftStreetcodeCommand+ F
requestG N
,N O
CancellationTokenP a
cancellationTokenb s
)s t
{ 
var 

streetcode 
= 
await 
_repositoryWrapper 1
.1 2 
StreetcodeRepository2 F
. "
GetFirstOrDefaultAsync #
(# $
f$ %
=>& (
f) *
.* +
Id+ -
==. 0
request1 8
.8 9
Id9 ;
); <
;< =
if 

( 

streetcode 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  X
{X Y
requestY `
.` a
Ida c
}c d
"d e
;e f
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
throw 
new !
ArgumentNullException +
(+ ,
errorMsg, 4
)4 5
;5 6
} 	

streetcode 
. 
Status 
= 
DAL 
.  
Enums  %
.% &
StreetcodeStatus& 6
.6 7
Deleted7 >
;> ?

streetcode   
.   
	UpdatedAt   
=   
DateTime   '
.  ' (
Now  ( +
;  + ,
_repositoryWrapper"" 
.""  
StreetcodeRepository"" /
.""/ 0
Update""0 6
(""6 7

streetcode""7 A
)""A B
;""B C
var$$  
resultIsDeleteSucces$$  
=$$! "
await$$# (
_repositoryWrapper$$) ;
.$$; <
SaveChangesAsync$$< L
($$L M
)$$M N
>$$O P
$num$$Q R
;$$R S
if&& 

(&&
  
resultIsDeleteSucces&& 
)&&  
{'' 	
return(( 
Result(( 
.(( 
Ok(( 
((( 
Unit(( !
.((! "
Value((" '
)((' (
;((( )
})) 	
else** 
{++ 	
const,, 
string,, 
errorMsg,, !
=,," #
$str,,$ V
;,,V W
_logger-- 
.-- 
LogError-- 
(-- 
request-- $
,--$ %
errorMsg--& .
)--. /
;--/ 0
return.. 
Result.. 
... 
Fail.. 
(.. 
new.. "
Error..# (
(..( )
errorMsg..) 1
)..1 2
)..2 3
;..3 4
}// 	
}00 
}11 º
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Streetcode\DeleteSoft\DeleteSoftStreetcodeCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,

Streetcode, 6
.6 7

DeleteSoft7 A
;A B
public 
record '
DeleteSoftStreetcodeCommand )
() *
int* -
Id. 0
)0 1
:2 3
IRequest4 <
<< =
Result= C
<C D
UnitD H
>H I
>I J
;J KÒ
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\Update\UpdateRelatedTermHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
Update8 >
{ 
public		 

class		 $
UpdateRelatedTermHandler		 )
:		* +
IRequestHandler		, ;
<		; <$
UpdateRelatedTermCommand		< T
,		T U
Result		V \
<		\ ]
Unit		] a
>		a b
>		b c
{

 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
public $
UpdateRelatedTermHandler '
(' (
IMapper( /
mapper0 6
,6 7
IRepositoryWrapper8 J

repositoryK U
)U V
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 

repository $
;$ %
} 	
public 
Task 
< 
Result 
< 
Unit 
>  
>  !
Handle" (
(( )$
UpdateRelatedTermCommand) A
requestB I
,I J
CancellationTokenK \
cancellationToken] n
)n o
{ 	
throw 
new #
NotImplementedException -
(- .
). /
;/ 0
} 	
} 
} ê
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\Update\UpdateRelatedTermCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
Update8 >
{ 
public 

record $
UpdateRelatedTermCommand *
(* +
int+ .
id/ 1
,1 2
RelatedTermDTO3 A
RelatedTermB M
)M N
:O P
IRequestQ Y
<Y Z
ResultZ `
<` a
Unita e
>e f
>f g
{ 
}		 
}

 á
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\GetById\GetRelatedTermByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
GetById8 ?
{ 
internal		 
class		 #
GetRelatedTermByIdQuery		 *
{

 
} 
} Ö
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\GetAll\GetAllRelatedTermsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
GetAll8 >
{ 
internal		 
class		 #
GetAllRelatedTermsQuery		 *
{

 
} 
} ´
©D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\GetAllByTermId\GetAllRelatedTermsByTermIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
GetAllByTermId8 F
{ 
public 

record +
GetAllRelatedTermsByTermIdQuery 1
(1 2
int2 5
id6 8
)8 9
:: ;
IRequest< D
<D E
ResultE K
<K L
IEnumerableL W
<W X
RelatedTermDTOX f
>f g
>g h
>h i
{ 
}		 
}

 ’!
´D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\GetAllByTermId\GetAllRelatedTermsByTermIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !

Streetcode		! +
.		+ ,
RelatedTerm		, 7
.		7 8
GetAllByTermId		8 F
{

 
public 

record -
!GetAllRelatedTermsByTermIdHandler 3
:4 5
IRequestHandler6 E
<E F+
GetAllRelatedTermsByTermIdQueryF e
,e f
Resultg m
<m n
IEnumerablen y
<y z
RelatedTermDTO	z à
>
à â
>
â ä
>
ä ã
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public -
!GetAllRelatedTermsByTermIdHandler 0
(0 1
IMapper1 8
mapper9 ?
,? @
IRepositoryWrapperA S
repositoryWrapperT e
,e f
ILoggerServiceg u
loggerv |
)| }
{ 	
_mapper 
= 
mapper 
; 
_repository 
= 
repositoryWrapper +
;+ ,
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
RelatedTermDTO- ;
>; <
>< =
>= >
Handle? E
(E F+
GetAllRelatedTermsByTermIdQueryF e
requestf m
,m n
CancellationToken	o Ä
cancellationToken
Å í
)
í ì
{ 	
var 
relatedTerms 
= 
await $
_repository% 0
.0 1!
RelatedTermRepository1 F
. 
GetAllAsync 
( 
	predicate 
: 
rt 
=>  
rt! #
.# $
TermId$ *
==+ -
request. 5
.5 6
id6 8
,8 9
include 
: 
rt 
=> 
rt !
.! "
Include" )
() *
rt* ,
=>- /
rt0 2
.2 3
Term3 7
)7 8
)8 9
;9 :
if 
( 
relatedTerms 
is 
null  $
)$ %
{   
const!! 
string!! 
errorMsg!! %
=!!& '
$str!!( E
;!!E F
_logger"" 
."" 
LogError""  
(""  !
request""! (
,""( )
errorMsg""* 2
)""2 3
;""3 4
return## 
new## 
Error##  
(##  !
errorMsg##! )
)##) *
;##* +
}$$ 
var&& 
relatedTermsDTO&& 
=&&  !
_mapper&&" )
.&&) *
Map&&* -
<&&- .
IEnumerable&&. 9
<&&9 :
RelatedTermDTO&&: H
>&&H I
>&&I J
(&&J K
relatedTerms&&K W
)&&W X
;&&X Y
if(( 
((( 
relatedTermsDTO(( 
is((  "
null((# '
)((' (
{)) 
const** 
string** 
errorMsg** %
=**& '
$str**( O
;**O P
_logger++ 
.++ 
LogError++  
(++  !
request++! (
,++( )
errorMsg++* 2
)++2 3
;++3 4
return,, 
new,, 
Error,,  
(,,  !
errorMsg,,! )
),,) *
;,,* +
}-- 
return// 
Result// 
.// 
Ok// 
(// 
relatedTermsDTO// ,
)//, -
;//- .
}00 	
}11 
}22 à%
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\Delete\DeleteRelatedTermHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
Delete8 >
{		 
public

 

class

 $
DeleteRelatedTermHandler

 )
:

* +
IRequestHandler

, ;
<

; <$
DeleteRelatedTermCommand

< T
,

T U
Result

V \
<

\ ]
RelatedTermDTO

] k
>

k l
>

l m
{ 
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public $
DeleteRelatedTermHandler '
(' (
IRepositoryWrapper( :

repository; E
,E F
IMapperG N
mapperO U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 	
_repository 
= 

repository $
;$ %
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
RelatedTermDTO! /
>/ 0
>0 1
Handle2 8
(8 9$
DeleteRelatedTermCommand9 Q
requestR Y
,Y Z
CancellationToken[ l
cancellationTokenm ~
)~ 
{ 	
var 
relatedTerm 
= 
await #
_repository$ /
./ 0!
RelatedTermRepository0 E
.E F"
GetFirstOrDefaultAsyncF \
(\ ]
rt] _
=>` b
rtc e
.e f
Wordf j
.j k
ToLowerk r
(r s
)s t
.t u
Equalsu {
({ |
request	| É
.
É Ñ
word
Ñ à
.
à â
ToLower
â ê
(
ê ë
)
ë í
)
í ì
)
ì î
;
î ï
if 
( 
relatedTerm 
is 
null #
)# $
{ 
string 
errorMsg 
=  !
$"" $
$str$ @
{@ A
requestA H
.H I
wordI M
}M N
"N O
;O P
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return 
Result 
. 
Fail "
(" #
new# &
Error' ,
(, -
errorMsg- 5
)5 6
)6 7
;7 8
}   
_repository"" 
."" !
RelatedTermRepository"" -
.""- .
Delete"". 4
(""4 5
relatedTerm""5 @
)""@ A
;""A B
var$$ 
resultIsSuccess$$ 
=$$  !
await$$" '
_repository$$( 3
.$$3 4
SaveChangesAsync$$4 D
($$D E
)$$E F
>$$G H
$num$$I J
;$$J K
var%% 
relatedTermDto%% 
=%%  
_mapper%%! (
.%%( )
Map%%) ,
<%%, -
RelatedTermDTO%%- ;
>%%; <
(%%< =
relatedTerm%%= H
)%%H I
;%%I J
if&& 
(&& 
resultIsSuccess&& 
&&&& !
relatedTermDto&&" 0
!=&&1 3
null&&4 8
)&&8 9
{'' 
return(( 
Result(( 
.(( 
Ok((  
(((  !
relatedTermDto((! /
)((/ 0
;((0 1
})) 
else** 
{++ 
const,, 
string,, 
errorMsg,, %
=,,& '
$str,,( I
;,,I J
_logger-- 
.-- 
LogError--  
(--  !
request--! (
,--( )
errorMsg--* 2
)--2 3
;--3 4
return.. 
Result.. 
... 
Fail.. "
(.." #
new..# &
Error..' ,
(.., -
errorMsg..- 5
)..5 6
)..6 7
;..7 8
}// 
}00 	
}11 
}22 Ã
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\Delete\DeleteRelatedTermCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
Delete8 >
{ 
public 

record $
DeleteRelatedTermCommand *
(* +
string+ 1
word2 6
)6 7
:8 9
IRequest: B
<B C
ResultC I
<I J
RelatedTermDTOJ X
>X Y
>Y Z
;Z [
} À1
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\Create\CreateRelatedTermHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !

Streetcode

! +
.

+ ,
RelatedTerm

, 7
.

7 8
Create

8 >
{ 
public 

class $
CreateRelatedTermHandler )
:* +
IRequestHandler, ;
<; <$
CreateRelatedTermCommand< T
,T U
ResultV \
<\ ]
RelatedTermDTO] k
>k l
>l m
{ 
private 
readonly 
IRepositoryWrapper +
_repository, 7
;7 8
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public $
CreateRelatedTermHandler '
(' (
IRepositoryWrapper( :

repository; E
,E F
IMapperG N
mapperO U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 	
_repository 
= 

repository $
;$ %
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
RelatedTermDTO! /
>/ 0
>0 1
Handle2 8
(8 9$
CreateRelatedTermCommand9 Q
requestR Y
,Y Z
CancellationToken[ l
cancellationTokenm ~
)~ 
{ 	
var 
relatedTerm 
= 
_mapper %
.% &
Map& )
<) *
Entity* 0
>0 1
(1 2
request2 9
.9 :
RelatedTerm: E
)E F
;F G
if 
( 
relatedTerm 
is 
null #
)# $
{ 
const 
string 
errorMsg %
=& '
$str( T
;T U
_logger   
.   
LogError    
(    !
request  ! (
,  ( )
errorMsg  * 2
)  2 3
;  3 4
return!! 
Result!! 
.!! 
Fail!! "
(!!" #
new!!# &
Error!!' ,
(!!, -
errorMsg!!- 5
)!!5 6
)!!6 7
;!!7 8
}"" 
var$$ 
existingTerms$$ 
=$$ 
await$$  %
_repository$$& 1
.$$1 2!
RelatedTermRepository$$2 G
.%% 
GetAllAsync%% 
(%% 
	predicate&& 
:&& 
rt&& 
=>&&  
rt&&! #
.&&# $
TermId&&$ *
==&&+ -
request&&. 5
.&&5 6
RelatedTerm&&6 A
.&&A B
TermId&&B H
&&&&I K
rt&&L N
.&&N O
Word&&O S
==&&T V
request&&W ^
.&&^ _
RelatedTerm&&_ j
.&&j k
Word&&k o
)&&o p
;&&p q
if(( 
((( 
existingTerms(( 
is((  
null((! %
||((& (
existingTerms(() 6
.((6 7
Any((7 :
(((: ;
)((; <
)((< =
{)) 
const** 
string** 
errorMsg** %
=**& '
$str**( K
;**K L
_logger++ 
.++ 
LogError++  
(++  !
request++! (
,++( )
errorMsg++* 2
)++2 3
;++3 4
return,, 
Result,, 
.,, 
Fail,, "
(,," #
new,,# &
Error,,' ,
(,,, -
errorMsg,,- 5
),,5 6
),,6 7
;,,7 8
}-- 
var// 
createdRelatedTerm// "
=//# $
await//% *
_repository//+ 6
.//6 7!
RelatedTermRepository//7 L
.//L M
CreateAsync//M X
(//X Y
relatedTerm//Y d
)//d e
;//e f
var11 
isSuccessResult11 
=11  !
await11" '
_repository11( 3
.113 4
SaveChangesAsync114 D
(11D E
)11E F
>11G H
$num11I J
;11J K
if33 
(33 
!33 
isSuccessResult33 
)33  
{44 
const55 
string55 
errorMsg55 %
=55& '
$str55( j
;55j k
_logger66 
.66 
LogError66  
(66  !
request66! (
,66( )
errorMsg66* 2
)662 3
;663 4
return77 
Result77 
.77 
Fail77 "
(77" #
new77# &
Error77' ,
(77, -
errorMsg77- 5
)775 6
)776 7
;777 8
}88 
var:: !
createdRelatedTermDTO:: %
=::& '
_mapper::( /
.::/ 0
Map::0 3
<::3 4
RelatedTermDTO::4 B
>::B C
(::C D
createdRelatedTerm::D V
)::V W
;::W X
if<< 
(<< !
createdRelatedTermDTO<< $
!=<<% '
null<<( ,
)<<, -
{== 
return>> 
Result>> 
.>> 
Ok>>  
(>>  !!
createdRelatedTermDTO>>! 6
)>>6 7
;>>7 8
}?? 
else@@ 
{AA 
constBB 
stringBB 
errorMsgBB %
=BB& '
$strBB( <
;BB< =
_loggerCC 
.CC 
LogErrorCC  
(CC  !
requestCC! (
,CC( )
errorMsgCC* 2
)CC2 3
;CC3 4
returnDD 
ResultDD 
.DD 
FailDD "
(DD" #
newDD# &
ErrorDD' ,
(DD, -
errorMsgDD- 5
)DD5 6
)DD6 7
;DD7 8
}EE 
}FF 	
}GG 
}HH Í
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedTerm\Create\CreateRelatedTermCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedTerm, 7
.7 8
Create8 >
{ 
public 

record $
CreateRelatedTermCommand *
(* +
RelatedTermDTO+ 9
RelatedTerm: E
)E F
:G H
IRequestI Q
<Q R
ResultR X
<X Y
RelatedTermDTOY g
>g h
>h i
{ 
}		 
}

 ç.
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\–°reate\CreateRelatedFigureHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !

Streetcode		! +
.		+ ,
RelatedFigure		, 9
.		9 :
Create		: @
;		@ A
public 
class &
CreateRelatedFigureHandler '
:( )
IRequestHandler* 9
<9 :&
CreateRelatedFigureCommand: T
,T U
ResultV \
<\ ]
Unit] a
>a b
>b c
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
&
CreateRelatedFigureHandler %
(% &
IRepositoryWrapper& 8
repositoryWrapper9 J
,J K
IMapperL S
mapperT Z
,Z [
ILoggerService\ j
loggerk q
)q r
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +&
CreateRelatedFigureCommand+ E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 
var 
observerEntity 
= 
await "
_repositoryWrapper# 5
.5 6 
StreetcodeRepository6 J
.J K"
GetFirstOrDefaultAsyncK a
(a b
relb e
=>f h
reli l
.l m
Idm o
==p r
requests z
.z {

ObserverId	{ Ö
)
Ö Ü
;
Ü á
var 
targetEntity 
= 
await  
_repositoryWrapper! 3
.3 4 
StreetcodeRepository4 H
.H I"
GetFirstOrDefaultAsyncI _
(_ `
rel` c
=>d f
relg j
.j k
Idk m
==n p
requestq x
.x y
TargetId	y Å
)
Å Ç
;
Ç É
if 

( 
observerEntity 
is 
null "
)" #
{ 	
string 
errorMsg 
= 
$"  
$str  @
{@ A
requestA H
.H I

ObserverIdI S
}S T
"T U
;U V
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
if$$ 

($$ 
targetEntity$$ 
is$$ 
null$$  
)$$  !
{%% 	
string&& 
errorMsg&& 
=&& 
$"&&  
$str&&  @
{&&@ A
request&&A H
.&&H I
TargetId&&I Q
}&&Q R
"&&R S
;&&S T
_logger'' 
.'' 
LogError'' 
('' 
request'' $
,''$ %
errorMsg''& .
)''. /
;''/ 0
return(( 
Result(( 
.(( 
Fail(( 
((( 
new(( "
Error((# (
(((( )
errorMsg(() 1
)((1 2
)((2 3
;((3 4
})) 	
var++ 
relation++ 
=++ 
new++ 
DAL++ 
.++ 
Entities++ '
.++' (

Streetcode++( 2
.++2 3
RelatedFigure++3 @
{,, 	

ObserverId-- 
=-- 
observerEntity-- '
.--' (
Id--( *
,--* +
TargetId.. 
=.. 
targetEntity.. #
...# $
Id..$ &
,..& '
}// 	
;//	 

await11 
_repositoryWrapper11  
.11  !#
RelatedFigureRepository11! 8
.118 9
CreateAsync119 D
(11D E
relation11E M
)11M N
;11N O
var33 
resultIsSuccess33 
=33 
await33 #
_repositoryWrapper33$ 6
.336 7
SaveChangesAsync337 G
(33G H
)33H I
>33J K
$num33L M
;33M N
if44 

(44
 
resultIsSuccess44 
)44 
{55 	
return66 
Result66 
.66 
Ok66 
(66 
Unit66 !
.66! "
Value66" '
)66' (
;66( )
}77 	
else88 
{99 	
string:: 
errorMsg:: 
=:: 
$str:: <
;::< =
_logger;; 
.;; 
LogError;; 
(;; 
request;; $
,;;$ %
errorMsg;;& .
);;. /
;;;/ 0
return<< 
Result<< 
.<< 
Fail<< 
(<< 
new<< "
Error<<# (
(<<( )
errorMsg<<) 1
)<<1 2
)<<2 3
;<<3 4
}== 	
}>> 
}?? ˜
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\–°reate\CreateRelatedFigureCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedFigure, 9
.9 :
Create: @
;@ A
public 
record &
CreateRelatedFigureCommand (
(( )
int) ,

ObserverId- 7
,7 8
int9 <
TargetId= E
)E F
:G H
IRequestI Q
<Q R
ResultR X
<X Y
UnitY ]
>] ^
>^ _
;_ `ô
•D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\GetByTagId\GetRelatedFiguresByTagIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedFigure, 9
.9 :

GetByTagId: D
{ 
public 
record	 )
GetRelatedFiguresByTagIdQuery -
(- .
int. 1
tagId2 7
)7 8
:9 :
IRequest; C
<C D
ResultD J
<J K
IEnumerableK V
<V W
RelatedFigureDTOW g
>g h
>h i
>i j
;j k
} Ö#
ßD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\GetByTagId\GetRelatedFiguresByTagIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedFigure, 9
.9 :

GetByTagId: D
{ 
internal 

class +
GetRelatedFiguresByTagIdHandler 0
:1 2
IRequestHandler3 B
<B C)
GetRelatedFiguresByTagIdQueryC `
,` a
Resultb h
<h i
IEnumerablei t
<t u
RelatedFigureDTO	u Ö
>
Ö Ü
>
Ü á
>
á à
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public +
GetRelatedFiguresByTagIdHandler .
(. /
IRepositoryWrapper/ A
repositoryWrapperB S
,S T
IMapperU \
mapper] c
,c d
ILoggerServicee s
loggert z
)z {
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
RelatedFigureDTO- =
>= >
>> ?
>? @
HandleA G
(G H)
GetRelatedFiguresByTagIdQueryH e
requestf m
,m n
CancellationToken	o Ä
cancellationToken
Å í
)
í ì
{ 	
var 
streetcodes 
= 
await #
_repositoryWrapper$ 6
.6 7 
StreetcodeRepository7 K
. 
GetAllAsync 
( 
	predicate 
: 
sc 
=>  
sc! #
.# $
Status$ *
==+ -
DAL. 1
.1 2
Enums2 7
.7 8
StreetcodeStatus8 H
.H I
	PublishedI R
&&S U
sc   
.   
Tags   
.   
Select    
(    !
t  ! "
=>  # %
t  & '
.  ' (
Id  ( *
)  * +
.  + ,
Any  , /
(  / 0
tag  0 3
=>  4 6
tag  7 :
==  ; =
request  > E
.  E F
tagId  F K
)  K L
,  L M
include!! 
:!! 
scl!! 
=>!! 
scl!!  #
."" 
Include"" 
("" 
sc"" 
=>""  "
sc""# %
.""% &
Images""& ,
)"", -
.## 
Include## 
(## 
sc## 
=>##  "
sc### %
.##% &
Tags##& *
)##* +
)##+ ,
;##, -
if%% 
(%% 
streetcodes%% 
is%% 
null%% #
)%%# $
{&& 
string'' 
errorMsg'' 
=''  !
$"''" $
$str''$ Y
{''Y Z
request''Z a
.''a b
tagId''b g
}''g h
"''h i
;''i j
_logger(( 
.(( 
LogError((  
(((  !
request((! (
,((( )
errorMsg((* 2
)((2 3
;((3 4
return)) 
Result)) 
.)) 
Fail)) "
())" #
new))# &
Error))' ,
()), -
errorMsg))- 5
)))5 6
)))6 7
;))7 8
}** 
return,, 
Result,, 
.,, 
Ok,, 
(,, 
_mapper,, $
.,,$ %
Map,,% (
<,,( )
IEnumerable,,) 4
<,,4 5
RelatedFigureDTO,,5 E
>,,E F
>,,F G
(,,G H
streetcodes,,H S
),,S T
),,T U
;,,U V
}-- 	
}.. 
}// Æ
≥D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\GetByStreetcodeId\GetRelatedFiguresByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedFigure, 9
.9 :
GetByStreetcodeId: K
;K L
public 
record /
#GetRelatedFigureByStreetcodeIdQuery 1
(1 2
int2 5
StreetcodeId6 B
)B C
:D E
IRequestF N
<N O
ResultO U
<U V
IEnumerableV a
<a b
RelatedFigureDTOb r
>r s
>s t
>t u
;u vÀ=
µD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\GetByStreetcodeId\GetRelatedFiguresByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedFigure, 9
.9 :
GetByStreetcodeId: K
;K L
public 
class 2
&GetRelatedFiguresByStreetcodeIdHandler 3
:4 5
IRequestHandler6 E
<E F/
#GetRelatedFigureByStreetcodeIdQueryF i
,i j
Resultk q
<q r
IEnumerabler }
<} ~
RelatedFigureDTO	~ é
>
é è
>
è ê
>
ê ë
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
2
&GetRelatedFiguresByStreetcodeIdHandler 1
(1 2
IMapper2 9
mapper: @
,@ A
IRepositoryWrapperB T
repositoryWrapperU f
,f g
ILoggerServiceh v
loggerw }
)} ~
{ 
_mapper 
= 
mapper 
; 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
RelatedFigureDTO) 9
>9 :
>: ;
>; <
Handle= C
(C D/
#GetRelatedFigureByStreetcodeIdQueryD g
requesth o
,o p
CancellationToken	q Ç
cancellationToken
É î
)
î ï
{ 
var 
relatedFigureIds 
= -
!GetRelatedFigureIdsByStreetcodeId @
(@ A
requestA H
.H I
StreetcodeIdI U
)U V
;V W
if 

( 
relatedFigureIds 
is 
null  $
)$ %
{   	
string!! 
errorMsg!! 
=!! 
$"!!  
$str!!  T
{!!T U
request!!U \
.!!\ ]
StreetcodeId!!] i
}!!i j
"!!j k
;!!k l
_logger"" 
."" 
LogError"" 
("" 
request"" $
,""$ %
errorMsg""& .
)"". /
;""/ 0
return## 
Result## 
.## 
Fail## 
(## 
new## "
Error### (
(##( )
errorMsg##) 1
)##1 2
)##2 3
;##3 4
}$$ 	
var&& 
relatedFigures&& 
=&& 
await&& "
_repositoryWrapper&&# 5
.&&5 6 
StreetcodeRepository&&6 J
.&&J K
GetAllAsync&&K V
(&&V W
	predicate''
 
:'' 
sc'' 
=>'' 
relatedFigureIds'' +
.''+ ,
Any'', /
(''/ 0
id''0 2
=>''3 5
id''6 8
==''9 ;
sc''< >
.''> ?
Id''? A
)''A B
&&''C E
sc''F H
.''H I
Status''I O
==''P R
DAL''S V
.''V W
Enums''W \
.''\ ]
StreetcodeStatus''] m
.''m n
	Published''n w
,''w x
include((
 
:(( 
scl(( 
=>(( 
scl(( 
.(( 
Include(( %
(((% &
sc((& (
=>(() +
sc((, .
.((. /
Images((/ 5
)((5 6
.((6 7
ThenInclude((7 B
(((B C
img((C F
=>((G I
img((J M
.((M N
ImageDetails((N Z
)((Z [
.)) 
Include)) %
())% &
sc))& (
=>))) +
sc)), .
.)). /
Tags))/ 3
)))3 4
)))4 5
;))5 6
if++ 

(++ 
relatedFigures++ 
is++ 
null++ "
)++" #
{,, 	
string-- 
errorMsg-- 
=-- 
$"--  
$str--  T
{--T U
request--U \
.--\ ]
StreetcodeId--] i
}--i j
"--j k
;--k l
_logger.. 
... 
LogError.. 
(.. 
request.. $
,..$ %
errorMsg..& .
)... /
;../ 0
return// 
Result// 
.// 
Fail// 
(// 
new// "
Error//# (
(//( )
errorMsg//) 1
)//1 2
)//2 3
;//3 4
}00 	
foreach22 
(22 
StreetcodeContent22 !

streetcode22" ,
in22- /
relatedFigures220 >
)22> ?
{33 	
if44 
(44 

streetcode44 
.44 
Images44  
!=44! #
null44$ (
)44( )
{55 

streetcode66 
.66 
Images66 !
=66" #

streetcode66$ .
.66. /
Images66/ 5
.665 6
OrderBy666 =
(66= >
img66> A
=>66B D
img66E H
.66H I
ImageDetails66I U
?66U V
.66V W
Alt66W Z
)66Z [
.66[ \
ToList66\ b
(66b c
)66c d
;66d e
}77 
}88 	
return:: 
Result:: 
.:: 
Ok:: 
(:: 
_mapper::  
.::  !
Map::! $
<::$ %
IEnumerable::% 0
<::0 1
RelatedFigureDTO::1 A
>::A B
>::B C
(::C D
relatedFigures::D R
)::R S
)::S T
;::T U
};; 
private== 

IQueryable== 
<== 
int== 
>== -
!GetRelatedFigureIdsByStreetcodeId== =
(=== >
int==> A
StreetcodeId==B N
)==N O
{>> 
try?? 
{@@ 	
varAA 
observerIdsAA 
=AA 
_repositoryWrapperAA 0
.AA0 1#
RelatedFigureRepositoryAA1 H
.BB 
FindAllBB 
(BB 
fBB 
=>BB 
fBB 
.BB 
TargetIdBB $
==BB% '
StreetcodeIdBB( 4
)BB4 5
.BB5 6
SelectBB6 <
(BB< =
oBB= >
=>BB? A
oBBB C
.BBC D

ObserverIdBBD N
)BBN O
;BBO P
varDD 
	targetIdsDD 
=DD 
_repositoryWrapperDD .
.DD. /#
RelatedFigureRepositoryDD/ F
.EE 
FindAllEE 
(EE 
fEE 
=>EE 
fEE 
.EE  

ObserverIdEE  *
==EE+ -
StreetcodeIdEE. :
)EE: ;
.EE; <
SelectEE< B
(EEB C
tEEC D
=>EEE G
tEEH I
.EEI J
TargetIdEEJ R
)EER S
;EES T
returnGG 
observerIdsGG 
.GG 
UnionGG $
(GG$ %
	targetIdsGG% .
)GG. /
.GG/ 0
DistinctGG0 8
(GG8 9
)GG9 :
;GG: ;
}HH 	
catchII 
(II !
ArgumentNullExceptionII $
)II$ %
{JJ 	
returnKK 
nullKK 
;KK 
}LL 	
}MM 
}NN ‰!
ûD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\Delete\DeleteRelatedFigureHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedFigure, 9
.9 :
Delete: @
;@ A
public 
class &
DeleteRelatedFigureHandler '
:( )
IRequestHandler* 9
<9 :&
DeleteRelatedFigureCommand: T
,T U
ResultV \
<\ ]
Unit] a
>a b
>b c
{		 
private

 
readonly

 
IRepositoryWrapper

 '
_repositoryWrapper

( :
;

: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
&
DeleteRelatedFigureHandler %
(% &
IRepositoryWrapper& 8
repositoryWrapper9 J
,J K
ILoggerServiceL Z
logger[ a
)a b
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +&
DeleteRelatedFigureCommand+ E
requestF M
,M N
CancellationTokenO `
cancellationTokena r
)r s
{ 
var 
relation 
= 
await 
_repositoryWrapper /
./ 0#
RelatedFigureRepository0 G
.  !"
GetFirstOrDefaultAsync! 7
(7 8
rel8 ;
=>< >
rel  #
.# $

ObserverId$ .
==/ 1
request2 9
.9 :

ObserverId: D
&&E G
rel  #
.# $
TargetId$ ,
==- /
request0 7
.7 8
TargetId8 @
)@ A
;A B
if 

( 
relation 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  c
{c d
requestd k
.k l

ObserverIdl v
}v w
$strw z
{z {
request	{ Ç
.
Ç É
TargetId
É ã
}
ã å
"
å ç
;
ç é
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
} 	
_repositoryWrapper!! 
.!! #
RelatedFigureRepository!! 2
.!!2 3
Delete!!3 9
(!!9 :
relation!!: B
)!!B C
;!!C D
var## 
resultIsSuccess## 
=## 
await## #
_repositoryWrapper##$ 6
.##6 7
SaveChangesAsync##7 G
(##G H
)##H I
>##J K
$num##L M
;##M N
if$$ 

($$
 
resultIsSuccess$$ 
)$$ 
{%% 	
return&& 
Result&& 
.&& 
Ok&& 
(&& 
Unit&& !
.&&! "
Value&&" '
)&&' (
;&&( )
}'' 	
else(( 
{)) 	
const** 
string** 
errorMsg** !
=**" #
$str**$ B
;**B C
_logger++ 
.++ 
LogError++ 
(++ 
request++ $
,++$ %
errorMsg++& .
)++. /
;++/ 0
return,, 
Result,, 
.,, 
Fail,, 
(,, 
new,, "
Error,,# (
(,,( )
errorMsg,,) 1
),,1 2
),,2 3
;,,3 4
}-- 	
}.. 
}// ˆ
ûD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\RelatedFigure\Delete\DeleteRelatedFigureCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
RelatedFigure, 9
.9 :
Delete: @
;@ A
public 
record &
DeleteRelatedFigureCommand (
(( )
int) ,

ObserverId- 7
,7 8
int9 <
TargetId= E
)E F
:G H
IRequestI Q
<Q R
ResultR X
<X Y
UnitY ]
>] ^
>^ _
;_ `Ä
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Fact\GetByStreetcodeId\GetFactByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Fact, 0
.0 1
GetByStreetcodeId1 B
;B C
public 
record &
GetFactByStreetcodeIdQuery (
(( )
int) ,
StreetcodeId- 9
)9 :
:; <
IRequest= E
<E F
ResultF L
<L M
IEnumerableM X
<X Y
FactDtoY `
>` a
>a b
>b c
;c d™
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Fact\GetByStreetcodeId\GetFactByStreetcodeIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !

Streetcode		! +
.		+ ,
Fact		, 0
.		0 1
GetByStreetcodeId		1 B
;		B C
public 
class (
GetFactByStreetcodeIdHandler )
:* +
IRequestHandler, ;
<; <&
GetFactByStreetcodeIdQuery< V
,V W
ResultX ^
<^ _
IEnumerable_ j
<j k
FactDtok r
>r s
>s t
>t u
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
(
GetFactByStreetcodeIdHandler '
(' (
IRepositoryWrapper( :
repositoryWrapper; L
,L M
IMapperN U
mapperV \
,\ ]
ILoggerService^ l
loggerm s
)s t
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
FactDto) 0
>0 1
>1 2
>2 3
Handle4 :
(: ;&
GetFactByStreetcodeIdQuery; U
requestV ]
,] ^
CancellationToken_ p
cancellationToken	q Ç
)
Ç É
{ 
var 
fact 
= 
await 
_repositoryWrapper +
.+ ,
FactRepository, :
. 
GetAllAsync 
( 
f 
=> 
f 
.  
StreetcodeId  ,
==- /
request0 7
.7 8
StreetcodeId8 D
)D E
;E F
if 

( 
fact 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  K
{K L
requestL S
.S T
StreetcodeIdT `
}` a
"a b
;b c
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$  
.$$  !
Map$$! $
<$$$ %
IEnumerable$$% 0
<$$0 1
FactDto$$1 8
>$$8 9
>$$9 :
($$: ;
fact$$; ?
)$$? @
)$$@ A
;$$A B
}%% 
}&& ó
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Fact\GetById\GetFactByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Fact, 0
.0 1
GetById1 8
;8 9
public 
record 
GetFactByIdQuery 
( 
int "
Id# %
)% &
:' (
IRequest) 1
<1 2
Result2 8
<8 9
FactDto9 @
>@ A
>A B
;B C¨
éD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Fact\GetById\GetFactByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Fact, 0
.0 1
GetById1 8
;8 9
public

 
class

 
GetFactByIdHandler

 
:

  !
IRequestHandler

" 1
<

1 2
GetFactByIdQuery

2 B
,

B C
Result

D J
<

J K
FactDto

K R
>

R S
>

S T
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetFactByIdHandler 
( 
IRepositoryWrapper 0
repositoryWrapper1 B
,B C
IMapperD K
mapperL R
,R S
ILoggerServiceT b
loggerc i
)i j
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
FactDto $
>$ %
>% &
Handle' -
(- .
GetFactByIdQuery. >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 
var 
facts 
= 
await 
_repositoryWrapper ,
., -
FactRepository- ;
.; <"
GetFirstOrDefaultAsync< R
(R S
fS T
=>U W
fX Y
.Y Z
IdZ \
==] _
request` g
.g h
Idh j
)j k
;k l
if 

( 
facts 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  L
{L M
requestM T
.T U
IdU W
}W X
"X Y
;Y Z
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
}   	
return"" 
Result"" 
."" 
Ok"" 
("" 
_mapper""  
.""  !
Map""! $
<""$ %
FactDto""% ,
>"", -
(""- .
facts"". 3
)""3 4
)""4 5
;""5 6
}## 
}$$ ç
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Fact\GetAll\GetAllFactsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !

Streetcode! +
.+ ,
Fact, 0
.0 1
GetAll1 7
;7 8
public 
record 
GetAllFactsQuery 
:  
IRequest! )
<) *
Result* 0
<0 1
IEnumerable1 <
<< =
FactDto= D
>D E
>E F
>F G
;G HÙ
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Streetcode\Fact\GetAll\GetAllFactsHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !

Streetcode		! +
.		+ ,
Fact		, 0
.		0 1
GetAll		1 7
;		7 8
public 
class 
GetAllFactsHandler 
:  !
IRequestHandler" 1
<1 2
GetAllFactsQuery2 B
,B C
ResultD J
<J K
IEnumerableK V
<V W
FactDtoW ^
>^ _
>_ `
>` a
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAllFactsHandler 
( 
IRepositoryWrapper 0
repositoryWrapper1 B
,B C
IMapperD K
mapperL R
,R S
ILoggerServiceT b
loggerc i
)i j
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
FactDto) 0
>0 1
>1 2
>2 3
Handle4 :
(: ;
GetAllFactsQuery; K
requestL S
,S T
CancellationTokenU f
cancellationTokeng x
)x y
{ 
var 
facts 
= 
await 
_repositoryWrapper ,
., -
FactRepository- ;
.; <
GetAllAsync< G
(G H
)H I
;I J
if 

( 
facts 
is 
null 
) 
{ 	
const 
string 
errorMsg !
=" #
$"$ &
$str& :
": ;
;; <
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return   
Result   
.   
Fail   
(   
new   "
Error  # (
(  ( )
errorMsg  ) 1
)  1 2
)  2 3
;  3 4
}!! 	
return## 
Result## 
.## 
Ok## 
(## 
_mapper##  
.##  !
Map##! $
<##$ %
IEnumerable##% 0
<##0 1
FactDto##1 8
>##8 9
>##9 :
(##: ;
facts##; @
)##@ A
)##A B
;##B C
}$$ 
}%% Ù
≈D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetCategoryContentByStreetcodeId\GetCategoryContentByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Sources! (
.( )
SourceLinkCategory) ;
.; <,
 GetCategoryContentByStreetcodeId< \
{ 
public 

record 1
%GetCategoryContentByStreetcodeIdQuery 7
(7 8
int8 ;
streetcodeId< H
,H I
intJ M

categoryIdN X
)X Y
:Z [
IRequest\ d
<d e
Resulte k
<k l)
StreetcodeCategoryContentDTO	l à
>
à â
>
â ä
;
ä ã
}		 ’$
«D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetCategoryContentByStreetcodeId\GetCategoryContentByStreetcodeIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Sources		! (
.		( )
SourceLinkCategory		) ;
.		; <,
 GetCategoryContentByStreetcodeId		< \
{

 
public 

class 3
'GetCategoryContentByStreetcodeIdHandler 8
:9 :
IRequestHandler; J
<J K1
%GetCategoryContentByStreetcodeIdQueryK p
,p q
Resultr x
<x y)
StreetcodeCategoryContentDTO	y ï
>
ï ñ
>
ñ ó
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 3
'GetCategoryContentByStreetcodeIdHandler 6
(6 7
IRepositoryWrapper7 I
repositoryWrapperJ [
,[ \
IMapper] d
mappere k
,k l
ILoggerServicem {
logger	| Ç
)
Ç É
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !(
StreetcodeCategoryContentDTO! =
>= >
>> ?
Handle@ F
(F G1
%GetCategoryContentByStreetcodeIdQueryG l
requestm t
,t u
CancellationToken	v á
cancellationToken
à ô
)
ô ö
{ 	
if 
( 
( 
await 
_repositoryWrapper (
.( ) 
StreetcodeRepository) =
. "
GetFirstOrDefaultAsync '
(' (
s( )
=>* ,
s- .
.. /
Id/ 1
==2 4
request5 <
.< =
streetcodeId= I
)I J
)J K
==L N
nullO S
)S T
{ 
string 
errorMsg 
=  !
$"" $
$str$ A
{A B
requestB I
.I J
streetcodeIdJ V
}V W
"W X
;X Y
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return 
Result 
. 
Fail "
(" #
new# &
Error' ,
(, -
errorMsg- 5
)5 6
)6 7
;7 8
}   
var"" 
streetcodeContent"" !
=""" #
await""$ )
_repositoryWrapper""* <
.""< =/
#StreetcodeCategoryContentRepository""= `
.## "
GetFirstOrDefaultAsync## '
(##' (
sc$$ 
=>$$ 
sc$$ 
.$$ 
StreetcodeId$$ )
==$$* ,
request$$- 4
.$$4 5
streetcodeId$$5 A
&&$$B D
sc$$E G
.$$G H 
SourceLinkCategoryId$$H \
==$$] _
request$$` g
.$$g h

categoryId$$h r
)$$r s
;$$s t
if&& 
(&& 
streetcodeContent&& !
==&&" $
null&&% )
)&&) *
{'' 
string(( 
errorMsg(( 
=((  !
$str((" B
;((B C
_logger)) 
.)) 
LogError))  
())  !
request))! (
,))( )
errorMsg))* 2
)))2 3
;))3 4
return** 
Result** 
.** 
Fail** "
(**" #
new**# &
Error**' ,
(**, -
errorMsg**- 5
)**5 6
)**6 7
;**7 8
}++ 
return-- 
Result-- 
.-- 
Ok-- 
(-- 
_mapper-- $
.--$ %
Map--% (
<--( )(
StreetcodeCategoryContentDTO--) E
>--E F
(--F G
streetcodeContent--G X
)--X Y
)--Y Z
;--Z [
}.. 	
}// 
}00 À
£D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetCategoryById\GetCategoryByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Sources! (
.( )

SourceLink) 3
.3 4
GetCategoryById4 C
;C D
public 
record  
GetCategoryByIdQuery "
(" #
int# &
Id' )
)) *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =!
SourceLinkCategoryDTO= R
>R S
>S T
;T Uœ#
•D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetCategoryById\GetCategoryByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Sources! (
.( )

SourceLink) 3
.3 4
GetCategoryById4 C
;C D
public 
class "
GetCategoryByIdHandler #
:$ %
IRequestHandler& 5
<5 6 
GetCategoryByIdQuery6 J
,J K
ResultL R
<R S!
SourceLinkCategoryDTOS h
>h i
>i j
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
"
GetCategoryByIdHandler !
(! "
IRepositoryWrapper 
repositoryWrapper ,
,, -
IMapper 
mapper 
, 
IBlobService 
blobService  
,  !
ILoggerService 
logger 
) 
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public   

async   
Task   
<   
Result   
<   !
SourceLinkCategoryDTO   2
>  2 3
>  3 4
Handle  5 ;
(  ; < 
GetCategoryByIdQuery  < P
request  Q X
,  X Y
CancellationToken  Z k
cancellationToken  l }
)  } ~
{!! 
var"" 
srcCategories"" 
="" 
await"" !
_repositoryWrapper""" 4
.## $
SourceCategoryRepository## %
.$$ "
GetFirstOrDefaultAsync$$ #
($$# $
	predicate%% 
:%% 
sc%% 
=>%%  
sc%%! #
.%%# $
Id%%$ &
==%%' )
request%%* 1
.%%1 2
Id%%2 4
,%%4 5
include&& 
:&& 
scl&& 
=>&& 
scl&&  #
.'' 
Include'' 
('' 
sc'' 
=>''  "
sc''# %
.''% &&
StreetcodeCategoryContents''& @
)''@ A
.(( 
Include(( 
((( 
sc(( 
=>((  "
sc((# %
.((% &
Image((& +
)((+ ,
!((- .
)((. /
;((/ 0
if** 

(** 
srcCategories** 
is** 
null** !
)**! "
{++ 	
string,, 
errorMsg,, 
=,, 
$",,  
$str,,  U
{,,U V
request,,V ]
.,,] ^
Id,,^ `
},,` a
",,a b
;,,b c
_logger-- 
.-- 
LogError-- 
(-- 
request-- $
,--$ %
errorMsg--& .
)--. /
;--/ 0
return.. 
Result.. 
... 
Fail.. 
(.. 
new.. "
Error..# (
(..( )
errorMsg..) 1
)..1 2
)..2 3
;..3 4
}// 	
var11 
mappedSrcCategories11 
=11  !
_mapper11" )
.11) *
Map11* -
<11- .!
SourceLinkCategoryDTO11. C
>11C D
(11D E
srcCategories11E R
)11R S
;11S T
mappedSrcCategories33 
.33 
Image33 !
.33! "
Base6433" (
=33) *
_blobService33+ 7
.337 8%
FindFileInStorageAsBase64338 Q
(33Q R
mappedSrcCategories33R e
.33e f
Image33f k
.33k l
BlobName33l t
)33t u
;33u v
return55 
Result55 
.55 
Ok55 
(55 
mappedSrcCategories55 ,
)55, -
;55- .
}66 
}77 Ã
ûD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetAll\GetAllCategoryNamesQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Sources! (
.( )
SourceLinkCategory) ;
.; <
GetAll< B
{ 
public 

record $
GetAllCategoryNamesQuery *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =
IEnumerable= H
<H I
CategoryWithNameDTOI \
>\ ]
>] ^
>^ _
;_ `
}		 È&
ΩD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetCategoriesByStreetcodeId\GetCategoriesByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Sources! (
.( )

SourceLink) 3
.3 4'
GetCategoriesByStreetcodeId4 O
;O P
public 
class .
"GetCategoriesByStreetcodeIdHandler /
:0 1
IRequestHandler2 A
<A B,
 GetCategoriesByStreetcodeIdQueryB b
,b c
Resultd j
<j k
IEnumerablek v
<v w"
SourceLinkCategoryDTO	w å
>
å ç
>
ç é
>
é è
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
.
"GetCategoriesByStreetcodeIdHandler -
(- .
IRepositoryWrapper. @
repositoryWrapperA R
,R S
IMapperT [
mapper\ b
,b c
IBlobServiced p
blobServiceq |
,| }
ILoggerService	~ å
logger
ç ì
)
ì î
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )!
SourceLinkCategoryDTO) >
>> ?
>? @
>@ A
HandleB H
(H I,
 GetCategoriesByStreetcodeIdQueryI i
requestj q
,q r
CancellationToken	s Ñ
cancellationToken
Ö ñ
)
ñ ó
{ 
var 
srcCategories 
= 
await !
_repositoryWrapper" 4
. $
SourceCategoryRepository %
.   
GetAllAsync   
(   
	predicate!! 
:!! 
sc!! 
=>!!  
sc!!! #
.!!# $
Streetcodes!!$ /
.!!/ 0
Any!!0 3
(!!3 4
s!!4 5
=>!!6 8
s!!9 :
.!!: ;
Id!!; =
==!!> @
request!!A H
.!!H I
StreetcodeId!!I U
)!!U V
,!!V W
include"" 
:"" 
scl"" 
=>"" 
scl""  #
.""# $
Include""$ +
(""+ ,
sc"", .
=>""/ 1
sc""2 4
.""4 5
Image""5 :
)"": ;
!""< =
)""= >
;""> ?
if$$ 

($$ 
srcCategories$$ 
is$$ 
null$$ !
)$$! "
{%% 	
string&& 
errorMsg&& 
=&& 
$"&&  
$str&&  U
{&&U V
request&&V ]
.&&] ^
StreetcodeId&&^ j
}&&j k
"&&k l
;&&l m
_logger'' 
.'' 
LogError'' 
('' 
request'' $
,''$ %
errorMsg''& .
)''. /
;''/ 0
return(( 
Result(( 
.(( 
Fail(( 
((( 
new(( "
Error((# (
(((( )
errorMsg(() 1
)((1 2
)((2 3
;((3 4
})) 	
var++ 
mappedSrcCategories++ 
=++  !
_mapper++" )
.++) *
Map++* -
<++- .
IEnumerable++. 9
<++9 :!
SourceLinkCategoryDTO++: O
>++O P
>++P Q
(++Q R
srcCategories++R _
)++_ `
;++` a
foreach-- 
(-- 
var-- 
srcCategory--  
in--! #
mappedSrcCategories--$ 7
)--7 8
{.. 	
srcCategory// 
.// 
Image// 
.// 
Base64// $
=//% &
_blobService//' 3
.//3 4%
FindFileInStorageAsBase64//4 M
(//M N
srcCategory//N Y
.//Y Z
Image//Z _
.//_ `
BlobName//` h
)//h i
;//i j
}00 	
return22 
Result22 
.22 
Ok22 
(22 
mappedSrcCategories22 ,
)22, -
;22- .
}33 
}44 º
ªD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetCategoriesByStreetcodeId\GetCategoriesByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Sources! (
.( )

SourceLink) 3
.3 4'
GetCategoriesByStreetcodeId4 O
;O P
public 
record ,
 GetCategoriesByStreetcodeIdQuery .
(. /
int/ 2
StreetcodeId3 ?
)? @
:A B
IRequestC K
<K L
ResultL R
<R S
IEnumerableS ^
<^ _!
SourceLinkCategoryDTO_ t
>t u
>u v
>v w
;w xè
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetAll\GetAllCategoryNamesHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Sources		! (
.		( )
SourceLinkCategory		) ;
.		; <
GetAll		< B
{

 
public 

class &
GetAllCategoryNamesHandler +
:, -
IRequestHandler. =
<= >$
GetAllCategoryNamesQuery> V
,V W
ResultX ^
<^ _
IEnumerable_ j
<j k
CategoryWithNameDTOk ~
>~ 
>	 Ä
>
Ä Å
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public &
GetAllCategoryNamesHandler )
() *
IRepositoryWrapper* <
repositoryWrapper= N
,N O
IMapperP W
mapperX ^
,^ _
ILoggerService` n
loggero u
)u v
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
CategoryWithNameDTO- @
>@ A
>A B
>B C
HandleD J
(J K$
GetAllCategoryNamesQueryK c
requestd k
,k l
CancellationTokenm ~
cancellationToken	 ê
)
ê ë
{ 	
var 
allCategories 
= 
await  %
_repositoryWrapper& 8
.8 9$
SourceCategoryRepository9 Q
.Q R
GetAllAsyncR ]
(] ^
)^ _
;_ `
if 
( 
allCategories 
==  
null! %
)% &
{ 
const 
string 
errorMsg %
=& '
$"( *
$str* <
"< =
;= >
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return   
Result   
.   
Fail   "
(  " #
new  # &
Error  ' ,
(  , -
errorMsg  - 5
)  5 6
)  6 7
;  7 8
}!! 
return## 
Result## 
.## 
Ok## 
(## 
_mapper## $
.##$ %
Map##% (
<##( )
IEnumerable##) 4
<##4 5
CategoryWithNameDTO##5 H
>##H I
>##I J
(##J K
allCategories##K X
)##X Y
)##Y Z
;##Z [
}$$ 	
}%% 
}&& »
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetAll\GetAllCategoriesQuery.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Sources

! (
.

( )
SourceLinkCategory

) ;
.

; <
GetAll

< B
{ 
public 

record !
GetAllCategoriesQuery '
:( )
IRequest* 2
<2 3
Result3 9
<9 :
IEnumerable: E
<E F!
SourceLinkCategoryDTOF [
>[ \
>\ ]
>] ^
;^ _
} ¶"
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Sources\SourceLinkCategory\GetAll\GetAllCategoriesHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Sources! (
.( )
SourceLinkCategory) ;
.; <
GetAll< B
{ 
public 

class #
GetAllCategoriesHandler (
:) *
IRequestHandler+ :
<: ;!
GetAllCategoriesQuery; P
,P Q
ResultR X
<X Y
IEnumerableY d
<d e!
SourceLinkCategoryDTOe z
>z {
>{ |
>| }
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public #
GetAllCategoriesHandler &
(& '
IRepositoryWrapper' 9
repositoryWrapper: K
,K L
IMapperM T
mapperU [
,[ \
IBlobService] i
blobServicej u
,u v
ILoggerService	w Ö
logger
Ü å
)
å ç
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -!
SourceLinkCategoryDTO- B
>B C
>C D
>D E
HandleF L
(L M!
GetAllCategoriesQueryM b
requestc j
,j k
CancellationTokenl }
cancellationtoken	~ è
)
è ê
{ 	
var 
allCategories 
= 
await  %
_repositoryWrapper& 8
.8 9$
SourceCategoryRepository9 Q
.Q R
GetAllAsyncR ]
(] ^
include 
: 
cat 
=> 
cat  #
.# $
Include$ +
(+ ,
img, /
=>0 2
img3 6
.6 7
Image7 <
)< =
!> ?
)? @
;@ A
if   
(   
allCategories   
==    
null  ! %
)  % &
{!! 
const"" 
string"" 
errorMsg"" %
=""& '
$"""( *
$str""* <
"""< =
;""= >
_logger## 
.## 
LogError##  
(##  !
request##! (
,##( )
errorMsg##* 2
)##2 3
;##3 4
return$$ 
Result$$ 
.$$ 
Fail$$ "
($$" #
new$$# &
Error$$' ,
($$, -
errorMsg$$- 5
)$$5 6
)$$6 7
;$$7 8
}%% 
var'' 
dtos'' 
='' 
_mapper'' 
.'' 
Map'' "
<''" #
IEnumerable''# .
<''. /!
SourceLinkCategoryDTO''/ D
>''D E
>''E F
(''F G
allCategories''G T
)''T U
;''U V
foreach)) 
()) 
var)) 
dto)) 
in)) 
dtos))  $
)))$ %
{** 
dto++ 
.++ 
Image++ 
.++ 
Base64++  
=++! "
_blobService++# /
.++/ 0%
FindFileInStorageAsBase64++0 I
(++I J
dto++J M
.++M N
Image++N S
.++S T
BlobName++T \
)++\ ]
;++] ^
},, 
return.. 
Result.. 
... 
Ok.. 
(.. 
dtos.. !
)..! "
;.." #
}// 	
}00 
}11 ¿
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Payment\CreateInvoiceHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Payment! (
{ 
public		 

class		  
CreateInvoiceHandler		 %
:		& '
IRequestHandler		( 7
<		7 8 
CreateInvoiceCommand		8 L
,		L M
Result		N T
<		T U
InvoiceInfo		U `
>		` a
>		a b
{

 
private 
const 
int  
_hryvnyaCurrencyCode .
=/ 0
$num1 4
;4 5
private 
const 
int 
_currencyMultiplier -
=. /
$num0 3
;3 4
private 
readonly 
IPaymentService (
_paymentService) 8
;8 9
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public  
CreateInvoiceHandler #
(# $
IPaymentService$ 3
paymentService4 B
,B C
ILoggerServiceD R
loggerS Y
)Y Z
{ 	
_paymentService 
= 
paymentService ,
;, -
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
InvoiceInfo! ,
>, -
>- .
Handle/ 5
(5 6 
CreateInvoiceCommand6 J
requestK R
,R S
CancellationTokenT e
cancellationTokenf w
)w x
{ 	
var 
invoice 
= 
new 
Invoice %
(% &
request& -
.- .
Payment. 5
.5 6
Amount6 <
*= >
_currencyMultiplier? R
,R S 
_hryvnyaCurrencyCodeT h
,h i
newj m 
MerchantPaymentInfo	n Å
{
Ç É
Destination
Ñ è
=
ê ë
$str
í ◊
}
ÿ Ÿ
,
Ÿ ⁄
request
€ ‚
.
‚ „
Payment
„ Í
.
Í Î
RedirectUrl
Î ˆ
)
ˆ ˜
;
˜ ¯
return 
Result 
. 
Ok 
( 
await "
_paymentService# 2
.2 3
CreateInvoiceAsync3 E
(E F
invoiceF M
)M N
)N O
;O P
} 	
} 
} ∫
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\ResultVariations\NullResult.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
ResultVariations! 1
{ 
public 

class 

NullResult 
< 
T 
> 
:  
Result! '
<' (
T( )
>) *
{ 
public 

NullResult 
( 
) 
: 
base 
( 
) 
{		 	
}

 	
} 
} ◊
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Payment\CreateInvoiceCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Payment! (
;( )
public 
record  
CreateInvoiceCommand "
(" #

PaymentDTO# -
Payment. 5
)5 6
:7 8
IRequest9 A
<A B
ResultB H
<H I
InvoiceInfoI T
>T U
>U V
;V Wë
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\Update\UpdatePartnerQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
Update* 0
{ 
public 
record	 
UpdatePartnerQuery "
(" #
CreatePartnerDTO# 3
Partner4 ;
); <
:= >
IRequest? G
<G H
ResultH N
<N O

PartnerDTOO Y
>Y Z
>Z [
;[ \
}		 Ÿ:
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\Update\UpdatePartnerHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Partners		! )
.		) *
Update		* 0
{

 
public 

class  
UpdatePartnerHandler %
:& '
IRequestHandler( 7
<7 8
UpdatePartnerQuery8 J
,J K
ResultL R
<R S

PartnerDTOS ]
>] ^
>^ _
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public  
UpdatePartnerHandler #
(# $
IRepositoryWrapper$ 6
repositoryWrapper7 H
,H I
IMapperJ Q
mapperR X
,X Y
ILoggerServiceZ h
loggeri o
)o p
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !

PartnerDTO! +
>+ ,
>, -
Handle. 4
(4 5
UpdatePartnerQuery5 G
requestH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 	
var 
partner 
= 
_mapper !
.! "
Map" %
<% &
Partner& -
>- .
(. /
request/ 6
.6 7
Partner7 >
)> ?
;? @
try 
{ 
var 
links 
= 
await !
_repositoryWrapper" 4
.4 5'
PartnerSourceLinkRepository5 P
. 
GetAllAsync 
(  
	predicate  )
:) *
l+ ,
=>- /
l0 1
.1 2
	PartnerId2 ;
==< >
partner? F
.F G
IdG I
)I J
;J K
var!! 

newLinkIds!! 
=!!  
partner!!! (
.!!( )
PartnerSourceLinks!!) ;
.!!; <
Select!!< B
(!!B C
l!!C D
=>!!E G
l!!H I
.!!I J
Id!!J L
)!!L M
.!!M N
ToList!!N T
(!!T U
)!!U V
;!!V W
foreach## 
(## 
var## 
link## !
in##" $
links##% *
)##* +
{$$ 
if%% 
(%% 
!%% 

newLinkIds%% #
.%%# $
Contains%%$ ,
(%%, -
link%%- 1
.%%1 2
Id%%2 4
)%%4 5
)%%5 6
{&& 
_repositoryWrapper'' *
.''* +'
PartnerSourceLinkRepository''+ F
.''F G
Delete''G M
(''M N
link''N R
)''R S
;''S T
}(( 
})) 
partner++ 
.++ 
Streetcodes++ #
.++# $
Clear++$ )
(++) *
)++* +
;+++ ,
_repositoryWrapper,, "
.,," #
PartnersRepository,,# 5
.,,5 6
Update,,6 <
(,,< =
partner,,= D
),,D E
;,,E F
await-- 
_repositoryWrapper-- (
.--( )
SaveChangesAsync--) 9
(--9 :
)--: ;
;--; <
var.. 
newStreetcodeIds.. $
=..% &
request..' .
.... /
Partner../ 6
...6 7
Streetcodes..7 B
...B C
Select..C I
(..I J
s..J K
=>..L N
s..O P
...P Q
Id..Q S
)..S T
...T U
ToList..U [
(..[ \
)..\ ]
;..] ^
var// 
oldStreetcodes// "
=//# $
await//% *
_repositoryWrapper//+ =
.//= >'
PartnerStreetcodeRepository//> Y
.00 
GetAllAsync00  
(00  !
ps00! #
=>00$ &
ps00' )
.00) *
	PartnerId00* 3
==004 6
partner007 >
.00> ?
Id00? A
)00A B
;00B C
foreach22 
(22 
var22 
old22  
in22! #
oldStreetcodes22$ 2
!222 3
)223 4
{33 
if44 
(44 
!44 
newStreetcodeIds44 )
.44) *
Contains44* 2
(442 3
old443 6
.446 7
StreetcodeId447 C
)44C D
)44D E
{55 
_repositoryWrapper66 *
.66* +'
PartnerStreetcodeRepository66+ F
.66F G
Delete66G M
(66M N
old66N Q
)66Q R
;66R S
}77 
}88 
foreach:: 
(:: 
var:: 
	newCodeId:: &
in::' )
newStreetcodeIds::* :
!::: ;
)::; <
{;; 
if<< 
(<< 
oldStreetcodes<< &
.<<& '
FirstOrDefault<<' 5
(<<5 6
x<<6 7
=><<8 :
x<<; <
.<<< =
StreetcodeId<<= I
==<<J L
	newCodeId<<M V
)<<V W
==<<X Z
null<<[ _
)<<_ `
{== 
_repositoryWrapper>> *
.>>* +'
PartnerStreetcodeRepository>>+ F
.>>F G
CreateAsync>>G R
(>>R S
new?? 
StreetcodePartner??  1
(??1 2
)??2 3
{??4 5
	PartnerId??6 ?
=??@ A
partner??B I
.??I J
Id??J L
,??L M
StreetcodeId??N Z
=??[ \
	newCodeId??] f
}??g h
)??h i
;??i j
}@@ 
}AA 
awaitCC 
_repositoryWrapperCC (
.CC( )
SaveChangesAsyncCC) 9
(CC9 :
)CC: ;
;CC; <
varDD 
dboDD 
=DD 
_mapperDD !
.DD! "
MapDD" %
<DD% &

PartnerDTODD& 0
>DD0 1
(DD1 2
partnerDD2 9
)DD9 :
;DD: ;
dboEE 
.EE 
StreetcodesEE 
=EE  !
requestEE" )
.EE) *
PartnerEE* 1
.EE1 2
StreetcodesEE2 =
;EE= >
returnFF 
ResultFF 
.FF 
OkFF  
(FF  !
dboFF! $
)FF$ %
;FF% &
}GG 
catchHH 
(HH 
	ExceptionHH 
exHH 
)HH  
{II 
_loggerJJ 
.JJ 
LogErrorJJ  
(JJ  !
requestJJ! (
,JJ( )
exJJ* ,
.JJ, -
MessageJJ- 4
)JJ4 5
;JJ5 6
returnKK 
ResultKK 
.KK 
FailKK "
(KK" #
exKK# %
.KK% &
MessageKK& -
)KK- .
;KK. /
}LL 
}MM 	
}NN 
}OO ·
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetByStreetcodeId\GetPartnersByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
GetByStreetcodeId* ;
;; <
public 
record *
GetPartnersByStreetcodeIdQuery ,
(, -
int- 0
StreetcodeId1 =
)= >
:? @
IRequestA I
<I J
ResultJ P
<P Q
IEnumerableQ \
<\ ]

PartnerDTO] g
>g h
>h i
>i j
;j k∞(
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetByStreetcodeId\GetPartnersByStreetcodeIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Partners

! )
.

) *
GetByStreetcodeId

* ;
;

; <
public 
class ,
 GetPartnersByStreetcodeIdHandler -
:. /
IRequestHandler0 ?
<? @*
GetPartnersByStreetcodeIdQuery@ ^
,^ _
Result` f
<f g
IEnumerableg r
<r s

PartnerDTOs }
>} ~
>~ 
>	 Ä
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
,
 GetPartnersByStreetcodeIdHandler +
(+ ,
IMapper, 3
mapper4 :
,: ;
IRepositoryWrapper< N
repositoryWrapperO `
,` a
ILoggerServiceb p
loggerq w
)w x
{ 
_mapper 
= 
mapper 
; 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )

PartnerDTO) 3
>3 4
>4 5
>5 6
Handle7 =
(= >*
GetPartnersByStreetcodeIdQuery> \
request] d
,d e
CancellationTokenf w
cancellationToken	x â
)
â ä
{ 
var 

streetcode 
= 
await 
_repositoryWrapper 1
.1 2 
StreetcodeRepository2 F
. #
GetSingleOrDefaultAsync $
($ %
st% '
=>( *
st+ -
.- .
Id. 0
==1 3
request4 ;
.; <
StreetcodeId< H
)H I
;I J
if 

( 

streetcode 
is 
null 
) 
{ 	
string   
errorMsg   
=   
$"    
$str    [
{  [ \
request  \ c
.  c d
StreetcodeId  d p
}  p q
"  q r
;  r s
_logger!! 
.!! 
LogError!! 
(!! 
request!! $
,!!$ %
errorMsg!!& .
)!!. /
;!!/ 0
return"" 
Result"" 
."" 
Fail"" 
("" 
new"" "
Error""# (
(""( )
errorMsg"") 1
)""1 2
)""2 3
;""3 4
}## 	
var%% 
partners%% 
=%% 
await%% 
_repositoryWrapper%% /
.%%/ 0
PartnersRepository%%0 B
.&& 
GetAllAsync&& 
(&& 
	predicate'' 
:'' 
p'' 
=>'' 
p''  !
.''! "
Streetcodes''" -
.''- .
Any''. 1
(''1 2
sc''2 4
=>''5 7
sc''8 :
.'': ;
Id''; =
==''> @

streetcode''A K
.''K L
Id''L N
)''N O
||''P R
p''S T
.''T U
IsVisibleEverywhere''U h
,''h i
include(( 
:(( 
p(( 
=>(( 
p(( 
.((  
Include((  '
(((' (
pl((( *
=>((+ -
pl((. 0
.((0 1
PartnerSourceLinks((1 C
)((C D
)((D E
;((E F
if** 

(** 
partners** 
is** 
null** 
)** 
{++ 	
string,, 
errorMsg,, 
=,, 
$",,  
$str,,  K
{,,K L
request,,L S
.,,S T
StreetcodeId,,T `
},,` a
",,a b
;,,b c
_logger-- 
.-- 
LogError-- 
(-- 
request-- $
,--$ %
errorMsg--& .
)--. /
;--/ 0
return.. 
Result.. 
... 
Fail.. 
(.. 
new.. "
Error..# (
(..( )
errorMsg..) 1
)..1 2
)..2 3
;..3 4
}// 	
return11 
Result11 
.11 
Ok11 
(11 
value11 
:11 
_mapper11  '
.11' (
Map11( +
<11+ ,
IEnumerable11, 7
<117 8

PartnerDTO118 B
>11B C
>11C D
(11D E
partners11E M
)11M N
)11N O
;11O P
}22 
}33 ˆ
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetById\GetPartnerByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
GetById* 1
;1 2
public 
record 
GetPartnerByIdQuery !
(! "
int" %
Id& (
)( )
:* +
IRequest, 4
<4 5
Result5 ;
<; <

PartnerDTO< F
>F G
>G H
;H IÕ
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetById\GetPartnerByIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Partners		! )
.		) *
GetById		* 1
;		1 2
public 
class !
GetPartnerByIdHandler "
:# $
IRequestHandler% 4
<4 5
GetPartnerByIdQuery5 H
,H I
ResultJ P
<P Q

PartnerDTOQ [
>[ \
>\ ]
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
!
GetPartnerByIdHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
IMapperG N
mapperO U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 

PartnerDTO '
>' (
>( )
Handle* 0
(0 1
GetPartnerByIdQuery1 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
partner 
= 
await 
_repositoryWrapper .
. 
PartnersRepository 
. #
GetSingleOrDefaultAsync $
($ %
	predicate 
: 
p 
=> 
p  !
.! "
Id" $
==% '
request( /
./ 0
Id0 2
,2 3
include 
: 
p 
=> 
p 
. 
Include 
( 
pl 
=>  "
pl# %
.% &
PartnerSourceLinks& 8
)8 9
)9 :
;: ;
if!! 

(!! 
partner!! 
is!! 
null!! 
)!! 
{"" 	
string## 
errorMsg## 
=## 
$"##  
$str##  O
{##O P
request##P W
.##W X
Id##X Z
}##Z [
"##[ \
;##\ ]
_logger$$ 
.$$ 
LogError$$ 
($$ 
request$$ $
,$$$ %
errorMsg$$& .
)$$. /
;$$/ 0
return%% 
Result%% 
.%% 
Fail%% 
(%% 
new%% "
Error%%# (
(%%( )
errorMsg%%) 1
)%%1 2
)%%2 3
;%%3 4
}&& 	
return(( 
Result(( 
.(( 
Ok(( 
((( 
_mapper((  
.((  !
Map((! $
<(($ %

PartnerDTO((% /
>((/ 0
(((0 1
partner((1 8
)((8 9
)((9 :
;((: ;
})) 
}** Ï
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetAll\GetAllPartnersQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
GetAll* 0
;0 1
public 
record 
GetAllPartnersQuery !
:" #
IRequest$ ,
<, -
Result- 3
<3 4
IEnumerable4 ?
<? @

PartnerDTO@ J
>J K
>K L
>L M
;M N˙
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetAll\GetAllPartnersHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
GetAll* 0
;0 1
public 
class !
GetAllPartnersHandler "
:# $
IRequestHandler% 4
<4 5
GetAllPartnersQuery5 H
,H I
ResultJ P
<P Q
IEnumerableQ \
<\ ]

PartnerDTO] g
>g h
>h i
>i j
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
!
GetAllPartnersHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
IMapperG N
mapperO U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )

PartnerDTO) 3
>3 4
>4 5
>5 6
Handle7 =
(= >
GetAllPartnersQuery> Q
requestR Y
,Y Z
CancellationToken[ l
cancellationTokenm ~
)~ 
{ 
var 
partners 
= 
await 
_repositoryWrapper /
. 
PartnersRepository 
. 
GetAllAsync 
( 
include 
: 
p 
=> 
p 
.   
Include   
(   
pl   
=>    "
pl  # %
.  % &
PartnerSourceLinks  & 8
)  8 9
.!! 
Include!! 
(!! 
p!! 
=>!! !
p!!" #
.!!# $
Streetcodes!!$ /
)!!/ 0
)!!0 1
;!!1 2
if## 

(## 
partners## 
is## 
null## 
)## 
{$$ 	
const%% 
string%% 
errorMsg%% !
=%%" #
$"%%$ &
$str%%& >
"%%> ?
;%%? @
_logger&& 
.&& 
LogError&& 
(&& 
request&& $
,&&$ %
errorMsg&&& .
)&&. /
;&&/ 0
return'' 
Result'' 
.'' 
Fail'' 
('' 
new'' "
Error''# (
(''( )
errorMsg'') 1
)''1 2
)''2 3
;''3 4
}(( 	
return** 
Result** 
.** 
Ok** 
(** 
_mapper**  
.**  !
Map**! $
<**$ %
IEnumerable**% 0
<**0 1

PartnerDTO**1 ;
>**; <
>**< =
(**= >
partners**> F
)**F G
)**G H
;**H I
}++ 
},, †
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetAllPartnerShort\GetAllPartnersShortQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
GetAllPartnerShort* <
{ 
public 

record $
GetAllPartnersShortQuery *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =
IEnumerable= H
<H I
PartnerShortDTOI X
>X Y
>Y Z
>Z [
;[ \
} ¬
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\GetAllPartnerShort\GetAllPartnerShortHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Partners		! )
.		) *
GetAllPartnerShort		* <
{

 
internal 
class %
GetAllPartnerShortHandler ,
:- .
IRequestHandler/ >
<> ?$
GetAllPartnersShortQuery? W
,W X
ResultY _
<_ `
IEnumerable` k
<k l
PartnerShortDTOl {
>{ |
>| }
>} ~
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public %
GetAllPartnerShortHandler (
(( )
IRepositoryWrapper) ;
repositoryWrapper< M
,M N
IMapperO V
mapperW ]
,] ^
ILoggerService_ m
loggern t
)t u
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
PartnerShortDTO- <
>< =
>= >
>> ?
Handle@ F
(F G$
GetAllPartnersShortQueryG _
request` g
,g h
CancellationTokeni z
cancellationToken	{ å
)
å ç
{ 	
var 
partners 
= 
await  
_repositoryWrapper! 3
.3 4
PartnersRepository4 F
.F G
GetAllAsyncG R
(R S
)S T
;T U
if 
( 
partners 
is 
null  
)  !
{ 
const 
string 
errorMsg %
=& '
$"( *
$str* B
"B C
;C D
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return   
Result   
.   
Fail   "
(  " #
new  # &
Error  ' ,
(  , -
errorMsg  - 5
)  5 6
)  6 7
;  7 8
}!! 
return## 
Result## 
.## 
Ok## 
(## 
_mapper## $
.##$ %
Map##% (
<##( )
IEnumerable##) 4
<##4 5
PartnerShortDTO##5 D
>##D E
>##E F
(##F G
partners##G O
)##O P
)##P Q
;##Q R
}$$ 	
}%% 
}&& ˇ
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\Delete\DeletePartnerQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
Delete* 0
{ 
public 

record 
DeletePartnerQuery $
($ %
int% (
id) +
)+ ,
:- .
IRequest/ 7
<7 8
Result8 >
<> ?

PartnerDTO? I
>I J
>J K
;K L
} ç
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\Delete\DeletePartnerHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
Delete* 0
{		 
public

 

class

  
DeletePartnerHandler

 %
:

& '
IRequestHandler

( 7
<

7 8
DeletePartnerQuery

8 J
,

J K
Result

L R
<

R S

PartnerDTO

S ]
>

] ^
>

^ _
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public  
DeletePartnerHandler #
(# $
IRepositoryWrapper$ 6
repositoryWrapper7 H
,H I
IMapperJ Q
mapperR X
,X Y
ILoggerServiceZ h
loggeri o
)o p
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !

PartnerDTO! +
>+ ,
>, -
Handle. 4
(4 5
DeletePartnerQuery5 G
requestH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 	
var 
partner 
= 
await 
_repositoryWrapper  2
.2 3
PartnersRepository3 E
.E F"
GetFirstOrDefaultAsyncF \
(\ ]
p] ^
=>_ a
pb c
.c d
Idd f
==g i
requestj q
.q r
idr t
)t u
;u v
if 
( 
partner 
== 
null 
)  
{ 
const 
string 
errorMsg %
=& '
$str( A
;A B
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return 
Result 
. 
Fail "
(" #
errorMsg# +
)+ ,
;, -
} 
else   
{!! 
_repositoryWrapper"" "
.""" #
PartnersRepository""# 5
.""5 6
Delete""6 <
(""< =
partner""= D
)""D E
;""E F
try## 
{$$ 
await%% 
_repositoryWrapper%% ,
.%%, -
SaveChangesAsync%%- =
(%%= >
)%%> ?
;%%? @
return&& 
Result&& !
.&&! "
Ok&&" $
(&&$ %
_mapper&&% ,
.&&, -
Map&&- 0
<&&0 1

PartnerDTO&&1 ;
>&&; <
(&&< =
partner&&= D
)&&D E
)&&E F
;&&F G
}'' 
catch(( 
((( 
	Exception(( 
ex((  "
)((" #
{)) 
_logger** 
.** 
LogError** $
(**$ %
request**% ,
,**, -
ex**. 0
.**0 1
Message**1 8
)**8 9
;**9 :
return++ 
Result++ !
.++! "
Fail++" &
(++& '
ex++' )
.++) *
Message++* 1
)++1 2
;++2 3
},, 
}-- 
}.. 	
}// 
}00 î
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\Create\CreatePartnerQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Partners! )
.) *
Create* 0
{ 
public 
record	 
CreatePartnerQuery "
(" #
CreatePartnerDTO# 3

newPartner4 >
)> ?
:@ A
IRequestB J
<J K
ResultK Q
<Q R

PartnerDTOR \
>\ ]
>] ^
;^ _
} Ü#
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Partners\Create\CreatePartnerHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Partners		! )
.		) *
Create		* 0
{

 
public 

class  
CreatePartnerHandler %
:& '
IRequestHandler( 7
<7 8
CreatePartnerQuery8 J
,J K
ResultL R
<R S

PartnerDTOS ]
>] ^
>^ _
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public  
CreatePartnerHandler #
(# $
IRepositoryWrapper$ 6
repositoryWrapper7 H
,H I
IMapperJ Q
mapperR X
,X Y
ILoggerServiceZ h
loggeri o
)o p
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !

PartnerDTO! +
>+ ,
>, -
Handle. 4
(4 5
CreatePartnerQuery5 G
requestH O
,O P
CancellationTokenQ b
cancellationTokenc t
)t u
{ 	
var 

newPartner 
= 
_mapper $
.$ %
Map% (
<( )
Partner) 0
>0 1
(1 2
request2 9
.9 :

newPartner: D
)D E
;E F
try 
{ 

newPartner 
. 
Streetcodes &
.& '
Clear' ,
(, -
)- .
;. /

newPartner 
= 
await "
_repositoryWrapper# 5
.5 6
PartnersRepository6 H
.H I
CreateAsyncI T
(T U

newPartnerU _
)_ `
;` a
await   
_repositoryWrapper   (
.  ( )
SaveChangesAsync  ) 9
(  9 :
)  : ;
;  ; <
var"" 
streetcodeIds"" !
=""" #
request""$ +
.""+ ,

newPartner"", 6
.""6 7
Streetcodes""7 B
.""B C
Select""C I
(""I J
s""J K
=>""L N
s""O P
.""P Q
Id""Q S
)""S T
.""T U
ToList""U [
(""[ \
)""\ ]
;""] ^

newPartner## 
.## 
Streetcodes## &
.##& '
AddRange##' /
(##/ 0
await##0 5
_repositoryWrapper##6 H
.$$  
StreetcodeRepository$$ )
.%% 
GetAllAsync%%  
(%%  !
s%%! "
=>%%# %
streetcodeIds%%& 3
.%%3 4
Contains%%4 <
(%%< =
s%%= >
.%%> ?
Id%%? A
)%%A B
)%%B C
)%%C D
;%%D E
await'' 
_repositoryWrapper'' (
.''( )
SaveChangesAsync'') 9
(''9 :
)'': ;
;''; <
return(( 
Result(( 
.(( 
Ok((  
(((  !
_mapper((! (
.((( )
Map(() ,
<((, -

PartnerDTO((- 7
>((7 8
(((8 9

newPartner((9 C
)((C D
)((D E
;((E F
})) 
catch** 
(** 
	Exception** 
ex** 
)** 
{++ 
_logger,, 
.,, 
LogError,,  
(,,  !
request,,! (
,,,( )
ex,,* ,
.,,, -
Message,,- 4
),,4 5
;,,5 6
return-- 
Result-- 
.-- 
Fail-- "
(--" #
ex--# %
.--% &
Message--& -
)--- .
;--. /
}.. 
}// 	
}00 
}11 §,
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\Update\UpdateNewsHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Newss

! &
.

& '
Update

' -
{ 
public 

class 
UpdateNewsHandler "
:# $
IRequestHandler% 4
<4 5
UpdateNewsCommand5 F
,F G
ResultH N
<N O
NewsDTOO V
>V W
>W X
{ 
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IBlobService %
_blobSevice& 1
;1 2
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
UpdateNewsHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
IMapperG N
mapperO U
,U V
IBlobServiceW c
blobServiced o
,o p
ILoggerServiceq 
logger
Ä Ü
)
Ü á
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_blobSevice 
= 
blobService %
;% &
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
NewsDTO! (
>( )
>) *
Handle+ 1
(1 2
UpdateNewsCommand2 C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
var 
news 
= 
_mapper 
. 
Map "
<" #
News# '
>' (
(( )
request) 0
.0 1
news1 5
)5 6
;6 7
if 
( 
news 
is 
null 
) 
{ 
const 
string 
errorMsg %
=& '
$"( *
$str* E
"E F
;F G
_logger   
.   
LogError    
(    !
request  ! (
,  ( )
errorMsg  * 2
)  2 3
;  3 4
return!! 
Result!! 
.!! 
Fail!! "
(!!" #
new!!# &
Error!!' ,
(!!, -
errorMsg!!- 5
)!!5 6
)!!6 7
;!!7 8
}"" 
var$$ 
response$$ 
=$$ 
_mapper$$ "
.$$" #
Map$$# &
<$$& '
NewsDTO$$' .
>$$. /
($$/ 0
news$$0 4
)$$4 5
;$$5 6
if&& 
(&& 
news&& 
.&& 
Image&& 
is&& 
not&& !
null&&" &
)&&& '
{'' 
response(( 
.(( 
Image(( 
.(( 
Base64(( %
=((& '
_blobSevice((( 3
.((3 4%
FindFileInStorageAsBase64((4 M
(((M N
response((N V
.((V W
Image((W \
.((\ ]
BlobName((] e
)((e f
;((f g
})) 
else** 
{++ 
var,, 
img,, 
=,, 
await,, 
_repositoryWrapper,,  2
.,,2 3
ImageRepository,,3 B
.,,B C"
GetFirstOrDefaultAsync,,C Y
(,,Y Z
x,,Z [
=>,,\ ^
x,,_ `
.,,` a
Id,,a c
==,,d f
response,,g o
.,,o p
ImageId,,p w
),,w x
;,,x y
if-- 
(-- 
img-- 
!=-- 
null-- 
)--  
{.. 
_repositoryWrapper// &
.//& '
ImageRepository//' 6
.//6 7
Delete//7 =
(//= >
img//> A
)//A B
;//B C
}00 
}11 
_repositoryWrapper33 
.33 
NewsRepository33 -
.33- .
Update33. 4
(334 5
news335 9
)339 :
;33: ;
var44 
resultIsSuccess44 
=44  !
await44" '
_repositoryWrapper44( :
.44: ;
SaveChangesAsync44; K
(44K L
)44L M
>44N O
$num44P Q
;44Q R
if66 
(66 
resultIsSuccess66 
)66 
{77 
return88 
Result88 
.88 
Ok88  
(88  !
response88! )
)88) *
;88* +
}99 
else:: 
{;; 
const<< 
string<< 
errorMsg<< %
=<<& '
$"<<( *
$str<<* ?
"<<? @
;<<@ A
_logger== 
.== 
LogError==  
(==  !
request==! (
,==( )
errorMsg==* 2
)==2 3
;==3 4
return>> 
Result>> 
.>> 
Fail>> "
(>>" #
new>># &
Error>>' ,
(>>, -
errorMsg>>- 5
)>>5 6
)>>6 7
;>>7 8
}?? 
}@@ 	
}AA 
}BB ˙
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\Update\UpdateNewsCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
Update' -
{ 
public 

record 
UpdateNewsCommand #
(# $
NewsDTO$ +
news, 0
)0 1
:2 3
IRequest4 <
<< =
Result= C
<C D
NewsDTOD K
>K L
>L M
;M N
}		 ü
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\SortedByDateTime\SortedByDateTimeQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
SortedByDateTime' 7
{ 
public 

record !
SortedByDateTimeQuery '
(' (
)( )
:* +
IRequest, 4
<4 5
Result5 ;
<; <
List< @
<@ A
NewsDTOA H
>H I
>I J
>J K
;K L
} ∆#
íD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\SortedByDateTime\SortedByDateTimeHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
SortedByDateTime' 7
{ 
public 

class #
SortedByDateTimeHandler (
:) *
IRequestHandler+ :
<: ;!
SortedByDateTimeQuery; P
,P Q
ResultR X
<X Y
ListY ]
<] ^
NewsDTO^ e
>e f
>f g
>g h
{ 
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public #
SortedByDateTimeHandler &
(& '
IRepositoryWrapper' 9
repositoryWrapper: K
,K L
IMapperM T
mapperU [
,[ \
IBlobService] i
blobServicej u
,u v
ILoggerService	w Ö
logger
Ü å
)
å ç
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
List! %
<% &
NewsDTO& -
>- .
>. /
>/ 0
Handle1 7
(7 8!
SortedByDateTimeQuery8 M
requestN U
,U V
CancellationTokenW h
cancellationTokeni z
)z {
{ 	
var 
news 
= 
await 
_repositoryWrapper /
./ 0
NewsRepository0 >
.> ?
GetAllAsync? J
(J K
include 
: 
cat 
=> 
cat  #
.# $
Include$ +
(+ ,
img, /
=>0 2
img3 6
.6 7
Image7 <
)< =
)= >
;> ?
if   
(   
news   
==   
null   
)   
{!! 
const"" 
string"" 
errorMsg"" %
=""& '
$str""( K
;""K L
_logger## 
.## 
LogError##  
(##  !
request##! (
,##( )
errorMsg##* 2
)##2 3
;##3 4
return$$ 
Result$$ 
.$$ 
Fail$$ "
($$" #
errorMsg$$# +
)$$+ ,
;$$, -
}%% 
var'' 
newsDTOs'' 
='' 
_mapper'' "
.''" #
Map''# &
<''& '
IEnumerable''' 2
<''2 3
NewsDTO''3 :
>'': ;
>''; <
(''< =
news''= A
)''A B
.''B C
OrderByDescending''C T
(''T U
x''U V
=>''W Y
x''Z [
.''[ \
CreationDate''\ h
)''h i
.''i j
ToList''j p
(''p q
)''q r
;''r s
foreach)) 
()) 
var)) 
dto)) 
in)) 
newsDTOs))  (
)))( )
{** 
if++ 
(++ 
dto++ 
.++ 
Image++ 
is++  
not++! $
null++% )
)++) *
{,, 
dto-- 
.-- 
Image-- 
.-- 
Base64-- $
=--% &
_blobService--' 3
.--3 4%
FindFileInStorageAsBase64--4 M
(--M N
dto--N Q
.--Q R
Image--R W
.--W X
BlobName--X `
)--` a
;--a b
}.. 
}// 
return11 
Result11 
.11 
Ok11 
(11 
newsDTOs11 %
)11% &
;11& '
}22 	
}33 
}44 ¨
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetNewsAndLinksByUrl\GetNewsAndLinksByUrlQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& ' 
GetNewsAndLinksByUrl' ;
{ 
public 

record %
GetNewsAndLinksByUrlQuery +
(+ ,
string, 2
url3 6
)6 7
:8 9
IRequest: B
<B C
ResultC I
<I J
NewsDTOWithURLsJ Y
>Y Z
>Z [
;[ \
} ¸H
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetNewsAndLinksByUrl\GetNewsAndLinksByUrlHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& ' 
GetNewsAndLinksByUrl' ;
{ 
public 

class '
GetNewsAndLinksByUrlHandler ,
:- .
IRequestHandler/ >
<> ?%
GetNewsAndLinksByUrlQuery? X
,X Y
ResultZ `
<` a
NewsDTOWithURLsa p
>p q
>q r
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public '
GetNewsAndLinksByUrlHandler *
(* +
IMapper+ 2
mapper3 9
,9 :
IRepositoryWrapper; M
repositoryWrapperN _
,_ `
IBlobServicea m
blobServicen y
,y z
ILoggerService	{ â
logger
ä ê
)
ê ë
{ 	
_mapper 
= 
mapper 
; 
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
NewsDTOWithURLs! 0
>0 1
>1 2
Handle3 9
(9 :%
GetNewsAndLinksByUrlQuery: S
requestT [
,[ \
CancellationToken] n
cancellationToken	o Ä
)
Ä Å
{ 	
string 
url 
= 
request  
.  !
url! $
;$ %
var 
newsDTO 
= 
_mapper !
.! "
Map" %
<% &
NewsDTO& -
>- .
(. /
await/ 4
_repositoryWrapper5 G
.G H
NewsRepositoryH V
.V W"
GetFirstOrDefaultAsyncW m
(m n
	predicate 
: 
sc 
=>  
sc! #
.# $
URL$ '
==( *
url+ .
,. /
include   
:   
scl   
=>   
scl    #
.!! 
Include!! 
(!! 
sc!! 
=>!!  "
sc!!# %
.!!% &
Image!!& +
)!!+ ,
)!!, -
)!!- .
;!!. /
if## 
(## 
newsDTO## 
is## 
null## 
)##  
{$$ 
string%% 
errorMsg%% 
=%%  !
$"%%" $
$str%%$ =
{%%= >
url%%> A
}%%A B
"%%B C
;%%C D
_logger&& 
.&& 
LogError&&  
(&&  !
request&&! (
,&&( )
errorMsg&&* 2
)&&2 3
;&&3 4
return'' 
Result'' 
.'' 
Fail'' "
(''" #
errorMsg''# +
)''+ ,
;'', -
}(( 
if** 
(** 
newsDTO** 
.** 
Image** 
is**  
not**! $
null**% )
)**) *
{++ 
newsDTO,, 
.,, 
Image,, 
.,, 
Base64,, $
=,,% &
_blobService,,' 3
.,,3 4%
FindFileInStorageAsBase64,,4 M
(,,M N
newsDTO,,N U
.,,U V
Image,,V [
.,,[ \
BlobName,,\ d
),,d e
;,,e f
}-- 
var// 
news// 
=// 
(// 
await// 
_repositoryWrapper// 0
.//0 1
NewsRepository//1 ?
.//? @
GetAllAsync//@ K
(//K L
)//L M
)//M N
.//N O
ToList//O U
(//U V
)//V W
;//W X
var00 
	newsIndex00 
=00 
news00  
.00  !
	FindIndex00! *
(00* +
x00+ ,
=>00- /
x000 1
.001 2
Id002 4
==005 7
newsDTO008 ?
.00? @
Id00@ B
)00B C
;00C D
string11 
prevNewsLink11 
=11  !
null11" &
;11& '
string22 
nextNewsLink22 
=22  !
null22" &
;22& '
if44 
(44 
	newsIndex44 
!=44 
$num44 
)44 
{55 
prevNewsLink66 
=66 
news66 #
[66# $
	newsIndex66$ -
-66. /
$num660 1
]661 2
.662 3
URL663 6
;666 7
}77 
if99 
(99 
	newsIndex99 
!=99 
news99  
.99  !
Count99! &
-99' (
$num99) *
)99* +
{:: 
nextNewsLink;; 
=;; 
news;; #
[;;# $
	newsIndex;;$ -
+;;. /
$num;;0 1
];;1 2
.;;2 3
URL;;3 6
;;;6 7
}<< 
var>> "
randomNewsTitleAndLink>> &
=>>' (
new>>) ,
RandomNewsDTO>>- :
(>>: ;
)>>; <
;>>< =
var@@ 
arrCount@@ 
=@@ 
news@@ 
.@@  
Count@@  %
;@@% &
ifAA 
(AA 
arrCountAA 
>AA 
$numAA 
)AA 
{BB 
ifCC 
(CC 
	newsIndexCC 
+CC 
$numCC  !
==CC" $
arrCountCC% -
-CC. /
$numCC0 1
||CC2 4
	newsIndexCC5 >
==CC? A
arrCountCCB J
-CCK L
$numCCM N
)CCN O
{DD "
randomNewsTitleAndLinkEE *
.EE* +
RandomNewsUrlEE+ 8
=EE9 :
newsEE; ?
[EE? @
	newsIndexEE@ I
-EEJ K
$numEEL M
]EEM N
.EEN O
URLEEO R
;EER S"
randomNewsTitleAndLinkFF *
.FF* +
TitleFF+ 0
=FF1 2
newsFF3 7
[FF7 8
	newsIndexFF8 A
-FFB C
$numFFD E
]FFE F
.FFF G
TitleFFG L
;FFL M
}GG 
elseHH 
{II "
randomNewsTitleAndLinkJJ *
.JJ* +
RandomNewsUrlJJ+ 8
=JJ9 :
newsJJ; ?
[JJ? @
arrCountJJ@ H
-JJI J
$numJJK L
]JJL M
.JJM N
URLJJN Q
;JJQ R"
randomNewsTitleAndLinkKK *
.KK* +
TitleKK+ 0
=KK1 2
newsKK3 7
[KK7 8
arrCountKK8 @
-KKA B
$numKKC D
]KKD E
.KKE F
TitleKKF K
;KKK L
}LL 
}MM 
elseNN 
{OO "
randomNewsTitleAndLinkPP &
.PP& '
RandomNewsUrlPP' 4
=PP5 6
newsPP7 ;
[PP; <
	newsIndexPP< E
]PPE F
.PPF G
URLPPG J
;PPJ K"
randomNewsTitleAndLinkQQ &
.QQ& '
TitleQQ' ,
=QQ- .
newsQQ/ 3
[QQ3 4
	newsIndexQQ4 =
]QQ= >
.QQ> ?
TitleQQ? D
;QQD E
}RR 
varTT 
newsDTOWithUrlsTT 
=TT  !
newTT" %
NewsDTOWithURLsTT& 5
(TT5 6
)TT6 7
;TT7 8
newsDTOWithUrlsUU 
.UU 

RandomNewsUU &
=UU' ("
randomNewsTitleAndLinkUU) ?
;UU? @
newsDTOWithUrlsVV 
.VV 
NewsVV  
=VV! "
newsDTOVV# *
;VV* +
newsDTOWithUrlsWW 
.WW 
NextNewsUrlWW '
=WW( )
nextNewsLinkWW* 6
;WW6 7
newsDTOWithUrlsXX 
.XX 
PrevNewsUrlXX '
=XX( )
prevNewsLinkXX* 6
;XX6 7
ifZZ 
(ZZ 
newsDTOWithUrlsZZ 
isZZ  "
nullZZ# '
)ZZ' (
{[[ 
string\\ 
errorMsg\\ 
=\\  !
$"\\" $
$str\\$ =
{\\= >
url\\> A
}\\A B
"\\B C
;\\C D
_logger]] 
.]] 
LogError]]  
(]]  !
request]]! (
,]]( )
errorMsg]]* 2
)]]2 3
;]]3 4
return^^ 
Result^^ 
.^^ 
Fail^^ "
(^^" #
errorMsg^^# +
)^^+ ,
;^^, -
}__ 
returnaa 
Resultaa 
.aa 
Okaa 
(aa 
newsDTOWithUrlsaa ,
)aa, -
;aa- .
}bb 	
}cc 
}dd ¸
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetByUrl\GetNewsByUrlQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
GetByUrl' /
{ 
public 

record 
GetNewsByUrlQuery #
(# $
string$ *
url+ .
). /
:0 1
IRequest2 :
<: ;
Result; A
<A B
NewsDTOB I
>I J
>J K
;K L
} ™!
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetByUrl\GetNewsByUrlHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
GetByUrl' /
{ 
public 

class 
GetNewsByUrlHandler $
:% &
IRequestHandler' 6
<6 7
GetNewsByUrlQuery7 H
,H I
ResultJ P
<P Q
NewsDTOQ X
>X Y
>Y Z
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
GetNewsByUrlHandler "
(" #
IMapper# *
mapper+ 1
,1 2
IRepositoryWrapper3 E
repositoryWrapperF W
,W X
IBlobServiceY e
blobServicef q
,q r
ILoggerService	s Å
logger
Ç à
)
à â
{ 	
_mapper 
= 
mapper 
; 
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
NewsDTO! (
>( )
>) *
Handle+ 1
(1 2
GetNewsByUrlQuery2 C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
string 
url 
= 
request  
.  !
url! $
;$ %
var 
newsDTO 
= 
_mapper !
.! "
Map" %
<% &
NewsDTO& -
>- .
(. /
await/ 4
_repositoryWrapper5 G
.G H
NewsRepositoryH V
.V W"
GetFirstOrDefaultAsyncW m
(m n
	predicate 
: 
sc 
=>  
sc! #
.# $
URL$ '
==( *
url+ .
,. /
include   
:   
scl   
=>   
scl    #
.!! 
Include!! 
(!! 
sc!! 
=>!!  "
sc!!# %
.!!% &
Image!!& +
)!!+ ,
)!!, -
)!!- .
;!!. /
if"" 
("" 
newsDTO"" 
is"" 
null"" 
)"" 
{## 
string$$ 
errorMsg$$ 
=$$  !
$"$$" $
$str$$$ =
{$$= >
url$$> A
}$$A B
"$$B C
;$$C D
_logger%% 
.%% 
LogError%%  
(%%  !
request%%! (
,%%( )
errorMsg%%* 2
)%%2 3
;%%3 4
return&& 
Result&& 
.&& 
Fail&& "
(&&" #
errorMsg&&# +
)&&+ ,
;&&, -
}'' 
if)) 
()) 
newsDTO)) 
.)) 
Image)) 
is))  
not))! $
null))% )
)))) *
{** 
newsDTO++ 
.++ 
Image++ 
.++ 
Base64++ $
=++% &
_blobService++' 3
.++3 4%
FindFileInStorageAsBase64++4 M
(++M N
newsDTO++N U
.++U V
Image++V [
.++[ \
BlobName++\ d
)++d e
;++e f
},, 
return.. 
Result.. 
... 
Ok.. 
(.. 
newsDTO.. $
)..$ %
;..% &
}// 	
}00 
}11 Ù
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetById\GetNewsByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
GetById' .
{ 
public 

record 
GetNewsByIdQuery "
(" #
int# &
id' )
)) *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =
NewsDTO= D
>D E
>E F
;F G
} õ!
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetById\GetNewsByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
GetById' .
{ 
public 

class 
GetNewsByIdHandler #
:$ %
IRequestHandler& 5
<5 6
GetNewsByIdQuery6 F
,F G
ResultH N
<N O
NewsDTOO V
>V W
>W X
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
GetNewsByIdHandler !
(! "
IMapper" )
mapper* 0
,0 1
IRepositoryWrapper2 D
repositoryWrapperE V
,V W
IBlobServiceX d
blobServicee p
,p q
ILoggerService	r Ä
logger
Å á
)
á à
{ 	
_mapper 
= 
mapper 
; 
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
NewsDTO! (
>( )
>) *
Handle+ 1
(1 2
GetNewsByIdQuery2 B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
{ 	
int 
id 
= 
request 
. 
id 
;  
var 
newsDTO 
= 
_mapper !
.! "
Map" %
<% &
NewsDTO& -
>- .
(. /
await/ 4
_repositoryWrapper5 G
.G H
NewsRepositoryH V
.V W"
GetFirstOrDefaultAsyncW m
(m n
	predicate 
: 
sc 
=>  
sc! #
.# $
Id$ &
==' )
id* ,
,, -
include   
:   
scl   
=>   
scl    #
.!! 
Include!! 
(!! 
sc!! 
=>!!  "
sc!!# %
.!!% &
Image!!& +
)!!+ ,
)!!, -
)!!- .
;!!. /
if"" 
("" 
newsDTO"" 
is"" 
null"" 
)"" 
{## 
string$$ 
errorMsg$$ 
=$$  !
$"$$" $
$str$$$ <
{$$< =
id$$= ?
}$$? @
"$$@ A
;$$A B
_logger%% 
.%% 
LogError%%  
(%%  !
request%%! (
,%%( )
errorMsg%%* 2
)%%2 3
;%%3 4
return&& 
Result&& 
.&& 
Fail&& "
(&&" #
errorMsg&&# +
)&&+ ,
;&&, -
}'' 
if)) 
()) 
newsDTO)) 
.)) 
Image)) 
is))  
not))! $
null))% )
)))) *
{** 
newsDTO++ 
.++ 
Image++ 
.++ 
Base64++ $
=++% &
_blobService++' 3
.++3 4%
FindFileInStorageAsBase64++4 M
(++M N
newsDTO++N U
.++U V
Image++V [
.++[ \
BlobName++\ d
)++d e
;++e f
},, 
return.. 
Result.. 
... 
Ok.. 
(.. 
newsDTO.. $
)..$ %
;..% &
}// 	
}00 
}11 Ü
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetAll\GetAllNewsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
GetAll' -
{ 
public 

record 
GetAllNewsQuery !
(! "
)" #
:$ %
IRequest& .
<. /
Result/ 5
<5 6
IEnumerable6 A
<A B
NewsDTOB I
>I J
>J K
>K L
;L M
} Ω!
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\GetAll\GetAllNewsHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
GetAll' -
{ 
public 

class 
GetAllNewsHandler "
:# $
IRequestHandler% 4
<4 5
GetAllNewsQuery5 D
,D E
ResultF L
<L M
IEnumerableM X
<X Y
NewsDTOY `
>` a
>a b
>b c
{ 
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
GetAllNewsHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
IMapperG N
mapperO U
,U V
IBlobServiceW c
blobServiced o
,o p
ILoggerServiceq 
logger
Ä Ü
)
Ü á
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
NewsDTO- 4
>4 5
>5 6
>6 7
Handle8 >
(> ?
GetAllNewsQuery? N
requestO V
,V W
CancellationTokenX i
cancellationTokenj {
){ |
{ 	
var 
news 
= 
await 
_repositoryWrapper /
./ 0
NewsRepository0 >
.> ?
GetAllAsync? J
(J K
include 
: 
cat 
=> 
cat  #
.# $
Include$ +
(+ ,
img, /
=>0 2
img3 6
.6 7
Image7 <
)< =
)= >
;> ?
if   
(   
news   
==   
null   
)   
{!! 
const"" 
string"" 
errorMsg"" %
=""& '
$str""( K
;""K L
_logger## 
.## 
LogError##  
(##  !
request##! (
,##( )
errorMsg##* 2
)##2 3
;##3 4
return$$ 
Result$$ 
.$$ 
Fail$$ "
($$" #
errorMsg$$# +
)$$+ ,
;$$, -
}%% 
var'' 
newsDTOs'' 
='' 
_mapper'' "
.''" #
Map''# &
<''& '
IEnumerable''' 2
<''2 3
NewsDTO''3 :
>'': ;
>''; <
(''< =
news''= A
)''A B
;''B C
foreach)) 
()) 
var)) 
dto)) 
in)) 
newsDTOs))  (
)))( )
{** 
if++ 
(++ 
dto++ 
.++ 
Image++ 
is++ 
not++  #
null++$ (
)++( )
{,, 
dto-- 
.-- 
Image-- 
.-- 
Base64-- $
=--% &
_blobService--' 3
.--3 4%
FindFileInStorageAsBase64--4 M
(--M N
dto--N Q
.--Q R
Image--R W
.--W X
BlobName--X `
)--` a
;--a b
}.. 
}// 
return11 
Result11 
.11 
Ok11 
(11 
newsDTOs11 %
)11% &
;11& '
}22 	
}33 
}44 †!
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\Delete\DeleteNewsHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
Delete' -
{		 
public

 

class

 
DeleteNewsHandler

 "
:

# $
IRequestHandler

% 4
<

4 5
DeleteNewsCommand

5 F
,

F G
Result

H N
<

N O
Unit

O S
>

S T
>

T U
{ 
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
DeleteNewsHandler  
(  !
IRepositoryWrapper! 3
repositoryWrapper4 E
,E F
ILoggerServiceG U
loggerV \
)\ ]
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. /
DeleteNewsCommand/ @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 	
int 
id 
= 
request 
. 
id 
;  
var 
news 
= 
await 
_repositoryWrapper /
./ 0
NewsRepository0 >
.> ?"
GetFirstOrDefaultAsync? U
(U V
nV W
=>X Z
n[ \
.\ ]
Id] _
==` b
idc e
)e f
;f g
if 
( 
news 
== 
null 
) 
{ 
string 
errorMsg 
=  !
$"" $
$str$ B
{B C
idC E
}E F
"F G
;G H
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return 
Result 
. 
Fail "
(" #
errorMsg# +
)+ ,
;, -
} 
if 
( 
news 
. 
Image 
is 
not !
null" &
)& '
{   
_repositoryWrapper!! "
.!!" #
ImageRepository!!# 2
.!!2 3
Delete!!3 9
(!!9 :
news!!: >
.!!> ?
Image!!? D
)!!D E
;!!E F
}"" 
_repositoryWrapper$$ 
.$$ 
NewsRepository$$ -
.$$- .
Delete$$. 4
($$4 5
news$$5 9
)$$9 :
;$$: ;
var%% 
resultIsSuccess%% 
=%%  !
await%%" '
_repositoryWrapper%%( :
.%%: ;
SaveChangesAsync%%; K
(%%K L
)%%L M
>%%N O
$num%%P Q
;%%Q R
if&& 
(&& 
resultIsSuccess&& 
)&& 
{'' 
return(( 
Result(( 
.(( 
Ok((  
(((  !
Unit((! %
.((% &
Value((& +
)((+ ,
;((, -
})) 
else** 
{++ 
string,, 
errorMsg,, 
=,,  !
$str,," 9
;,,9 :
_logger-- 
.-- 
LogError--  
(--  !
request--! (
,--( )
errorMsg--* 2
)--2 3
;--3 4
return.. 
Result.. 
... 
Fail.. "
(.." #
new..# &
Error..' ,
(.., -
errorMsg..- 5
)..5 6
)..6 7
;..7 8
}// 
}00 	
}11 
}22 Ò
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\Delete\DeleteNewsCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
Delete' -
{ 
public 

record 
DeleteNewsCommand #
(# $
int$ '
id( *
)* +
:, -
IRequest. 6
<6 7
Result7 =
<= >
Unit> B
>B C
>C D
;D E
} ˝
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\Create\CreateNewsCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Newss! &
.& '
Create' -
{ 
public 

record 
CreateNewsCommand #
(# $
NewsDTO$ +
newNews, 3
)3 4
:5 6
IRequest7 ?
<? @
Result@ F
<F G
NewsDTOG N
>N O
>O P
;P Q
} ◊!
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Newss\Create\CreateNewsHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Newss		! &
.		& '
Create		' -
{

 
public 

class 
CreateNewsHandler "
:# $
IRequestHandler% 4
<4 5
CreateNewsCommand5 F
,F G
ResultH N
<N O
NewsDTOO V
>V W
>W X
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
CreateNewsHandler  
(  !
IMapper! (
mapper) /
,/ 0
IRepositoryWrapper1 C
repositoryWrapperD U
,U V
ILoggerServiceW e
loggerf l
)l m
{ 	
_mapper 
= 
mapper 
; 
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
NewsDTO! (
>( )
>) *
Handle+ 1
(1 2
CreateNewsCommand2 C
requestD K
,K L
CancellationTokenM ^
cancellationToken_ p
)p q
{ 	
var 
newNews 
= 
_mapper !
.! "
Map" %
<% &
News& *
>* +
(+ ,
request, 3
.3 4
newNews4 ;
); <
;< =
if 
( 
newNews 
is 
null 
)  
{ 
const 
string 
errorMsg %
=& '
$str( E
;E F
_logger 
. 
LogError  
(  !
request! (
,( )
errorMsg* 2
)2 3
;3 4
return 
Result 
. 
Fail "
(" #
errorMsg# +
)+ ,
;, -
} 
if!! 
(!! 
newNews!! 
.!! 
ImageId!! 
==!!  "
$num!!# $
)!!$ %
{"" 
newNews## 
.## 
ImageId## 
=##  !
null##" &
;##& '
}$$ 
var&& 
entity&& 
=&& 
await&& 
_repositoryWrapper&& 1
.&&1 2
NewsRepository&&2 @
.&&@ A
CreateAsync&&A L
(&&L M
newNews&&M T
)&&T U
;&&U V
var'' 
resultIsSuccess'' 
=''  !
await''" '
_repositoryWrapper''( :
.'': ;
SaveChangesAsync''; K
(''K L
)''L M
>''N O
$num''P Q
;''Q R
if(( 
((( 
resultIsSuccess(( 
)(( 
{)) 
return** 
Result** 
.** 
Ok**  
(**  !
_mapper**! (
.**( )
Map**) ,
<**, -
NewsDTO**- 4
>**4 5
(**5 6
entity**6 <
)**< =
)**= >
;**> ?
}++ 
else,, 
{-- 
const.. 
string.. 
errorMsg.. %
=..& '
$str..( A
;..A B
_logger// 
.// 
LogError//  
(//  !
request//! (
,//( )
errorMsg//* 2
)//2 3
;//3 4
return00 
Result00 
.00 
Fail00 "
(00" #
new00# &
Error00' ,
(00, -
errorMsg00- 5
)005 6
)006 7
;007 8
}11 
}22 	
}33 
}44 ƒ
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Video\GetByStreetcodeId\GetVideoByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Video' ,
., -
GetByStreetcodeId- >
;> ?
public 
record '
GetVideoByStreetcodeIdQuery )
() *
int* -
StreetcodeId. :
): ;
:< =
IRequest> F
<F G
ResultG M
<M N
VideoDTON V
>V W
>W X
;X Yë!
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Video\GetByStreetcodeId\GetVideoByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Video' ,
., -
GetByStreetcodeId- >
;> ?
public 
class )
GetVideoByStreetcodeIdHandler *
:+ ,
IRequestHandler- <
<< ='
GetVideoByStreetcodeIdQuery= X
,X Y
ResultZ `
<` a
VideoDTOa i
>i j
>j k
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
)
GetVideoByStreetcodeIdHandler (
(( )
IRepositoryWrapper) ;
repositoryWrapper< M
,M N
IMapperO V
mapperW ]
,] ^
ILoggerService_ m
loggern t
)t u
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
VideoDTO %
>% &
>& '
Handle( .
(. /'
GetVideoByStreetcodeIdQuery/ J
requestK R
,R S
CancellationTokenT e
cancellationTokenf w
)w x
{ 
var 
video 
= 
await 
_repositoryWrapper ,
., -
VideoRepository- <
. "
GetFirstOrDefaultAsync #
(# $
video$ )
=>* ,
video- 2
.2 3
StreetcodeId3 ?
==@ B
requestC J
.J K
StreetcodeIdK W
)W X
;X Y
if 

(
 
video 
== 
null 
) 
{   	
StreetcodeContent!! 
?!! 

streetcode!! )
=!!* +
await!!, 1
_repositoryWrapper!!2 D
.!!D E 
StreetcodeRepository!!E Y
.!!Y Z"
GetFirstOrDefaultAsync!!Z p
(!!p q
x!!q r
=>!!s u
x!!v w
.!!w x
Id!!x z
==!!{ }
request	!!~ Ö
.
!!Ö Ü
StreetcodeId
!!Ü í
)
!!í ì
;
!!ì î
if"" 
("" 

streetcode"" 
is"" 
null"" "
)""" #
{## 
string$$ 
errorMsg$$ 
=$$  !
$"$$" $
$str$$$ 8
{$$8 9
request$$9 @
.$$@ A
StreetcodeId$$A M
}$$M N
$str$$N \
"$$\ ]
;$$] ^
_logger%% 
.%% 
LogError%%  
(%%  !
request%%! (
,%%( )
errorMsg%%* 2
)%%2 3
;%%3 4
return&& 
Result&& 
.&& 
Fail&& "
(&&" #
new&&# &
Error&&' ,
(&&, -
errorMsg&&- 5
)&&5 6
)&&6 7
;&&7 8
}'' 
}(( 	

NullResult** 
<** 
VideoDTO** 
>** 
result** #
=**$ %
new**& )

NullResult*** 4
<**4 5
VideoDTO**5 =
>**= >
(**> ?
)**? @
;**@ A
result++ 
.++ 
	WithValue++ 
(++ 
_mapper++  
.++  !
Map++! $
<++$ %
VideoDTO++% -
>++- .
(++. /
video++/ 4
)++4 5
)++5 6
;++6 7
return,, 
result,, 
;,, 
}-- 
}.. í
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Video\GetById\GetVideoByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Video' ,
., -
GetById- 4
;4 5
public 
record 
GetVideoByIdQuery 
(  
int  #
Id$ &
)& '
:( )
IRequest* 2
<2 3
Result3 9
<9 :
VideoDTO: B
>B C
>C D
;D E≠
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Video\GetById\GetVideoByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Video' ,
., -
GetById- 4
;4 5
public

 
class

 
GetVideoByIdHandler

  
:

! "
IRequestHandler

# 2
<

2 3
GetVideoByIdQuery

3 D
,

D E
Result

F L
<

L M
VideoDTO

M U
>

U V
>

V W
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetVideoByIdHandler 
( 
IRepositoryWrapper 1
repositoryWrapper2 C
,C D
IMapperE L
mapperM S
,S T
ILoggerServiceU c
loggerd j
)j k
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
VideoDTO %
>% &
>& '
Handle( .
(. /
GetVideoByIdQuery/ @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 
var 
video 
= 
await 
_repositoryWrapper ,
., -
VideoRepository- <
.< ="
GetFirstOrDefaultAsync= S
(S T
fT U
=>V X
fY Z
.Z [
Id[ ]
==^ `
requesta h
.h i
Idi k
)k l
;l m
if 

( 
video 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  K
{K L
requestL S
.S T
IdT V
}V W
"W X
;X Y
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
}   	
return"" 
Result"" 
."" 
Ok"" 
("" 
_mapper""  
.""  !
Map""! $
<""$ %
VideoDTO""% -
>""- .
("". /
video""/ 4
)""4 5
)""5 6
;""6 7
}## 
}$$ à
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Video\GetAll\GetAllVideosQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Video' ,
., -
GetAll- 3
;3 4
public 
record 
GetAllVideosQuery 
:  !
IRequest" *
<* +
Result+ 1
<1 2
IEnumerable2 =
<= >
VideoDTO> F
>F G
>G H
>H I
;I JŸ
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Video\GetAll\GetAllVideosHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Video

' ,
.

, -
GetAll

- 3
;

3 4
public 
class 
GetAllVideosHandler  
:! "
IRequestHandler# 2
<2 3
GetAllVideosQuery3 D
,D E
ResultF L
<L M
IEnumerableM X
<X Y
VideoDTOY a
>a b
>b c
>c d
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAllVideosHandler 
( 
IRepositoryWrapper 1
repositoryWrapper2 C
,C D
IMapperE L
mapperM S
,S T
ILoggerServiceU c
loggerd j
)j k
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
VideoDTO) 1
>1 2
>2 3
>3 4
Handle5 ;
(; <
GetAllVideosQuery< M
requestN U
,U V
CancellationTokenW h
cancellationTokeni z
)z {
{ 
var 
videos 
= 
await 
_repositoryWrapper -
.- .
VideoRepository. =
.= >
GetAllAsync> I
(I J
)J K
;K L
if 

( 
videos 
is 
null 
) 
{ 	
const 
string 
errorMsg !
=" #
$str$ <
;< =
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$  
.$$  !
Map$$! $
<$$$ %
IEnumerable$$% 0
<$$0 1
VideoDTO$$1 9
>$$9 :
>$$: ;
($$; <
videos$$< B
)$$B C
)$$C D
;$$D E
}%% 
}&& ∞
≠D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\StreetcodeArt\GetByStreetcodeId\GetStreetcodeArtByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
StreetcodeArt' 4
.4 5
GetByStreetcodeId5 F
{ 
public 
record	 /
#GetStreetcodeArtByStreetcodeIdQuery 3
(3 4
int4 7
StreetcodeId8 D
)D E
:F G
IRequestH P
<P Q
ResultQ W
<W X
IEnumerableX c
<c d
StreetcodeArtDTOd t
>t u
>u v
>v w
;w x
} Î&
ØD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\StreetcodeArt\GetByStreetcodeId\GetStreetcodeArtByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
StreetcodeArt' 4
.4 5
GetByStreetcodeId5 F
{ 
public 
class	 1
%GetStreetcodeArtByStreetcodeIdHandler 4
:5 6
IRequestHandler7 F
<F G/
#GetStreetcodeArtByStreetcodeIdQueryG j
,j k
Resultl r
<r s
IEnumerables ~
<~ 
StreetcodeArtDTO	 è
>
è ê
>
ê ë
>
ë í
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 1
%GetStreetcodeArtByStreetcodeIdHandler 4
(4 5
IRepositoryWrapper 
repositoryWrapper 0
,0 1
IMapper 
mapper 
, 
IBlobService 
blobService $
,$ %
ILoggerService 
logger !
)! "
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public   
async   
Task   
<   
Result    
<    !
IEnumerable  ! ,
<  , -
StreetcodeArtDTO  - =
>  = >
>  > ?
>  ? @
Handle  A G
(  G H/
#GetStreetcodeArtByStreetcodeIdQuery  H k
request  l s
,  s t
CancellationToken	  u Ü
cancellationToken
  á ò
)
  ò ô
{!! 	
var** 
art** 
=** 
await** 
_repositoryWrapper** .
.++ #
StreetcodeArtRepository++ $
.,, 
GetAllAsync,, 
(,, 
	predicate-- 
:-- 
s-- 
=>-- 
s--  !
.--! "
StreetcodeId--" .
==--/ 1
request--2 9
.--9 :
StreetcodeId--: F
,--F G
include.. 
:.. 
art.. 
=>.. 
art..  #
.// 
Include// 
(// 
a// 
=>// !
a//" #
.//# $
Art//$ '
)//' (
.00 
Include00 
(00 
i00 
=>00 !
i00" #
.00# $
Art00$ '
.00' (
Image00( -
)00- .
!00/ 0
)000 1
;001 2
if22 
(22 
art22 
is22 
null22 
)22 
{33 
string44 
errorMsg44 
=44  !
$"44" $
$str44$ Y
{44Y Z
request44Z a
.44a b
StreetcodeId44b n
}44n o
"44o p
;44p q
_logger55 
.55 
LogError55  
(55  !
request55! (
,55( )
errorMsg55* 2
)552 3
;553 4
return66 
Result66 
.66 
Fail66 "
(66" #
new66# &
Error66' ,
(66, -
errorMsg66- 5
)665 6
)666 7
;667 8
}77 
var99 
artsDto99 
=99 
_mapper99 !
.99! "
Map99" %
<99% &
IEnumerable99& 1
<991 2
StreetcodeArtDTO992 B
>99B C
>99C D
(99D E
art99E H
)99H I
;99I J
foreach;; 
(;; 
var;; 
artDto;; 
in;;  "
artsDto;;# *
);;* +
{<< 
artDto== 
.== 
Art== 
.== 
Image==  
.==  !
Base64==! '
===( )
_blobService==* 6
.==6 7%
FindFileInStorageAsBase64==7 P
(==P Q
artDto==Q W
.==W X
Art==X [
.==[ \
Image==\ a
.==a b
BlobName==b j
)==j k
;==k l
}>> 
return@@ 
Result@@ 
.@@ 
Ok@@ 
(@@ 
artsDto@@ $
)@@$ %
;@@% &
}AA 	
}BB 
}CC ˚
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetByStreetcodeId\GetImageByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Image' ,
., -
GetByStreetcodeId- >
;> ?
public 
record '
GetImageByStreetcodeIdQuery )
() *
int* -
StreetcodeId. :
): ;
:< =
IRequest> F
<F G
ResultG M
<M N
IEnumerableN Y
<Y Z
ImageDTOZ b
>b c
>c d
>d e
;e f¢'
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetByStreetcodeId\GetImageByStreetcodeIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Image

' ,
.

, -
GetByStreetcodeId

- >
;

> ?
public 
class )
GetImageByStreetcodeIdHandler *
:+ ,
IRequestHandler- <
<< ='
GetImageByStreetcodeIdQuery= X
,X Y
ResultZ `
<` a
IEnumerablea l
<l m
ImageDTOm u
>u v
>v w
>w x
{ 
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
)
GetImageByStreetcodeIdHandler (
(( )
IRepositoryWrapper) ;
repositoryWrapper< M
,M N
IMapperO V
mapperW ]
,] ^
IBlobService_ k
blobServicel w
,w x
ILoggerService	y á
logger
à é
)
é è
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
ImageDTO) 1
>1 2
>2 3
>3 4
Handle5 ;
(; <'
GetImageByStreetcodeIdQuery< W
requestX _
,_ `
CancellationTokena r
cancellationToken	s Ñ
)
Ñ Ö
{ 
var 
images 
= 
( 
await 
_repositoryWrapper .
.. /
ImageRepository/ >
. 
GetAllAsync 
( 
f 
=> 
f 
. 
Streetcodes 
. 
Any "
(" #
s# $
=>% '
s( )
.) *
Id* ,
==- /
request0 7
.7 8
StreetcodeId8 D
)D E
,E F
include   
:   
q   
=>   
q   
.   
Include   #
(  # $
img  $ '
=>  ( *
img  + .
.  . /
ImageDetails  / ;
)  ; <
)  < =
)  = >
.  > ?
OrderBy  ? F
(  F G
img  G J
=>  K M
img  N Q
.  Q R
ImageDetails  R ^
?  ^ _
.  _ `
Alt  ` c
)  c d
;  d e
if"" 

("" 
images"" 
is"" 
null"" 
||"" 
images"" $
.""$ %
Count""% *
(""* +
)""+ ,
==""- /
$num""0 1
)""1 2
{## 	
string$$ 
errorMsg$$ 
=$$ 
$"$$  
$str$$  [
{$$[ \
request$$\ c
.$$c d
StreetcodeId$$d p
}$$p q
"$$q r
;$$r s
_logger%% 
.%% 
LogError%% 
(%% 
request%% $
,%%$ %
errorMsg%%& .
)%%. /
;%%/ 0
return&& 
Result&& 
.&& 
Fail&& 
(&& 
new&& "
Error&&# (
(&&( )
errorMsg&&) 1
)&&1 2
)&&2 3
;&&3 4
}'' 	
var)) 
	imageDtos)) 
=)) 
_mapper)) 
.))  
Map))  #
<))# $
IEnumerable))$ /
<))/ 0
ImageDTO))0 8
>))8 9
>))9 :
()): ;
images)); A
)))A B
;))B C
foreach++ 
(++ 
var++ 
image++ 
in++ 
	imageDtos++ '
)++' (
{,, 	
image-- 
.-- 
Base64-- 
=-- 
_blobService-- '
.--' (%
FindFileInStorageAsBase64--( A
(--A B
image--B G
.--G H
BlobName--H P
)--P Q
;--Q R
}.. 	
return00 
Result00 
.00 
Ok00 
(00 
	imageDtos00 "
)00" #
;00# $
}11 
}22 í
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetById\GetImageByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Image' ,
., -
GetById- 4
;4 5
public 
record 
GetImageByIdQuery 
(  
int  #
Id$ &
)& '
:( )
IRequest* 2
<2 3
Result3 9
<9 :
ImageDTO: B
>B C
>C D
;D Eª!
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetById\GetImageByIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Image

' ,
.

, -
GetById

- 4
;

4 5
public 
class 
GetImageByIdHandler  
:! "
IRequestHandler# 2
<2 3
GetImageByIdQuery3 D
,D E
ResultF L
<L M
ImageDTOM U
>U V
>V W
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetImageByIdHandler 
( 
IRepositoryWrapper 1
repositoryWrapper2 C
,C D
IMapperE L
mapperM S
,S T
IBlobServiceU a
blobServiceb m
,m n
ILoggerServiceo }
logger	~ Ñ
)
Ñ Ö
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
ImageDTO %
>% &
>& '
Handle( .
(. /
GetImageByIdQuery/ @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 
var 
image 
= 
await 
_repositoryWrapper ,
., -
ImageRepository- <
.< ="
GetFirstOrDefaultAsync= S
(S T
f 
=> 
f 
. 
Id 
== 
request  
.  !
Id! #
,# $
include 
: 
q 
=> 
q 
. 
Include #
(# $
i$ %
=>& (
i) *
.* +
ImageDetails+ 7
)7 8
!9 :
): ;
;; <
if!! 

(!! 
image!! 
is!! 
null!! 
)!! 
{"" 	
string## 
errorMsg## 
=## 
$"##  
$str##  K
{##K L
request##L S
.##S T
Id##T V
}##V W
"##W X
;##X Y
_logger$$ 
.$$ 
LogError$$ 
($$ 
request$$ $
,$$$ %
errorMsg$$& .
)$$. /
;$$/ 0
return%% 
Result%% 
.%% 
Fail%% 
(%% 
new%% "
Error%%# (
(%%( )
errorMsg%%) 1
)%%1 2
)%%2 3
;%%3 4
}&& 	
var(( 
imageDto(( 
=(( 
_mapper(( 
.(( 
Map(( "
<((" #
ImageDTO((# +
>((+ ,
(((, -
image((- 2
)((2 3
;((3 4
if)) 

())
 
imageDto)) 
.)) 
BlobName)) 
!=)) 
null))  $
)))$ %
{** 	
imageDto++ 
.++ 
Base64++ 
=++ 
_blobService++ *
.++* +%
FindFileInStorageAsBase64+++ D
(++D E
image++E J
.++J K
BlobName++K S
)++S T
;++T U
},, 	
return.. 
Result.. 
... 
Ok.. 
(.. 
imageDto.. !
)..! "
;.." #
}// 
}00 †
éD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetBaseImage\GetBaseImageQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Image' ,
., -
GetBaseImage- 9
;9 :
public 
record 
GetBaseImageQuery 
(  
int  #
Id$ &
)& '
:( )
IRequest* 2
<2 3
Result3 9
<9 :
MemoryStream: F
>F G
>G H
;H Iû
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetBaseImage\GetBaseImageHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Image' ,
., -
GetBaseImage- 9
;9 :
public		 
class		 
GetBaseImageHandler		  
:		! "
IRequestHandler		# 2
<		2 3
GetBaseImageQuery		3 D
,		D E
Result		F L
<		L M
MemoryStream		M Y
>		Y Z
>		Z [
{

 
private 
readonly 
IBlobService !
_blobStorage" .
;. /
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetBaseImageHandler 
( 
IBlobService +
blobService, 7
,7 8
IRepositoryWrapper9 K
repositoryWrapperL ]
,] ^
ILoggerService_ m
loggern t
)t u
{ 
_blobStorage 
= 
blobService "
;" #
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
MemoryStream )
>) *
>* +
Handle, 2
(2 3
GetBaseImageQuery3 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
image 
= 
await 
_repositoryWrapper ,
., -
ImageRepository- <
.< ="
GetFirstOrDefaultAsync= S
(S T
aT U
=>V X
aY Z
.Z [
Id[ ]
==^ `
requesta h
.h i
Idi k
)k l
;l m
if 

( 
image 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  L
{L M
requestM T
.T U
IdU W
}W X
"X Y
;Y Z
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
} 	
return!! 
_blobStorage!! 
.!! +
FindFileInStorageAsMemoryStream!! ;
(!!; <
image!!< A
.!!A B
BlobName!!B J
)!!J K
;!!K L
}"" 
}## à
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetAll\GetAllImagesQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Image' ,
., -
GetAll- 3
;3 4
public 
record 
GetAllImagesQuery 
:  !
IRequest" *
<* +
Result+ 1
<1 2
IEnumerable2 =
<= >
ImageDTO> F
>F G
>G H
>H I
;I JÔ
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\GetAll\GetAllImagesHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Image

' ,
.

, -
GetAll

- 3
;

3 4
public 
class 
GetAllImagesHandler  
:! "
IRequestHandler# 2
<2 3
GetAllImagesQuery3 D
,D E
ResultF L
<L M
IEnumerableM X
<X Y
ImageDTOY a
>a b
>b c
>c d
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAllImagesHandler 
( 
IRepositoryWrapper 1
repositoryWrapper2 C
,C D
IMapperE L
mapperM S
,S T
IBlobServiceU a
blobServiceb m
,m n
ILoggerServiceo }
logger	~ Ñ
)
Ñ Ö
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
ImageDTO) 1
>1 2
>2 3
>3 4
Handle5 ;
(; <
GetAllImagesQuery< M
requestN U
,U V
CancellationTokenW h
cancellationTokeni z
)z {
{ 
var 
images 
= 
await 
_repositoryWrapper -
.- .
ImageRepository. =
.= >
GetAllAsync> I
(I J
)J K
;K L
if 

( 
images 
is 
null 
) 
{   	
const!! 
string!! 
errorMsg!! !
=!!" #
$"!!$ &
$str!!& ;
"!!; <
;!!< =
_logger"" 
."" 
LogError"" 
("" 
request"" $
,""$ %
errorMsg""& .
)"". /
;""/ 0
return## 
Result## 
.## 
Fail## 
(## 
new## "
Error### (
(##( )
errorMsg##) 1
)##1 2
)##2 3
;##3 4
}$$ 	
var&& 
	imageDtos&& 
=&& 
_mapper&& 
.&&  
Map&&  #
<&&# $
IEnumerable&&$ /
<&&/ 0
ImageDTO&&0 8
>&&8 9
>&&9 :
(&&: ;
images&&; A
)&&A B
;&&B C
foreach(( 
((( 
var(( 
image(( 
in(( 
	imageDtos(( '
)((' (
{)) 	
image** 
.** 
Base64** 
=** 
_blobService** '
.**' (%
FindFileInStorageAsBase64**( A
(**A B
image**B G
.**G H
BlobName**H P
)**P Q
;**Q R
}++ 	
return-- 
Result-- 
.-- 
Ok-- 
(-- 
	imageDtos-- "
)--" #
;--# $
}.. 
}// ø%
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\Delete\DeleteImageHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Media		! &
.		& '
Image		' ,
.		, -
Delete		- 3
;		3 4
public 
class 
DeleteImageHandler 
:  !
IRequestHandler" 1
<1 2
DeleteImageCommand2 D
,D E
ResultF L
<L M
UnitM Q
>Q R
>R S
{ 
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

DeleteImageHandler 
( 
IRepositoryWrapper 0
repositoryWrapper1 B
,B C
IBlobServiceD P
blobServiceQ \
,\ ]
ILoggerService^ l
loggerm s
)s t
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +
DeleteImageCommand+ =
request> E
,E F
CancellationTokenG X
cancellationTokenY j
)j k
{ 
var 
image 
= 
await 
_repositoryWrapper ,
., -
ImageRepository- <
. "
GetFirstOrDefaultAsync #
(# $
	predicate 
: 
i 
=> 
i 
. 
Id  
==! #
request$ +
.+ ,
Id, .
,. /
include 
: 
s 
=> 
s 
. 
Include #
(# $
i$ %
=>& (
i) *
.* +
Streetcodes+ 6
)6 7
)7 8
;8 9
if 

( 
image 
is 
null 
) 
{   	
string!! 
errorMsg!! 
=!! 
$"!!  
$str!!  T
{!!T U
request!!U \
.!!\ ]
Id!!] _
}!!_ `
"!!` a
;!!a b
_logger"" 
."" 
LogError"" 
("" 
request"" $
,""$ %
errorMsg""& .
)"". /
;""/ 0
return## 
Result## 
.## 
Fail## 
(## 
new## "
Error### (
(##( )
errorMsg##) 1
)##1 2
)##2 3
;##3 4
}$$ 	
_repositoryWrapper&& 
.&& 
ImageRepository&& *
.&&* +
Delete&&+ 1
(&&1 2
image&&2 7
)&&7 8
;&&8 9
var(( 
resultIsSuccess(( 
=(( 
await(( #
_repositoryWrapper(($ 6
.((6 7
SaveChangesAsync((7 G
(((G H
)((H I
>((J K
$num((L M
;((M N
if** 

(** 
resultIsSuccess** 
)** 
{++ 	
_blobService,, 
.,, 
DeleteFileInStorage,, ,
(,,, -
image,,- 2
.,,2 3
BlobName,,3 ;
),,; <
;,,< =
}-- 	
if// 

(//
 
resultIsSuccess// 
)// 
{00 	
return11 
Result11 
.11 
Ok11 
(11 
Unit11 !
.11! "
Value11" '
)11' (
;11( )
}22 	
else33 
{44 	
const55 
string55 
errorMsg55 !
=55" #
$"55$ &
$str55& ?
"55? @
;55@ A
_logger66 
.66 
LogError66 
(66 
request66 $
,66$ %
errorMsg66& .
)66. /
;66/ 0
return77 
Result77 
.77 
Fail77 
(77 
new77 "
Error77# (
(77( )
errorMsg77) 1
)771 2
)772 3
;773 4
}88 	
}99 
}:: é
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\Delete\DeleteImageCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Image' ,
., -
Delete- 3
;3 4
public 
record 
DeleteImageCommand  
(  !
int! $
Id% '
)' (
:) *
IRequest+ 3
<3 4
Result4 :
<: ;
Unit; ?
>? @
>@ A
;A B®
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\Create\CreateImageCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Image' ,
., -
Create- 3
;3 4
public 
record 
CreateImageCommand  
(  !"
ImageFileBaseCreateDTO! 7
Image8 =
)= >
:? @
IRequestA I
<I J
ResultJ P
<P Q
ImageDTOQ Y
>Y Z
>Z [
;[ \≤'
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Image\Create\CreateImageHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Media		! &
.		& '
Image		' ,
.		, -
Create		- 3
;		3 4
public 
class 
CreateImageHandler 
:  !
IRequestHandler" 1
<1 2
CreateImageCommand2 D
,D E
ResultF L
<L M
ImageDTOM U
>U V
>V W
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

CreateImageHandler 
( 
IBlobService 
blobService  
,  !
IRepositoryWrapper 
repositoryWrapper ,
,, -
IMapper 
mapper 
, 
ILoggerService 
logger 
) 
{ 
_blobService 
= 
blobService "
;" #
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
ImageDTO %
>% &
>& '
Handle( .
(. /
CreateImageCommand/ A
requestB I
,I J
CancellationTokenK \
cancellationToken] n
)n o
{ 
string   
hashBlobStorageName   "
=  # $
_blobService  % 1
.  1 2
SaveFileInStorage  2 C
(  C D
request!! 
.!! 
Image!! 
.!! 

BaseFormat!! $
,!!$ %
request"" 
."" 
Image"" 
."" 
Title"" 
,""  
request## 
.## 
Image## 
.## 
	Extension## #
)### $
;##$ %
var%% 
image%% 
=%% 
_mapper%% 
.%% 
Map%% 
<%%  
DAL%%  #
.%%# $
Entities%%$ ,
.%%, -
Media%%- 2
.%%2 3
Images%%3 9
.%%9 :
Image%%: ?
>%%? @
(%%@ A
request%%A H
.%%H I
Image%%I N
)%%N O
;%%O P
image'' 
.'' 
BlobName'' 
='' 
$"'' 
{'' 
hashBlobStorageName'' /
}''/ 0
$str''0 1
{''1 2
request''2 9
.''9 :
Image'': ?
.''? @
	Extension''@ I
}''I J
"''J K
;''K L
await)) 
_repositoryWrapper))  
.))  !
ImageRepository))! 0
.))0 1
CreateAsync))1 <
())< =
image))= B
)))B C
;))C D
var** 
resultIsSuccess** 
=** 
await** #
_repositoryWrapper**$ 6
.**6 7
SaveChangesAsync**7 G
(**G H
)**H I
>**J K
$num**L M
;**M N
var,, 
createdImage,, 
=,, 
_mapper,, "
.,," #
Map,,# &
<,,& '
ImageDTO,,' /
>,,/ 0
(,,0 1
image,,1 6
),,6 7
;,,7 8
createdImage.. 
... 
Base64.. 
=.. 
_blobService.. *
...* +%
FindFileInStorageAsBase64..+ D
(..D E
createdImage..E Q
...Q R
BlobName..R Z
)..Z [
;..[ \
if00 

(00
 
resultIsSuccess00 
)00 
{11 	
return22 
Result22 
.22 
Ok22 
(22 
createdImage22 )
)22) *
;22* +
}33 	
else44 
{55 	
const66 
string66 
errorMsg66 !
=66" #
$str66$ ?
;66? @
_logger77 
.77 
LogError77 
(77 
request77 $
,77$ %
errorMsg77& .
)77. /
;77/ 0
return88 
Result88 
.88 
Fail88 
(88 
new88 "
Error88# (
(88( )
errorMsg88) 1
)881 2
)882 3
;883 4
}99 	
}:: 
};; ˇ$
§D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetByStreetcodeId\GetAudioByStreetcodeIdQueryHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
GetByStreetcodeId- >
;> ?
public 
class .
"GetAudioByStreetcodeIdQueryHandler /
:0 1
IRequestHandler2 A
<A B'
GetAudioByStreetcodeIdQueryB ]
,] ^
Result_ e
<e f
AudioDTOf n
>n o
>o p
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
.
"GetAudioByStreetcodeIdQueryHandler -
(- .
IRepositoryWrapper. @
repositoryWrapperA R
,R S
IMapperT [
mapper\ b
,b c
IBlobServiced p
blobServiceq |
,| }
ILoggerService	~ å
logger
ç ì
)
ì î
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
AudioDTO %
>% &
>& '
Handle( .
(. /'
GetAudioByStreetcodeIdQuery/ J
requestK R
,R S
CancellationTokenT e
cancellationTokenf w
)w x
{ 
var 

streetcode 
= 
await 
_repositoryWrapper 1
.1 2 
StreetcodeRepository2 F
.F G"
GetFirstOrDefaultAsyncG ]
(] ^
s   
=>   
s   
.   
Id   
==   
request    
.    !
StreetcodeId  ! -
,  - .
include!! 
:!! 
q!! 
=>!! 
q!! 
.!! 
Include!! #
(!!# $
s!!$ %
=>!!& (
s!!) *
.!!* +
Audio!!+ 0
)!!0 1
!!!2 3
)!!3 4
;!!4 5
if"" 

("" 

streetcode"" 
=="" 
null"" 
)"" 
{## 	
string$$ 
errorMsg$$ 
=$$ 
$"$$  
$str$$  [
{$$[ \
request$$\ c
.$$c d
StreetcodeId$$d p
}$$p q
"$$q r
;$$r s
_logger%% 
.%% 
LogError%% 
(%% 
request%% $
,%%$ %
errorMsg%%& .
)%%. /
;%%/ 0
return&& 
Result&& 
.&& 
Fail&& 
(&& 
new&& "
Error&&# (
(&&( )
errorMsg&&) 1
)&&1 2
)&&2 3
;&&3 4
}'' 	

NullResult)) 
<)) 
AudioDTO)) 
>)) 
result)) #
=))$ %
new))& )

NullResult))* 4
<))4 5
AudioDTO))5 =
>))= >
())> ?
)))? @
;))@ A
if++ 

(++ 

streetcode++ 
.++ 
Audio++ 
!=++ 
null++  $
)++$ %
{,, 	
AudioDTO-- 
audioDto-- 
=-- 
_mapper--  '
.--' (
Map--( +
<--+ ,
AudioDTO--, 4
>--4 5
(--5 6

streetcode--6 @
.--@ A
Audio--A F
)--F G
;--G H
audioDto.. 
... 
Base64.. 
=.. 
_blobService.. *
...* +%
FindFileInStorageAsBase64..+ D
(..D E
audioDto..E M
...M N
BlobName..N V
)..V W
;..W X
result// 
.// 
	WithValue// 
(// 
audioDto// %
)//% &
;//& '
}00 	
return22 
result22 
;22 
}33 
}44 í
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetById\GetAudioByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
GetById- 4
;4 5
public 
record 
GetAudioByIdQuery 
(  
int  #
Id$ &
)& '
:( )
IRequest* 2
<2 3
Result3 9
<9 :
AudioDTO: B
>B C
>C D
;D Eƒ
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetByStreetcodeId\GetAudioByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
GetByStreetcodeId- >
;> ?
public 
record '
GetAudioByStreetcodeIdQuery )
() *
int* -
StreetcodeId. :
): ;
:< =
IRequest> F
<F G
ResultG M
<M N
AudioDTON V
>V W
>W X
;X Yå
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetById\GetAudioByIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Media		! &
.		& '
Audio		' ,
.		, -
GetById		- 4
;		4 5
public 
class 
GetAudioByIdHandler  
:! "
IRequestHandler# 2
<2 3
GetAudioByIdQuery3 D
,D E
ResultF L
<L M
AudioDTOM U
>U V
>V W
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAudioByIdHandler 
( 
IRepositoryWrapper 1
repositoryWrapper2 C
,C D
IMapperE L
mapperM S
,S T
IBlobServiceU a
blobServiceb m
,m n
ILoggerServiceo }
logger	~ Ñ
)
Ñ Ö
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
AudioDTO %
>% &
>& '
Handle( .
(. /
GetAudioByIdQuery/ @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 
var 
audio 
= 
await 
_repositoryWrapper ,
., -
AudioRepository- <
.< ="
GetFirstOrDefaultAsync= S
(S T
fT U
=>V X
fY Z
.Z [
Id[ ]
==^ `
requesta h
.h i
Idi k
)k l
;l m
if 

( 
audio 
is 
null 
) 
{ 	
string   
errorMsg   
=   
$"    
$str    L
{  L M
request  M T
.  T U
Id  U W
}  W X
"  X Y
;  Y Z
_logger!! 
.!! 
LogError!! 
(!! 
request!! $
,!!$ %
errorMsg!!& .
)!!. /
;!!/ 0
return"" 
Result"" 
."" 
Fail"" 
("" 
new"" "
Error""# (
(""( )
errorMsg"") 1
)""1 2
)""2 3
;""3 4
}## 	
var%% 
audioDto%% 
=%% 
_mapper%% 
.%% 
Map%% "
<%%" #
AudioDTO%%# +
>%%+ ,
(%%, -
audio%%- 2
)%%2 3
;%%3 4
audioDto'' 
.'' 
Base64'' 
='' 
_blobService'' &
.''& '%
FindFileInStorageAsBase64''' @
(''@ A
audioDto''A I
.''I J
BlobName''J R
)''R S
;''S T
return)) 
Result)) 
.)) 
Ok)) 
()) 
audioDto)) !
)))! "
;))" #
}** 
}++ †
éD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetBaseAudio\GetBaseAudioQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
GetBaseAudio- 9
;9 :
public 
record 
GetBaseAudioQuery 
(  
int  #
Id$ &
)& '
:( )
IRequest* 2
<2 3
Result3 9
<9 :
MemoryStream: F
>F G
>G H
;H Iû
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetBaseAudio\GetBaseAudioHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
GetBaseAudio- 9
;9 :
public		 
class		 
GetBaseAudioHandler		  
:		! "
IRequestHandler		# 2
<		2 3
GetBaseAudioQuery		3 D
,		D E
Result		F L
<		L M
MemoryStream		M Y
>		Y Z
>		Z [
{

 
private 
readonly 
IBlobService !
_blobStorage" .
;. /
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetBaseAudioHandler 
( 
IBlobService +
blobService, 7
,7 8
IRepositoryWrapper9 K
repositoryWrapperL ]
,] ^
ILoggerService_ m
loggern t
)t u
{ 
_blobStorage 
= 
blobService "
;" #
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
MemoryStream )
>) *
>* +
Handle, 2
(2 3
GetBaseAudioQuery3 D
requestE L
,L M
CancellationTokenN _
cancellationToken` q
)q r
{ 
var 
audio 
= 
await 
_repositoryWrapper ,
., -
AudioRepository- <
.< ="
GetFirstOrDefaultAsync= S
(S T
aT U
=>V X
aY Z
.Z [
Id[ ]
==^ `
requesta h
.h i
Idi k
)k l
;l m
if 

( 
audio 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  L
{L M
requestM T
.T U
IdU W
}W X
"X Y
;Y Z
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
} 	
return!! 
_blobStorage!! 
.!! +
FindFileInStorageAsMemoryStream!! ;
(!!; <
audio!!< A
.!!A B
BlobName!!B J
)!!J K
;!!K L
}"" 
}## à
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetAll\GetAllAudiosQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
GetAll- 3
;3 4
public 
record 
GetAllAudiosQuery 
:  !
IRequest" *
<* +
Result+ 1
<1 2
IEnumerable2 =
<= >
AudioDTO> F
>F G
>G H
>H I
;I J–
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\GetAll\GetAllAudiosHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Audio

' ,
.

, -
GetAll

- 3
;

3 4
public 
class 
GetAllAudiosHandler  
:! "
IRequestHandler# 2
<2 3
GetAllAudiosQuery3 D
,D E
ResultF L
<L M
IEnumerableM X
<X Y
AudioDTOY a
>a b
>b c
>c d
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAllAudiosHandler 
( 
IRepositoryWrapper 1
repositoryWrapper2 C
,C D
IMapperE L
mapperM S
,S T
IBlobServiceU a
blobServiceb m
,m n
ILoggerServiceo }
logger	~ Ñ
)
Ñ Ö
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
AudioDTO) 1
>1 2
>2 3
>3 4
Handle5 ;
(; <
GetAllAudiosQuery< M
requestN U
,U V
CancellationTokenW h
cancellationTokeni z
)z {
{ 
var 
audios 
= 
await 
_repositoryWrapper -
.- .
AudioRepository. =
.= >
GetAllAsync> I
(I J
)J K
;K L
if 

( 
audios 
is 
null 
) 
{   	
const!! 
string!! 
errorMsg!! !
=!!" #
$str!!$ <
;!!< =
_logger"" 
."" 
LogError"" 
("" 
request"" $
,""$ %
errorMsg""& .
)"". /
;""/ 0
return## 
Result## 
.## 
Fail## 
(## 
new## "
Error### (
(##( )
errorMsg##) 1
)##1 2
)##2 3
;##3 4
}$$ 	
var&& 
	audioDtos&& 
=&& 
_mapper&& 
.&&  
Map&&  #
<&&# $
IEnumerable&&$ /
<&&/ 0
AudioDTO&&0 8
>&&8 9
>&&9 :
(&&: ;
audios&&; A
)&&A B
;&&B C
foreach'' 
('' 
var'' 
audio'' 
in'' 
	audioDtos'' '
)''' (
{(( 	
audio)) 
.)) 
Base64)) 
=)) 
_blobService)) '
.))' (%
FindFileInStorageAsBase64))( A
())A B
audio))B G
.))G H
BlobName))H P
)))P Q
;))Q R
}** 	
return,, 
Result,, 
.,, 
Ok,, 
(,, 
	audioDtos,, "
),," #
;,,# $
}-- 
}.. ∫$
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\Delete\DeleteAudioHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
Delete- 3
;3 4
public		 
class		 
DeleteAudioHandler		 
:		  !
IRequestHandler		" 1
<		1 2
DeleteAudioCommand		2 D
,		D E
Result		F L
<		L M
Unit		M Q
>		Q R
>		R S
{

 
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

DeleteAudioHandler 
( 
IRepositoryWrapper 0
repositoryWrapper1 B
,B C
IBlobServiceD P
blobServiceQ \
,\ ]
ILoggerService^ l
loggerm s
)s t
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_blobService 
= 
blobService "
;" #
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +
DeleteAudioCommand+ =
request> E
,E F
CancellationTokenG X
cancellationTokenY j
)j k
{ 
var 
audio 
= 
await 
_repositoryWrapper ,
., -
AudioRepository- <
.< ="
GetFirstOrDefaultAsync= S
(S T
aT U
=>V X
aY Z
.Z [
Id[ ]
==^ `
requesta h
.h i
Idi k
)k l
;l m
if 

( 
audio 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  Q
{Q R
requestR Y
.Y Z
IdZ \
}\ ]
"] ^
;^ _
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
} 	
_repositoryWrapper!! 
.!! 
AudioRepository!! *
.!!* +
Delete!!+ 1
(!!1 2
audio!!2 7
)!!7 8
;!!8 9
var## 
resultIsSuccess## 
=## 
await## #
_repositoryWrapper##$ 6
.##6 7
SaveChangesAsync##7 G
(##G H
)##H I
>##J K
$num##L M
;##M N
if%% 

(%% 
resultIsSuccess%% 
)%% 
{&& 	
_blobService'' 
.'' 
DeleteFileInStorage'' ,
('', -
audio''- 2
.''2 3
BlobName''3 ;
)''; <
;''< =
}(( 	
if** 

(** 
resultIsSuccess** 
)** 
{++ 	
_logger,, 
?,, 
.,, 
LogInformation,, #
(,,# $
$",,$ &
$str,,& M
",,M N
),,N O
;,,O P
return-- 
Result-- 
.-- 
Ok-- 
(-- 
Unit-- !
.--! "
Value--" '
)--' (
;--( )
}.. 	
else// 
{00 	
string11 
errorMsg11 
=11 
$"11  
$str11  9
"119 :
;11: ;
_logger22 
.22 
LogError22 
(22 
request22 $
,22$ %
errorMsg22& .
)22. /
;22/ 0
return33 
Result33 
.33 
Fail33 
(33 
new33 "
Error33# (
(33( )
errorMsg33) 1
)331 2
)332 3
;333 4
}44 	
}55 
}66 é
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\Delete\DeleteAudioCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
Delete- 3
;3 4
public 
record 
DeleteAudioCommand  
(  !
int! $
Id% '
)' (
:) *
IRequest+ 3
<3 4
Result4 :
<: ;
Unit; ?
>? @
>@ A
;A B¶%
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\Create\CreateAudioHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
Media		! &
.		& '
Audio		' ,
.		, -
Create		- 3
;		3 4
public 
class 
CreateAudioHandler 
:  !
IRequestHandler" 1
<1 2
CreateAudioCommand2 D
,D E
ResultF L
<L M
AudioDTOM U
>U V
>V W
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
IBlobService !
_blobService" .
;. /
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

CreateAudioHandler 
( 
IBlobService 
blobService  
,  !
IRepositoryWrapper 
repositoryWrapper ,
,, -
IMapper 
mapper 
, 
ILoggerService 
logger 
) 
{ 
_blobService 
= 
blobService "
;" #
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
AudioDTO %
>% &
>& '
Handle( .
(. /
CreateAudioCommand/ A
requestB I
,I J
CancellationTokenK \
cancellationToken] n
)n o
{ 
string   
hashBlobStorageName   "
=  # $
_blobService  % 1
.  1 2
SaveFileInStorage  2 C
(  C D
request!! 
.!! 
Audio!! 
.!! 

BaseFormat!! $
,!!$ %
request"" 
."" 
Audio"" 
."" 
Title"" 
,""  
request## 
.## 
Audio## 
.## 
	Extension## #
)### $
;##$ %
var%% 
audio%% 
=%% 
_mapper%% 
.%% 
Map%% 
<%%  
DAL%%  #
.%%# $
Entities%%$ ,
.%%, -
Media%%- 2
.%%2 3
Audio%%3 8
>%%8 9
(%%9 :
request%%: A
.%%A B
Audio%%B G
)%%G H
;%%H I
audio'' 
.'' 
BlobName'' 
='' 
$"'' 
{'' 
hashBlobStorageName'' /
}''/ 0
$str''0 1
{''1 2
request''2 9
.''9 :
Audio'': ?
.''? @
	Extension''@ I
}''I J
"''J K
;''K L
await)) 
_repositoryWrapper))  
.))  !
AudioRepository))! 0
.))0 1
CreateAsync))1 <
())< =
audio))= B
)))B C
;))C D
var++ 
resultIsSuccess++ 
=++ 
await++ #
_repositoryWrapper++$ 6
.++6 7
SaveChangesAsync++7 G
(++G H
)++H I
>++J K
$num++L M
;++M N
var-- 
createdAudio-- 
=-- 
_mapper-- "
.--" #
Map--# &
<--& '
AudioDTO--' /
>--/ 0
(--0 1
audio--1 6
)--6 7
;--7 8
if// 

(//
 
resultIsSuccess// 
)// 
{00 	
return11 
Result11 
.11 
Ok11 
(11 
createdAudio11 )
)11) *
;11* +
}22 	
else33 
{44 	
const55 
string55 
errorMsg55 !
=55" #
$"55$ &
$str55& ?
"55? @
;55@ A
_logger66 
.66 
LogError66 
(66 
request66 $
,66$ %
errorMsg66& .
)66. /
;66/ 0
return77 
Result77 
.77 
Fail77 
(77 
new77 "
Error77# (
(77( )
errorMsg77) 1
)771 2
)772 3
;773 4
}88 	
}99 
}:: ®
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Audio\Create\CreateAudioCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Audio' ,
., -
Create- 3
;3 4
public 
record 
CreateAudioCommand  
(  !"
AudioFileBaseCreateDTO! 7
Audio8 =
)= >
:? @
IRequestA I
<I J
ResultJ P
<P Q
AudioDTOQ Y
>Y Z
>Z [
;[ \Ä
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Art\GetByStreetcodeId\GetArtsByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Art' *
.* +
GetByStreetcodeId+ <
{ 
public 
record	 &
GetArtsByStreetcodeIdQuery *
(* +
int+ .
StreetcodeId/ ;
); <
:= >
IRequest? G
<G H
ResultH N
<N O
IEnumerableO Z
<Z [
ArtDTO[ a
>a b
>b c
>c d
;d e
} †+
úD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Art\GetByStreetcodeId\GetArtsByStreetcodeIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Art

' *
.

* +
GetByStreetcodeId

+ <
{ 
public 
class	 (
GetArtsByStreetcodeIdHandler +
:, -
IRequestHandler. =
<= >&
GetArtsByStreetcodeIdQuery> X
,X Y
ResultZ `
<` a
IEnumerablea l
<l m
ArtDTOm s
>s t
>t u
>u v
{ 
private 
readonly 
IBlobService %
_blobService& 2
;2 3
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public (
GetArtsByStreetcodeIdHandler +
(+ ,
IRepositoryWrapper 
repositoryWrapper 0
,0 1
IMapper 
mapper 
, 
IBlobService 
blobService $
,$ %
ILoggerService 
logger !
)! "
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_blobService 
= 
blobService &
;& '
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
ArtDTO- 3
>3 4
>4 5
>5 6
Handle7 =
(= >&
GetArtsByStreetcodeIdQuery> X
requestY `
,` a
CancellationTokenb s
cancellationToken	t Ö
)
Ö Ü
{   	
var(( 
arts(( 
=(( 
await(( 
_repositoryWrapper(( /
.((/ 0
ArtRepository((0 =
.)) 
GetAllAsync)) 
()) 
	predicate** 
:** 
sc** 
=>**  
sc**! #
.**# $
StreetcodeArts**$ 2
.**2 3
Any**3 6
(**6 7
s**7 8
=>**9 ;
s**< =
.**= >
StreetcodeId**> J
==**K M
request**N U
.**U V
StreetcodeId**V b
)**b c
,**c d
include++ 
:++ 
scl++ 
=>++ 
scl++  #
.,, 
Include,, 
(,, 
sc,, 
=>,,  "
sc,,# %
.,,% &
Image,,& +
),,+ ,
!,,- .
),,. /
;,,/ 0
if.. 
(.. 
arts.. 
is.. 
null.. 
).. 
{// 
string00 
errorMsg00 
=00  !
$"00" $
$str00$ Z
{00Z [
request00[ b
.00b c
StreetcodeId00c o
}00o p
"00p q
;00q r
_logger11 
.11 
LogError11  
(11  !
request11! (
,11( )
errorMsg11* 2
)112 3
;113 4
return22 
Result22 
.22 
Fail22 "
(22" #
new22# &
Error22' ,
(22, -
errorMsg22- 5
)225 6
)226 7
;227 8
}33 
var55 
imageIds55 
=55 
arts55 
.55  
Where55  %
(55% &
a55& '
=>55( *
a55+ ,
.55, -
Image55- 2
!=553 5
null556 :
)55: ;
.55; <
Select55< B
(55B C
a55C D
=>55E G
a55H I
.55I J
Image55J O
!55O P
.55P Q
Id55Q S
)55S T
;55T U
var77 
artsDto77 
=77 
_mapper77 !
.77! "
Map77" %
<77% &
IEnumerable77& 1
<771 2
ArtDTO772 8
>778 9
>779 :
(77: ;
arts77; ?
)77? @
;77@ A
foreach88 
(88 
var88 
artDto88 
in88  "
artsDto88# *
)88* +
{99 
if:: 
(:: 
artDto:: 
.:: 
Image::  
!=::! #
null::$ (
&&::) +
artDto::, 2
.::2 3
Image::3 8
.::8 9
BlobName::9 A
!=::B D
null::E I
)::I J
{;; 
artDto<< 
.<< 
Image<<  
.<<  !
Base64<<! '
=<<( )
_blobService<<* 6
.<<6 7%
FindFileInStorageAsBase64<<7 P
(<<P Q
artDto<<Q W
.<<W X
Image<<X ]
.<<] ^
BlobName<<^ f
)<<f g
;<<g h
}== 
}>> 
return@@ 
Result@@ 
.@@ 
Ok@@ 
(@@ 
artsDto@@ $
)@@$ %
;@@% &
}AA 	
}BB 
}CC à
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Art\GetById\GetArtByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Art' *
.* +
GetById+ 2
;2 3
public 
record 
GetArtByIdQuery 
( 
int !
Id" $
)$ %
:& '
IRequest( 0
<0 1
Result1 7
<7 8
ArtDTO8 >
>> ?
>? @
;@ Aë
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Art\GetById\GetArtByIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Art

' *
.

* +
GetById

+ 2
;

2 3
public 
class 
GetArtByIdHandler 
:  
IRequestHandler! 0
<0 1
GetArtByIdQuery1 @
,@ A
ResultB H
<H I
ArtDTOI O
>O P
>P Q
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetArtByIdHandler 
( 
IRepositoryWrapper /
repositoryWrapper0 A
,A B
IMapperC J
mapperK Q
,Q R
ILoggerServiceS a
loggerb h
)h i
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
ArtDTO #
># $
>$ %
Handle& ,
(, -
GetArtByIdQuery- <
request= D
,D E
CancellationTokenF W
cancellationTokenX i
)i j
{ 
var 
art 
= 
await 
_repositoryWrapper *
.* +
ArtRepository+ 8
.8 9"
GetFirstOrDefaultAsync9 O
(O P
fP Q
=>R T
fU V
.V W
IdW Y
==Z \
request] d
.d e
Ide g
)g h
;h i
if 

( 
art 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  J
{J K
requestK R
.R S
IdS U
}U V
"V W
;W X
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$  
.$$  !
Map$$! $
<$$$ %
ArtDTO$$% +
>$$+ ,
($$, -
art$$- 0
)$$0 1
)$$1 2
;$$2 3
}%% 
}&& ˛
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Art\GetAll\GetAllArtsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Media! &
.& '
Art' *
.* +
GetAll+ 1
;1 2
public 
record 
GetAllArtsQuery 
: 
IRequest  (
<( )
Result) /
</ 0
IEnumerable0 ;
<; <
ArtDTO< B
>B C
>C D
>D E
;E F‹
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Media\Art\GetAll\GetAllArtsHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
Media

! &
.

& '
Art

' *
.

* +
GetAll

+ 1
;

1 2
public 
class 
GetAllArtsHandler 
:  
IRequestHandler! 0
<0 1
GetAllArtsQuery1 @
,@ A
ResultB H
<H I
IEnumerableI T
<T U
ArtDTOU [
>[ \
>\ ]
>] ^
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAllArtsHandler 
( 
IRepositoryWrapper /
repositoryWrapper0 A
,A B
IMapperC J
mapperK Q
,Q R
ILoggerServiceS a
loggerb h
)h i
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
ArtDTO) /
>/ 0
>0 1
>1 2
Handle3 9
(9 :
GetAllArtsQuery: I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 
var 
arts 
= 
await 
_repositoryWrapper +
.+ ,
ArtRepository, 9
.9 :
GetAllAsync: E
(E F
)F G
;G H
if 

( 
arts 
is 
null 
) 
{ 	
const 
string 
errorMsg !
=" #
$"$ &
$str& :
": ;
;; <
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$  
.$$  !
Map$$! $
<$$$ %
IEnumerable$$% 0
<$$0 1
ArtDTO$$1 7
>$$7 8
>$$8 9
($$9 :
arts$$: >
)$$> ?
)$$? @
;$$@ A
}%% 
}&& Î
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Instagram\GetAll\GetAllPostsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
	Instagram! *
.* +
GetAll+ 1
;1 2
public 
record 
GetAllPostsQuery 
:  
IRequest! )
<) *
Result* 0
<0 1
IEnumerable1 <
<< =
InstagramPost= J
>J K
>K L
>L M
;M N¢
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Instagram\GetAll\GetAllPostsHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
	Instagram! *
.* +
GetAll+ 1
{ 
public		 

class		 
GetAllPostsHandler		 #
:		$ %
IRequestHandler		& 5
<		5 6
GetAllPostsQuery		6 F
,		F G
Result		H N
<		N O
IEnumerable		O Z
<		Z [
InstagramPost		[ h
>		h i
>		i j
>		j k
{

 
private 
readonly 
IInstagramService *
_instagramService+ <
;< =
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
GetAllPostsHandler !
(! "
IInstagramService" 3
instagramService4 D
,D E
ILoggerServiceF T
loggerU [
)[ \
{ 	
_instagramService 
= 
instagramService  0
;0 1
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
IEnumerable! ,
<, -
InstagramPost- :
>: ;
>; <
>< =
Handle> D
(D E
GetAllPostsQueryE U
requestV ]
,] ^
CancellationToken_ p
cancellationToken	q Ç
)
Ç É
{ 	
var 
result 
= 
await 
_instagramService 0
.0 1
GetPostsAsync1 >
(> ?
)? @
;@ A
return 
Result 
. 
Ok 
( 
result #
)# $
;$ %
} 	
} 
} ê
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Email\SendEmailHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Email! &
{ 
public		 

class		 
SendEmailHandler		 !
:		" #
IRequestHandler		$ 3
<		3 4
SendEmailCommand		4 D
,		D E
Result		F L
<		L M
Unit		M Q
>		Q R
>		R S
{

 
private 
readonly 
IEmailService &
_emailService' 4
;4 5
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
SendEmailHandler 
(  
IEmailService  -
emailService. :
,: ;
ILoggerService< J
loggerK Q
)Q R
{ 	
_emailService 
= 
emailService (
;( )
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
Unit! %
>% &
>& '
Handle( .
(. /
SendEmailCommand/ ?
request@ G
,G H
CancellationTokenI Z
cancellationToken[ l
)l m
{ 	
var 
message 
= 
new 
Message %
(% &
new& )
string* 0
[0 1
]1 2
{3 4
$str5 M
}N O
,O P
requestQ X
.X Y
EmailY ^
.^ _
From_ c
,c d
$stre o
,o p
requestq x
.x y
Emaily ~
.~ 
Content	 Ü
)
Ü á
;
á à
bool 
isResultSuccess  
=! "
await# (
_emailService) 6
.6 7
SendEmailAsync7 E
(E F
messageF M
)M N
;N O
if 
( 
isResultSuccess 
) 
{ 
return 
Result 
. 
Ok  
(  !
Unit! %
.% &
Value& +
)+ ,
;, -
} 
else 
{ 
const 
string 
errorMsg %
=& '
$"( *
$str* F
"F G
;G H
_logger   
.   
LogError    
(    !
request  ! (
,  ( )
errorMsg  * 2
)  2 3
;  3 4
return!! 
Result!! 
.!! 
Fail!! "
(!!" #
new!!# &
Error!!' ,
(!!, -
errorMsg!!- 5
)!!5 6
)!!6 7
;!!7 8
}"" 
}## 	
}$$ 
}%% ø
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\Email\SendEmailCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
Email! &
;& '
public 
record 
SendEmailCommand 
( 
EmailDTO '
Email( -
)- .
:/ 0
IRequest1 9
<9 :
Result: @
<@ A
UnitA E
>E F
>F G
;G Hº
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetTagByTitle\GetTagByTitleQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
GetByStreetcodeId7 H
;H I
public 
record 
GetTagByTitleQuery  
(  !
string! '
Title( -
)- .
:/ 0
IRequest1 9
<9 :
Result: @
<@ A
TagDTOA G
>G H
>H I
;I JÕ
úD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetTagByTitle\GetTagByTitleHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
AdditionalContent		! 2
.		2 3
Tag		3 6
.		6 7
GetTagByTitle		7 D
;		D E
public 
class  
GetTagByTitleHandler !
:" #
IRequestHandler$ 3
<3 4
GetTagByTitleQuery4 F
,F G
ResultH N
<N O
TagDTOO U
>U V
>V W
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
 
GetTagByTitleHandler 
(  
IRepositoryWrapper  2
repositoryWrapper3 D
,D E
IMapperF M
mapperN T
,T U
ILoggerServiceV d
loggere k
)k l
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TagDTO #
># $
>$ %
Handle& ,
(, -
GetTagByTitleQuery- ?
request@ G
,G H
CancellationTokenI Z
cancellationToken[ l
)l m
{ 
var 
tag 
= 
await 
_repositoryWrapper *
.* +
TagRepository+ 8
.8 9"
GetFirstOrDefaultAsync9 O
(O P
fP Q
=>R T
fU V
.V W
TitleW \
==] _
request` g
.g h
Titleh m
)m n
;n o
if 

( 
tag 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  B
{B C
requestC J
.J K
TitleK P
}P Q
"Q R
;R S
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return   
Result   
.   
Fail   
(   
new   "
Error  # (
(  ( )
errorMsg  ) 1
)  1 2
)  2 3
;  3 4
}!! 	
return## 
Result## 
.## 
Ok## 
(## 
_mapper##  
.##  !
Map##! $
<##$ %
TagDTO##% +
>##+ ,
(##, -
tag##- 0
)##0 1
)##1 2
;##2 3
}$$ 
}%% ì
•D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetByStreetcodeId\GetTagByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
GetByStreetcodeId7 H
;H I
public 
record %
GetTagByStreetcodeIdQuery '
(' (
int( +
StreetcodeId, 8
)8 9
:: ;
IRequest< D
<D E
ResultE K
<K L
IEnumerableL W
<W X
StreetcodeTagDTOX h
>h i
>i j
>j k
;k lÒ
ßD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetByStreetcodeId\GetTagByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
GetByStreetcodeId7 H
;H I
public 
class '
GetTagByStreetcodeIdHandler (
:) *
IRequestHandler+ :
<: ;%
GetTagByStreetcodeIdQuery; T
,T U
ResultV \
<\ ]
IEnumerable] h
<h i
StreetcodeTagDTOi y
>y z
>z {
>{ |
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
'
GetTagByStreetcodeIdHandler &
(& '
IRepositoryWrapper' 9
repositoryWrapper: K
,K L
IMapperM T
mapperU [
,[ \
ILoggerService] k
loggerl r
)r s
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
StreetcodeTagDTO) 9
>9 :
>: ;
>; <
Handle= C
(C D%
GetTagByStreetcodeIdQueryD ]
request^ e
,e f
CancellationTokeng x
cancellationToken	y ä
)
ä ã
{ 
var## 

tagIndexed## 
=## 
await## 
_repositoryWrapper## 1
.##1 2(
StreetcodeTagIndexRepository##2 N
.$$ 
GetAllAsync$$ 
($$ 
t%% 
=>%% 
t%% 
.%% 
StreetcodeId%% #
==%%$ &
request%%' .
.%%. /
StreetcodeId%%/ ;
,%%; <
include&& 
:&& 
q&& 
=>&& 
q&& 
.&&  
Include&&  '
(&&' (
t&&( )
=>&&* ,
t&&- .
.&&. /
Tag&&/ 2
)&&2 3
)&&3 4
;&&4 5
if(( 

((( 

tagIndexed(( 
is(( 
null(( 
)(( 
{)) 	
string** 
errorMsg** 
=** 
$"**  
$str**  J
{**J K
request**K R
.**R S
StreetcodeId**S _
}**_ `
"**` a
;**a b
_logger++ 
.++ 
LogError++ 
(++ 
request++ $
,++$ %
errorMsg++& .
)++. /
;++/ 0
return,, 
Result,, 
.,, 
Fail,, 
(,, 
new,, "
Error,,# (
(,,( )
errorMsg,,) 1
),,1 2
),,2 3
;,,3 4
}-- 	
return// 
Result// 
.// 
Ok// 
(// 
_mapper//  
.//  !
Map//! $
<//$ %
IEnumerable//% 0
<//0 1
StreetcodeTagDTO//1 A
>//A B
>//B C
(//C D

tagIndexed//D N
.//N O
OrderBy//O V
(//V W
ti//W Y
=>//Z \
ti//] _
.//_ `
Index//` e
)//e f
)//f g
)//g h
;//h i
}00 
}11 †
ëD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetById\GetTagByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
GetById7 >
;> ?
public 
record 
GetTagByIdQuery 
( 
int !
Id" $
)$ %
:& '
IRequest( 0
<0 1
Result1 7
<7 8
TagDTO8 >
>> ?
>? @
;@ A©
ìD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetById\GetTagByIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
GetById7 >
;> ?
public

 
class

 
GetTagByIdHandler

 
:

  
IRequestHandler

! 0
<

0 1
GetTagByIdQuery

1 @
,

@ A
Result

B H
<

H I
TagDTO

I O
>

O P
>

P Q
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetTagByIdHandler 
( 
IRepositoryWrapper /
repositoryWrapper0 A
,A B
IMapperC J
mapperK Q
,Q R
ILoggerServiceS a
loggerb h
)h i
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
TagDTO #
># $
>$ %
Handle& ,
(, -
GetTagByIdQuery- <
request= D
,D E
CancellationTokenF W
cancellationTokenX i
)i j
{ 
var 
tag 
= 
await 
_repositoryWrapper *
.* +
TagRepository+ 8
.8 9"
GetFirstOrDefaultAsync9 O
(O P
fP Q
=>R T
fU V
.V W
IdW Y
==Z \
request] d
.d e
Ide g
)g h
;h i
if 

( 
tag 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  I
{I J
requestJ Q
.Q R
IdR T
}T U
"U V
;V W
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
errorMsg) 1
)1 2
)2 3
;3 4
}   	
return"" 
Result"" 
."" 
Ok"" 
("" 
_mapper""  
.""  !
Map""! $
<""$ %
TagDTO""% +
>""+ ,
("", -
tag""- 0
)""0 1
)""1 2
;""2 3
}## 
}$$ ñ
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetAll\GetAllTagsQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
GetAll7 =
;= >
public 
record 
GetAllTagsQuery 
: 
IRequest  (
<( )
Result) /
</ 0
IEnumerable0 ;
<; <
TagDTO< B
>B C
>C D
>D E
;E FÙ
íD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\GetAll\GetAllTagsHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
AdditionalContent

! 2
.

2 3
Tag

3 6
.

6 7
GetAll

7 =
;

= >
public 
class 
GetAllTagsHandler 
:  
IRequestHandler! 0
<0 1
GetAllTagsQuery1 @
,@ A
ResultB H
<H I
IEnumerableI T
<T U
TagDTOU [
>[ \
>\ ]
>] ^
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 

GetAllTagsHandler 
( 
IRepositoryWrapper /
repositoryWrapper0 A
,A B
IMapperC J
mapperK Q
,Q R
ILoggerServiceS a
loggerb h
)h i
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
TagDTO) /
>/ 0
>0 1
>1 2
Handle3 9
(9 :
GetAllTagsQuery: I
requestJ Q
,Q R
CancellationTokenS d
cancellationTokene v
)v w
{ 
var 
tags 
= 
await 
_repositoryWrapper +
.+ ,
TagRepository, 9
.9 :
GetAllAsync: E
(E F
)F G
;G H
if 

( 
tags 
is 
null 
) 
{ 	
const 
string 
errorMsg !
=" #
$"$ &
$str& :
": ;
;; <
_logger   
.   
LogError   
(   
request   $
,  $ %
errorMsg  & .
)  . /
;  / 0
return!! 
Result!! 
.!! 
Fail!! 
(!! 
new!! "
Error!!# (
(!!( )
errorMsg!!) 1
)!!1 2
)!!2 3
;!!3 4
}"" 	
return$$ 
Result$$ 
.$$ 
Ok$$ 
($$ 
_mapper$$  
.$$  !
Map$$! $
<$$$ %
IEnumerable$$% 0
<$$0 1
TagDTO$$1 7
>$$7 8
>$$8 9
($$9 :
tags$$: >
)$$> ?
)$$? @
;$$@ A
}%% 
}&& ≥
èD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\Create\CreateTagQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
Create7 =
{ 
public 
record	 
CreateTagQuery 
( 
CreateTagDTO +
tag, /
)/ 0
:1 2
IRequest3 ;
<; <
Result< B
<B C
TagDTOC I
>I J
>J K
;K L
}		 á
ëD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Tag\Create\CreateTagHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Tag3 6
.6 7
Create7 =
{		 
public

 
class

	 
CreateTagHandler

 
:

  !
IRequestHandler

" 1
<

1 2
CreateTagQuery

2 @
,

@ A
Result

B H
<

H I
TagDTO

I O
>

O P
>

P Q
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public 
CreateTagHandler 
(  
IRepositoryWrapper  2
repositoryWrapper3 D
,D E
IMapperF M
mapperN T
,T U
ILoggerServiceV d
loggere k
)k l
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
TagDTO! '
>' (
>( )
Handle* 0
(0 1
CreateTagQuery1 ?
request@ G
,G H
CancellationTokenI Z
cancellationToken[ l
)l m
{ 	
var 
newTag 
= 
await 
_repositoryWrapper 1
.1 2
TagRepository2 ?
.? @
CreateAsync@ K
(K L
newL O
DALP S
.S T
EntitiesT \
.\ ]
AdditionalContent] n
.n o
Tago r
(r s
)s t
{ 
Title 
= 
request 
.  
tag  #
.# $
Title$ )
} 
) 
; 
try 
{ 
await   
_repositoryWrapper   (
.  ( )
SaveChangesAsync  ) 9
(  9 :
)  : ;
;  ; <
}!! 
catch"" 
("" 
	Exception"" 
ex"" 
)"" 
{## 
_logger$$ 
.$$ 
LogError$$  
($$  !
request$$! (
,$$( )
ex$$* ,
.$$, -
ToString$$- 5
($$5 6
)$$6 7
)$$7 8
;$$8 9
return%% 
Result%% 
.%% 
Fail%% "
(%%" #
ex%%# %
.%%% &
ToString%%& .
(%%. /
)%%/ 0
)%%0 1
;%%1 2
}&& 
return(( 
Result(( 
.(( 
Ok(( 
((( 
_mapper(( $
.(($ %
Map((% (
<((( )
TagDTO(() /
>((/ 0
(((0 1
newTag((1 7
)((7 8
)((8 9
;((9 :
})) 	
}** 
}++ ˙
∞D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Subtitle\GetByStreetcodeId\GetSubtitlesByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Subtitle3 ;
.; <
GetByStreetcodeId< M
{ 
public 

record +
GetSubtitlesByStreetcodeIdQuery 1
(1 2
int2 5
StreetcodeId6 B
)B C
:D E
IRequestF N
<N O
ResultO U
<U V
SubtitleDTOV a
>a b
>b c
;c d
} €
≤D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Subtitle\GetByStreetcodeId\GetSubtitlesByStreetcodeIdHandler.cs
	namespace

 	

Streetcode


 
.

 
BLL

 
.

 
MediatR

  
.

  !
AdditionalContent

! 2
.

2 3
Subtitle

3 ;
.

; <
GetByStreetcodeId

< M
{ 
public 

class -
!GetSubtitlesByStreetcodeIdHandler 2
:3 4
IRequestHandler5 D
<D E+
GetSubtitlesByStreetcodeIdQueryE d
,d e
Resultf l
<l m
SubtitleDTOm x
>x y
>y z
{ 
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
IRepositoryWrapper +
_repositoryWrapper, >
;> ?
private 
readonly 
ILoggerService '
_logger( /
;/ 0
public -
!GetSubtitlesByStreetcodeIdHandler 0
(0 1
IRepositoryWrapper1 C
repositoryWrapperD U
,U V
IMapperW ^
mapper_ e
,e f
ILoggerServiceg u
loggerv |
)| }
{ 	
_repositoryWrapper 
=  
repositoryWrapper! 2
;2 3
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Result  
<  !
SubtitleDTO! ,
>, -
>- .
Handle/ 5
(5 6+
GetSubtitlesByStreetcodeIdQuery6 U
requestV ]
,] ^
CancellationToken_ p
cancellationToken	q Ç
)
Ç É
{ 	
var 
subtitle 
= 
await  
_repositoryWrapper! 3
.3 4
SubtitleRepository4 F
. "
GetFirstOrDefaultAsync '
(' (
Subtitle( 0
=>1 3
Subtitle4 <
.< =
StreetcodeId= I
==J L
requestM T
.T U
StreetcodeIdU a
)a b
;b c

NullResult 
< 
SubtitleDTO "
>" #
result$ *
=+ ,
new- 0

NullResult1 ;
<; <
SubtitleDTO< G
>G H
(H I
)I J
;J K
result 
. 
	WithValue 
( 
_mapper $
.$ %
Map% (
<( )
SubtitleDTO) 4
>4 5
(5 6
subtitle6 >
)> ?
)? @
;@ A
return   
result   
;   
}!! 	
}"" 
}## î
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Subtitle\GetById\GetSubtitleByIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
GetById3 :
;: ;
public 
record  
GetSubtitleByIdQuery "
(" #
int# &
Id' )
)) *
:+ ,
IRequest- 5
<5 6
Result6 <
<< =
SubtitleDTO= H
>H I
>I J
;J KÔ
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Subtitle\GetById\GetSubtitleByIdHandler.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
MediatR		  
.		  !
AdditionalContent		! 2
.		2 3
Subtitle		3 ;
.		; <
GetById		< C
;		C D
public 
class "
GetSubtitleByIdHandler #
:$ %
IRequestHandler& 5
<5 6 
GetSubtitleByIdQuery6 J
,J K
ResultL R
<R S
SubtitleDTOS ^
>^ _
>_ `
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
"
GetSubtitleByIdHandler !
(! "
IRepositoryWrapper" 4
repositoryWrapper5 F
,F G
IMapperH O
mapperP V
,V W
ILoggerServiceX f
loggerg m
)m n
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
SubtitleDTO (
>( )
>) *
Handle+ 1
(1 2 
GetSubtitleByIdQuery2 F
requestG N
,N O
CancellationTokenP a
cancellationTokenb s
)s t
{ 
var 
subtitle 
= 
await 
_repositoryWrapper /
./ 0
SubtitleRepository0 B
.B C"
GetFirstOrDefaultAsyncC Y
(Y Z
fZ [
=>\ ^
f_ `
.` a
Ida c
==d f
requestg n
.n o
Ido q
)q r
;r s
if 

( 
subtitle 
is 
null 
) 
{ 	
string 
errorMsg 
= 
$"  
$str  N
{N O
requestO V
.V W
IdW Y
}Y Z
"Z [
;[ \
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return   
Result   
.   
Fail   
(   
new   "
Error  # (
(  ( )
errorMsg  ) 1
)  1 2
)  2 3
;  3 4
}!! 	
return## 
Result## 
.## 
Ok## 
(## 
_mapper##  
.##  !
Map##! $
<##$ %
SubtitleDTO##% 0
>##0 1
(##1 2
subtitle##2 :
)##: ;
)##; <
;##< =
}$$ 
}%% Ø
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Subtitle\GetAll\GetAllSubtitlesQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Subtitle3 ;
.; <
GetAll< B
;B C
public 
record  
GetAllSubtitlesQuery "
:# $
IRequest% -
<- .
Result. 4
<4 5
IEnumerable5 @
<@ A
SubtitleDTOA L
>L M
>M N
>N O
;O PΩ
úD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Subtitle\GetAll\GetAllSubtitlesHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3
Subtitle3 ;
.; <
GetAll< B
;B C
public

 
class

 "
GetAllSubtitlesHandler

 #
:

$ %
IRequestHandler

& 5
<

5 6 
GetAllSubtitlesQuery

6 J
,

J K
Result

L R
<

R S
IEnumerable

S ^
<

^ _
SubtitleDTO

_ j
>

j k
>

k l
>

l m
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
"
GetAllSubtitlesHandler !
(! "
IRepositoryWrapper" 4
repositoryWrapper5 F
,F G
IMapperH O
mapperP V
,V W
ILoggerServiceX f
loggerg m
)m n
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )
SubtitleDTO) 4
>4 5
>5 6
>6 7
Handle8 >
(> ? 
GetAllSubtitlesQuery? S
requestT [
,[ \
CancellationToken] n
cancellationToken	o Ä
)
Ä Å
{ 
var 
	subtitles 
= 
await 
_repositoryWrapper 0
.0 1
SubtitleRepository1 C
.C D
GetAllAsyncD O
(O P
)P Q
;Q R
if 

( 
	subtitles 
is 
null 
) 
{ 	
const 
string 
errorMsg !
=" #
$"$ &
$str& ?
"? @
;@ A
_logger 
. 
LogError 
( 
request $
,$ %
errorMsg& .
). /
;/ 0
return   
Result   
.   
Fail   
(   
new   "
Error  # (
(  ( )
errorMsg  ) 1
)  1 2
)  2 3
;  3 4
}!! 	
return## 
Result## 
.## 
Ok## 
(## 
_mapper##  
.##  !
Map##! $
<##$ %
IEnumerable##% 0
<##0 1
SubtitleDTO##1 <
>##< =
>##= >
(##> ?
	subtitles##? H
)##H I
)##I J
;##J K
}$$ 
}%% ß
ûD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\Update\UpdateCoordinateHanler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
Update> D
;D E
public 
class #
UpdateCoordinateHandler $
:% &
IRequestHandler' 6
<6 7#
UpdateCoordinateCommand7 N
,N O
ResultP V
<V W
UnitW [
>[ \
>\ ]
{		 
private

 
readonly

 
IMapper

 
_mapper

 $
;

$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
public 
#
UpdateCoordinateHandler "
(" #
IRepositoryWrapper# 5
repositoryWrapper6 G
,G H
IMapperI P
mapperQ W
)W X
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +#
UpdateCoordinateCommand+ B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
{ 
var  
streetcodeCoordinate  
=! "
_mapper# *
.* +
Map+ .
<. /
DAL/ 2
.2 3
Entities3 ;
.; <
AdditionalContent< M
.M N
CoordinatesN Y
.Y Z
TypesZ _
._ ` 
StreetcodeCoordinate` t
>t u
(u v
requestv }
.} ~!
StreetcodeCoordinate	~ í
)
í ì
;
ì î
if 

(  
streetcodeCoordinate  
is! #
null$ (
)( )
{ 	
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
$str) V
)V W
)W X
;X Y
} 	
_repositoryWrapper 
. *
StreetcodeCoordinateRepository 9
.9 :
Update: @
(@ A 
streetcodeCoordinateA U
)U V
;V W
var 
resultIsSuccess 
= 
await #
_repositoryWrapper$ 6
.6 7
SaveChangesAsync7 G
(G H
)H I
>J K
$numL M
;M N
return 
resultIsSuccess 
?  
Result! '
.' (
Ok( *
(* +
Unit+ /
./ 0
Value0 5
)5 6
:7 8
Result9 ?
.? @
Fail@ D
(D E
newE H
ErrorI N
(N O
$strO x
)x y
)y z
;z {
}   
}!! ‡
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\Update\UpdateCoordinateCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
Update> D
;D E
public 
record #
UpdateCoordinateCommand %
(% &#
StreetcodeCoordinateDTO& = 
StreetcodeCoordinate> R
)R S
:T U
IRequestV ^
<^ _
Result_ e
<e f
Unitf j
>j k
>k l
;l m≈
¥D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\GetByStreetcodeId\GetCoordinatesByStreetcodeIdQuery.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
GetByStreetcodeId> O
{ 
public 

record -
!GetCoordinatesByStreetcodeIdQuery 3
(3 4
int4 7
StreetcodeId8 D
)D E
:F G
IRequestH P
<P Q
ResultQ W
<W X
IEnumerableX c
<c d#
StreetcodeCoordinateDTOd {
>{ |
>| }
>} ~
;~ 
} Å#
∂D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\GetByStreetcodeId\GetCoordinatesByStreetcodeIdHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
GetByStreetcodeId> O
;O P
public

 
class

 /
#GetCoordinatesByStreetcodeIdHandler

 0
:

1 2
IRequestHandler

3 B
<

B C-
!GetCoordinatesByStreetcodeIdQuery

C d
,

d e
Result

f l
<

l m
IEnumerable

m x
<

x y$
StreetcodeCoordinateDTO	

y ê
>


ê ë
>


ë í
>


í ì
{ 
private 
readonly 
IMapper 
_mapper $
;$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
private 
readonly 
ILoggerService #
_logger$ +
;+ ,
public 
/
#GetCoordinatesByStreetcodeIdHandler .
(. /
IRepositoryWrapper/ A
repositoryWrapperB S
,S T
IMapperU \
mapper] c
,c d
ILoggerServicee s
loggert z
)z {
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 
Result 
< 
IEnumerable (
<( )#
StreetcodeCoordinateDTO) @
>@ A
>A B
>B C
HandleD J
(J K-
!GetCoordinatesByStreetcodeIdQueryK l
requestm t
,t u
CancellationToken	v á
cancellationToken
à ô
)
ô ö
{ 
if 

( 
( 
await 
_repositoryWrapper %
.% & 
StreetcodeRepository& :
.: ;"
GetFirstOrDefaultAsync; Q
(Q R
sR S
=>T V
sW X
.X Y
IdY [
==\ ^
request_ f
.f g
StreetcodeIdg s
)s t
)t u
isv x
nully }
)} ~
{ 	
return 
Result 
. 
Fail 
( 
new 
Error 
( 
$" 
$str J
{J K
requestK R
.R S
StreetcodeIdS _
}_ `
$str	` á
"
á à
)
à â
)
â ä
;
ä ã
} 	
var 
coordinates 
= 
await 
_repositoryWrapper  2
.2 3*
StreetcodeCoordinateRepository3 Q
.   
GetAllAsync   
(   
c   
=>   
c   
.    
StreetcodeId    ,
==  - /
request  0 7
.  7 8
StreetcodeId  8 D
)  D E
;  E F
if"" 

("" 
coordinates"" 
is"" 
null"" 
)""  
{## 	
string$$ 
errorMsg$$ 
=$$ 
$"$$  
$str$$  N
{$$N O
request$$O V
.$$V W
StreetcodeId$$W c
}$$c d
"$$d e
;$$e f
_logger%% 
.%% 
LogError%% 
(%% 
request%% $
,%%$ %
errorMsg%%& .
)%%. /
;%%/ 0
return&& 
Result&& 
.&& 
Fail&& 
(&& 
new&& "
Error&&# (
(&&( )
errorMsg&&) 1
)&&1 2
)&&2 3
;&&3 4
}'' 	
return)) 
Result)) 
.)) 
Ok)) 
()) 
_mapper))  
.))  !
Map))! $
<))$ %
IEnumerable))% 0
<))0 1#
StreetcodeCoordinateDTO))1 H
>))H I
>))I J
())J K
coordinates))K V
)))V W
)))W X
;))X Y
}** 
}++ Ù
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\Delete\DeleteCoordinateHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
Delete> D
;D E
public 
class #
DeleteCoordinateHandler $
:% &
IRequestHandler' 6
<6 7#
DeleteCoordinateCommand7 N
,N O
ResultP V
<V W
UnitW [
>[ \
>\ ]
{ 
private		 
readonly		 
IRepositoryWrapper		 '
_repositoryWrapper		( :
;		: ;
public 
#
DeleteCoordinateHandler "
(" #
IRepositoryWrapper# 5
repositoryWrapper6 G
)G H
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +#
DeleteCoordinateCommand+ B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
{ 
var  
streetcodeCoordinate  
=! "
await# (
_repositoryWrapper) ;
.; <*
StreetcodeCoordinateRepository< Z
.Z ["
GetFirstOrDefaultAsync[ q
(q r
fr s
=>t v
fw x
.x y
Idy {
==| ~
request	 Ü
.
Ü á
Id
á â
)
â ä
;
ä ã
if 

(  
streetcodeCoordinate  
is! #
null$ (
)( )
{ 	
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
$") +
$str+ c
{c d
requestd k
.k l
Idl n
}n o
"o p
)p q
)q r
;r s
} 	
_repositoryWrapper 
. *
StreetcodeCoordinateRepository 9
.9 :
Delete: @
(@ A 
streetcodeCoordinateA U
)U V
;V W
var 
resultIsSuccess 
= 
await #
_repositoryWrapper$ 6
.6 7
SaveChangesAsync7 G
(G H
)H I
>J K
$numL M
;M N
return 
resultIsSuccess 
?  
Result! '
.' (
Ok( *
(* +
Unit+ /
./ 0
Value0 5
)5 6
:7 8
Result9 ?
.? @
Fail@ D
(D E
newE H
ErrorI N
(N O
$strO n
)n o
)o p
;p q
} 
} ∫
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\Delete\DeleteCoordinateCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
Delete> D
;D E
public 
record #
DeleteCoordinateCommand %
(% &
int& )
Id* ,
), -
:. /
IRequest0 8
<8 9
Result9 ?
<? @
Unit@ D
>D E
>E F
;F G¿
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\Create\CreateCoordinateHandler.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
Create> D
;D E
public 
class #
CreateCoordinateHandler $
:% &
IRequestHandler' 6
<6 7#
CreateCoordinateCommand7 N
,N O
ResultP V
<V W
UnitW [
>[ \
>\ ]
{		 
private

 
readonly

 
IMapper

 
_mapper

 $
;

$ %
private 
readonly 
IRepositoryWrapper '
_repositoryWrapper( :
;: ;
public 
#
CreateCoordinateHandler "
(" #
IRepositoryWrapper# 5
repositoryWrapper6 G
,G H
IMapperI P
mapperQ W
)W X
{ 
_repositoryWrapper 
= 
repositoryWrapper .
;. /
_mapper 
= 
mapper 
; 
} 
public 

async 
Task 
< 
Result 
< 
Unit !
>! "
>" #
Handle$ *
(* +#
CreateCoordinateCommand+ B
requestC J
,J K
CancellationTokenL ]
cancellationToken^ o
)o p
{ 
var  
streetcodeCoordinate  
=! "
_mapper# *
.* +
Map+ .
<. /
DAL/ 2
.2 3
Entities3 ;
.; <
AdditionalContent< M
.M N
CoordinatesN Y
.Y Z
TypesZ _
._ ` 
StreetcodeCoordinate` t
>t u
(u v
requestv }
.} ~!
StreetcodeCoordinate	~ í
)
í ì
;
ì î
if 

(  
streetcodeCoordinate  
is! #
null$ (
)( )
{ 	
return 
Result 
. 
Fail 
( 
new "
Error# (
(( )
$str) V
)V W
)W X
;X Y
} 	
await 
_repositoryWrapper  
.  !*
StreetcodeCoordinateRepository! ?
.? @
CreateAsync@ K
(K L 
streetcodeCoordinateL `
)` a
;a b
var 
resultIsSuccess 
= 
await #
_repositoryWrapper$ 6
.6 7
SaveChangesAsync7 G
(G H
)H I
>J K
$numL M
;M N
return 
resultIsSuccess 
?  
Result! '
.' (
Ok( *
(* +
Unit+ /
./ 0
Value0 5
)5 6
:7 8
Result9 ?
.? @
Fail@ D
(D E
newE H
ErrorI N
(N O
$strO x
)x y
)y z
;z {
}   
}!! ‡
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\MediatR\AdditionalContent\Coordinate\Create\CreateCoordinateCommand.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
MediatR  
.  !
AdditionalContent! 2
.2 3

Coordinate3 =
.= >
Create> D
;D E
public 
record #
CreateCoordinateCommand %
(% &#
StreetcodeCoordinateDTO& = 
StreetcodeCoordinate> R
)R S
:T U
IRequestV ^
<^ _
Result_ e
<e f
Unitf j
>j k
>k l
;l m≥	
uD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Users\UserProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Users! &
{ 
public 

class 
UserProfile 
: 
Profile &
{ 
public		 
UserProfile		 
(		 
)		 
{

 	
	CreateMap 
< 
User 
, 
UserLoginDTO (
>( )
() *
)* +
.+ ,

ReverseMap, 6
(6 7
)7 8
;8 9
	CreateMap 
< 
UserDTO 
, 
UserLoginDTO +
>+ ,
(, -
)- .
.. /

ReverseMap/ 9
(9 :
): ;
;; <
	CreateMap 
< 
User 
, 
UserDTO #
># $
($ %
)% &
.& '

ReverseMap' 1
(1 2
)2 3
;3 4
} 	
} 
} û
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Transactions\TransactionLinkProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Transactions! -
;- .
public 
class "
TransactionLinkProfile #
:$ %
Profile& -
{ 
public		 
"
TransactionLinkProfile		 !
(		! "
)		" #
{

 
	CreateMap 
< 
TransactionLink !
,! "
TransactLinkDTO# 2
>2 3
(3 4
)4 5
. 

ReverseMap 
( 
) 
; 
} 
} 
{D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Toponyms\ToponymProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Toponyms! )
;) *
public 
class 
ToponymProfile 
: 
Profile %
{ 
public		 

ToponymProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Toponym 
, 

ToponymDTO %
>% &
(& '
)' (
.( )

ReverseMap) 3
(3 4
)4 5
;5 6
} 
} Ω
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Toponyms\StreetcodeToponymProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Toponyms! )
{ 
public 
class	 $
StreetcodeToponymProfile '
:( )
Profile* 1
{ 
public		 $
StreetcodeToponymProfile			 !
(		! "
)		" #
{

 
} 
} 
} °
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Timeline\TimelineItemProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Timeline! )
;) *
public 
class 
TimelineItemProfile  
:! "
Profile# *
{ 
public		 

TimelineItemProfile		 
(		 
)		  
{

 
	CreateMap 
< 
TimelineItem 
, 
TimelineItemDTO  /
>/ 0
(0 1
)1 2
.2 3

ReverseMap3 =
(= >
)> ?
;? @
	CreateMap 
< 
TimelineItem 
, 
TimelineItemDTO  /
>/ 0
(0 1
)1 2
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
HistoricalContexts$ 6
,6 7
opt8 ;
=>< >
opt? B
.B C
MapFromC J
(J K
xK L
=>M O
xP Q
.Q R&
HistoricalContextTimelinesR l
. 
Select 
( 
x 
=> 
new   
HistoricalContextDTO! 5
{ 
Id 
= 
x 
. 
HistoricalContextId .
,. /
Title 
= 
x 
. 
HistoricalContext /
./ 0
Title0 5
} 
) 
. 
ToList 
( 
) 
) 
) 
; 
} 
} £
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Timeline\HistoricalContextProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Timeline! )
;) *
public 
class $
HistoricalContextProfile %
:& '
Profile( /
{ 
public		 
$
HistoricalContextProfile		 #
(		# $
)		$ %
{

 
	CreateMap 
< 
HistoricalContext #
,# $ 
HistoricalContextDTO% 9
>9 :
(: ;
); <
.< =

ReverseMap= G
(G H
)H I
;I J
} 
} ˆ
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Team\TeamProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Team! %
{ 
public 

class 
TeamProfile 
: 
Profile &
{ 
public		 
TeamProfile		 
(		 
)		 
{

 	
	CreateMap 
< 

TeamMember  
,  !
TeamMemberDTO" /
>/ 0
(0 1
)1 2
.2 3

ReverseMap3 =
(= >
)> ?
;? @
} 	
} 
} ä
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Team\TeamLinkProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Team! %
{ 
public 

class 
TeamLinkProfile  
:! "
Profile# *
{ 
public		 
TeamLinkProfile		 
(		 
)		  
{

 	
	CreateMap 
< 
TeamMemberLink $
,$ %
TeamMemberLinkDTO& 7
>7 8
(8 9
)9 :
.: ;

ReverseMap; E
(E F
)F G
;G H
} 	
} 
} ˇ
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Team\PositionProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Team! %
{ 
public 

class 
PositionProfile  
:! "
Profile# *
{ 
public		 
PositionProfile		 
(		 
)		  
{

 	
	CreateMap 
< 
	Positions 
,  
PositionDTO! ,
>, -
(- .
). /
./ 0

ReverseMap0 :
(: ;
); <
;< =
} 	
} 
} ˜
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\Types\PersonStreetCodeProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
.+ ,
Types, 1
;1 2
public		 
class		 #
PersonStreetcodeProfile		 $
:		% &
Profile		' .
{

 
public 
#
PersonStreetcodeProfile "
(" #
)# $
{ 
	CreateMap 
< 
PersonStreetcode "
," #
PersonStreetcodeDTO$ 7
>7 8
(8 9
)9 :
. 
IncludeBase 
< 
StreetcodeContent *
,* +
StreetcodeDTO, 9
>9 :
(: ;
); <
.< =

ReverseMap= G
(G H
)H I
;I J
} 
} Ú
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\Types\EventStreetCodeProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
.+ ,
Types, 1
;1 2
public		 
class		 "
EventStreetcodeProfile		 #
:		$ %
Profile		& -
{

 
public 
"
EventStreetcodeProfile !
(! "
)" #
{ 
	CreateMap 
< 
EventStreetcode !
,! "
EventStreetcodeDTO# 5
>5 6
(6 7
)7 8
. 
IncludeBase 
< 
StreetcodeContent *
,* +
StreetcodeDTO, 9
>9 :
(: ;
); <
.< =

ReverseMap= G
(G H
)H I
;I J
} 
} ˝
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\TextContent\TextProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
.+ ,
TextContent, 7
;7 8
public 
class 
TextProfile 
: 
Profile "
{ 
public		 

TextProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Text 
, 
TextDTO 
>  
(  !
)! "
." #

ReverseMap# -
(- .
). /
;/ 0
	CreateMap 
< 
TextCreateDTO 
,  
Text! %
>% &
(& '
)' (
.( )

ReverseMap) 3
(3 4
)4 5
;5 6
} 
} ö
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\TextContent\TermProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
.+ ,
TextContent, 7
;7 8
public 
class 
TermProfile 
: 
Profile "
{ 
public		 

TermProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Term 
, 
TermDTO 
>  
(  !
)! "
." #

ReverseMap# -
(- .
). /
;/ 0
} 
} Ω
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\TextContent\RelatedTermProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
.+ ,
TextContent, 7
;7 8
public 
class 
RelatedTermProfile 
:  !
Profile" )
{ 
public		 

RelatedTermProfile		 
(		 
)		 
{

 
	CreateMap 
< 
RelatedTerm 
, 
RelatedTermDTO -
>- .
(. /
)/ 0
.0 1

ReverseMap1 ;
(; <
)< =
;= >
} 
} É
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\TextContent\FactProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
.+ ,
TextContent, 7
;7 8
public 
class 
FactProfile 
: 
Profile "
{ 
public		 

FactProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Fact 
, 
FactDto 
>  
(  !
)! "
." #

ReverseMap# -
(- .
). /
;/ 0
	CreateMap 
< 
Fact 
, 
FactUpdateCreateDto +
>+ ,
(, -
)- .
.. /

ReverseMap/ 9
(9 :
): ;
;; <
} 
} ¶
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\StreetcodeProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
;+ ,
public		 
class		 
StreetcodeProfile		 
:		  
Profile		! (
{

 
public 

StreetcodeProfile 
( 
) 
{ 
	CreateMap 
< 
StreetcodeContent #
,# $
StreetcodeDTO% 2
>2 3
(3 4
)4 5
. 
	ForMember 
( 
x 
=> 
x 
. 
StreetcodeType ,
,, -
conf. 2
=>3 5
conf6 :
.: ;
MapFrom; B
(B C
sC D
=>E G
GetStreetcodeTypeH Y
(Y Z
sZ [
)[ \
)\ ]
)] ^
. 

ReverseMap 
( 
) 
; 
	CreateMap 
< 
StreetcodeContent #
,# $
StreetcodeShortDTO% 7
>7 8
(8 9
)9 :
.: ;

ReverseMap; E
(E F
)F G
;G H
	CreateMap 
< 
StreetcodeContent #
,# $!
StreetcodeMainPageDTO% :
>: ;
(; <
)< =
. 
ForPath 
( 
dto 
=> 
dto  
.  !
Text! %
,% &
conf' +
=>, .
conf/ 3
. 
MapFrom 
( 
e 
=> 
e 
.  
Text  $
.$ %
Title% *
)* +
)+ ,
. 
ForPath 
( 
dto 
=> 
dto 
.  
ImageId  '
,' (
conf) -
=>. 0
conf1 5
. 
MapFrom 
( 
e 
=> 
e 
.  
Images  &
.& '
Select' -
(- .
i. /
=>0 2
i3 4
.4 5
Id5 7
)7 8
.8 9
LastOrDefault9 F
(F G
)G H
)H I
)I J
;J K
} 
private 
StreetcodeType 
GetStreetcodeType ,
(, -
StreetcodeContent- >

streetcode? I
)I J
{ 
if 

(
 

streetcode 
is 
EventStreetcode (
)( )
{ 	
return 
StreetcodeType !
.! "
Event" '
;' (
} 	
return 
StreetcodeType 
. 
Person $
;$ %
}   
}!! è
ÉD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Streetcode\RelatedFigureProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !

Streetcode! +
;+ ,
public 
class  
RelatedFigureProfile !
:" #
Profile$ +
{		 
public

 
 
RelatedFigureProfile

 
(

  
)

  !
{ 
	CreateMap 
< 
EventStreetcode !
,! "
RelatedFigureDTO# 3
>3 4
(4 5
)5 6
. 
ForPath 
( 
dto 
=> 
dto 
.  
Title  %
,% &
conf' +
=>, .
conf/ 3
. 
MapFrom 
( 
e 
=> 
e 
.  
Title  %
)% &
)& '
. 
ForPath 
( 
dto 
=> 
dto 
.  
Url  #
,# $
conf% )
=>* ,
conf- 1
. 
MapFrom 
( 
e 
=> 
e 
.  
TransliterationUrl  2
)2 3
)3 4
. 
ForPath 
( 
dto 
=> 
dto 
.  
ImageId  '
,' (
conf) -
=>. 0
conf1 5
. 
MapFrom 
( 
e 
=> 
e 
.  
Images  &
.& '
Select' -
(- .
i. /
=>0 2
i3 4
.4 5
Id5 7
)7 8
.8 9
LastOrDefault9 F
(F G
)G H
)H I
)I J
;J K
	CreateMap 
< 
PersonStreetcode "
," #
RelatedFigureDTO$ 4
>4 5
(5 6
)6 7
. 
ForPath 
( 
dto 
=> 
dto 
.  
Url  #
,# $
conf% )
=>* ,
conf- 1
. 
MapFrom 
( 
e 
=> 
e 
.  
TransliterationUrl  2
)2 3
)3 4
. 
ForPath 
( 
dto 
=> 
dto 
.  
ImageId  '
,' (
conf) -
=>. 0
conf1 5
. 
MapFrom 
( 
e 
=> 
e 
.  
Images  &
.& '
Select' -
(- .
i. /
=>0 2
i3 4
.4 5
Id5 7
)7 8
.8 9
LastOrDefault9 F
(F G
)G H
)H I
)I J
;J K
	CreateMap 
< 
StreetcodeContent #
,# $!
RelatedFigureShortDTO% :
>: ;
(; <
)< =
;= >
} 
} ‹
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Sources\StreetcodeCategoryContentProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Sources! (
{ 
internal 
class ,
 StreetcodeCategoryContentProfile 3
:4 5
Profile6 =
{ 
public		 ,
 StreetcodeCategoryContentProfile		 /
(		/ 0
)		0 1
{

 	
	CreateMap 
< %
StreetcodeCategoryContent /
,/ 0(
StreetcodeCategoryContentDTO1 M
>M N
(N O
)O P
. 

ReverseMap 
( 
) 
; 
} 	
} 
} π
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Sources\SourceLinkSubCategoryProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Sources! (
;( )
public 
class (
SourceLinkSubCategoryProfile )
:* +
Profile, 3
{ 
public		 
(
SourceLinkSubCategoryProfile		 '
(		' (
)		( )
{

 
	CreateMap 
< $
CategoryContentCreateDTO *
,* +%
StreetcodeCategoryContent, E
>E F
(F G
)G H
.H I

ReverseMapI S
(S T
)T U
;U V
} 
} Û&
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Sources\SourceLinkCategoryProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Sources! (
;( )
public 
class %
SourceLinkCategoryProfile &
:' (
Profile) 0
{		 
public

 
%
SourceLinkCategoryProfile

 $
(

$ %
)

% &
{ 
	CreateMap 
< 
SourceLinkCategory $
,$ %!
SourceLinkCategoryDTO& ;
>; <
(< =
)= >
. 
	ForMember 
( 
dto 
=> 
dto !
.! "
Image" '
,' (
c) *
=>+ -
c. /
./ 0
MapFrom0 7
(7 8
b8 9
=>: <
b= >
.> ?
Image? D
)D E
)E F
. 

ReverseMap 
( 
) 
; 
	CreateMap 
< 
SourceLinkCategory $
,$ %
CategoryWithNameDTO& 9
>9 :
(: ;
); <
.< =

ReverseMap= G
(G H
)H I
;I J
	CreateMap 
< 
SourceLinkCategory $
,$ %
ImageDTO& .
>. /
(/ 0
)0 1
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
MimeType$ ,
,, -
opt. 1
=>2 4
opt5 8
.8 9
MapFrom9 @
(@ A
srcA D
=>E G
srcH K
.K L
ImageL Q
.Q R
MimeTypeR Z
)Z [
)[ \
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
BlobName$ ,
,, -
opt. 1
=>2 4
opt5 8
.8 9
MapFrom9 @
(@ A
srcA D
=>E G
srcH K
.K L
ImageL Q
.Q R
BlobNameR Z
)Z [
)[ \
;\ ]
	CreateMap 
< !
SourceLinkCategoryDTO '
,' (
SourceLinkCategory) ;
>; <
(< =
)= >
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Id$ &
,& '
opt( +
=>, .
opt/ 2
.2 3
MapFrom3 :
(: ;
src; >
=>? A
srcB E
.E F
IdF H
)H I
)I J
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Image$ )
,) *
opt+ .
=>/ 1
opt2 5
.5 6
MapFrom6 =
(= >
src> A
=>B D
srcE H
.H I
ImageI N
)N O
)O P
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Title$ )
,) *
opt+ .
=>/ 1
opt2 5
.5 6
MapFrom6 =
(= >
dto> A
=>B D
dtoE H
.H I
TitleI N
)N O
)O P
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Streetcodes$ /
,/ 0
opt1 4
=>5 7
opt8 ;
.; <
Ignore< B
(B C
)C D
)D E
. 
	ForMember 
( 
dest 
=> 
dest #
.# $&
StreetcodeCategoryContents$ >
,> ?
opt@ C
=>D F
optG J
.J K
IgnoreK Q
(Q R
)R S
)S T
. 
ForPath 
( 
dest 
=> 
dest !
.! "
Image" '
!' (
.( )
Streetcodes) 4
,4 5
c6 7
=>8 :
c; <
.< =
Ignore= C
(C D
)D E
)E F
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
ImageId$ +
,+ ,
opt- 0
=>1 3
opt4 7
.7 8
MapFrom8 ?
(? @
dto@ C
=>D F
dtoG J
.J K
ImageIdK R
)R S
)S T
;T U
} 
} ˙
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Payment\PaymentProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Payment! (
;( )
public		 
class		 
PaymentProfile		 
:		 
Profile		 %
{

 
public 

PaymentProfile 
( 
) 
{ 
	CreateMap 
< 
InvoiceInfo 
, 
PaymentResponseDTO 1
>1 2
(2 3
)3 4
.4 5

ReverseMap5 ?
(? @
)@ A
;A B
} 
} Ò

ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Partners\PartnerSourceLinkProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Partners! )
;) *
public 
class $
PartnerSourceLinkProfile %
:& '
Profile( /
{		 
public

 
$
PartnerSourceLinkProfile

 #
(

# $
)

$ %
{ 
	CreateMap 
< 
PartnerSourceLink #
,# $ 
PartnerSourceLinkDTO% 9
>9 :
(: ;
); <
. 
ForPath 
( 
dto 
=> 
dto 
.  
	TargetUrl  )
.) *
Href* .
,. /
conf0 4
=>5 7
conf8 <
.< =
MapFrom= D
(D E
olE G
=>H J
olK M
.M N
	TargetUrlN W
)W X
)X Y
;Y Z
	CreateMap 
< 
PartnerSourceLink #
,# $&
CreatePartnerSourceLinkDTO% ?
>? @
(@ A
)A B
.B C

ReverseMapC M
(M N
)N O
;O P
} 
} ®
{D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Partners\PartnerProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Partners! )
;) *
public 
class 
PartnerProfile 
: 
Profile %
{ 
public		 

PartnerProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Partner 
, 

PartnerDTO %
>% &
(& '
)' (
. 
ForPath 
( 
dto 
=> 
dto 
.  
	TargetUrl  )
.) *
Title* /
,/ 0
conf1 5
=>6 8
conf9 =
.= >
MapFrom> E
(E F
olF H
=>I K
olL N
.N O
UrlTitleO W
)W X
)X Y
. 
ForPath 
( 
dto 
=> 
dto 
.  
	TargetUrl  )
.) *
Href* .
,. /
conf0 4
=>5 7
conf8 <
.< =
MapFrom= D
(D E
olE G
=>H J
olK M
.M N
	TargetUrlN W
)W X
)X Y
;Y Z
	CreateMap 
< 
Partner 
, 
CreatePartnerDTO +
>+ ,
(, -
)- .
.. /

ReverseMap/ 9
(9 :
): ;
;; <
	CreateMap 
< 
Partner 
, 
PartnerShortDTO *
>* +
(+ ,
), -
.- .

ReverseMap. 8
(8 9
)9 :
;: ;
} 
} Ï
uD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Newss\NewsProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Newss! &
{ 
public 

class 
NewsProfile 
: 
Profile &
{ 
public		 
NewsProfile		 
(		 
)		 
{

 	
	CreateMap 
< 
News 
, 
NewsDTO #
># $
($ %
)% &
.& '

ReverseMap' 1
(1 2
)2 3
;3 4
} 	
} 
} õ
vD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Media\VideoProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Media! &
;& '
public 
class 
VideoProfile 
: 
Profile #
{ 
public		 

VideoProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Video 
, 
VideoDTO !
>! "
(" #
)# $
;$ %
} 
} ≥
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Media\Images\StreetcodeArtProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Media! &
.& '
Images' -
;- .
public 
class  
StreetcodeArtProfile !
:" #
Profile$ +
{		 
public

 
 
StreetcodeArtProfile

 
(

  
)

  !
{ 
	CreateMap 
< 
StreetcodeArt 
,  
StreetcodeArtDTO! 1
>1 2
(2 3
)3 4
.4 5

ReverseMap5 ?
(? @
)@ A
;A B
} 
} ≤
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Media\Images\ImageProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Media! &
.& '
Images' -
;- .
public 
class 
ImageProfile 
: 
Profile #
{		 
public

 

ImageProfile

 
(

 
)

 
{ 
	CreateMap 
< 
Image 
, 
ImageDTO !
>! "
(" #
)# $
.$ %

ReverseMap% /
(/ 0
)0 1
;1 2
	CreateMap 
< "
ImageFileBaseCreateDTO (
,( )
Image* /
>/ 0
(0 1
)1 2
;2 3
} 
} ø
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Media\Images\ImageDetailsProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Media! &
.& '
Images' -
{ 
public 

class 
ImageDetailsProfile $
:% &
Profile' .
{ 
public		 
ImageDetailsProfile		 "
(		" #
)		# $
{

 	
	CreateMap 
< 
ImageDetails "
," #
ImageDetailsDto$ 3
>3 4
(4 5
)5 6
.6 7

ReverseMap7 A
(A B
)B C
;C D
} 	
} 
} Ä
{D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Media\Images\ArtProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Media! &
.& '
Images' -
;- .
public 
class 

ArtProfile 
: 
Profile !
{		 
public

 


ArtProfile

 
(

 
)

 
{ 
	CreateMap 
< 
Art 
, 
ArtDTO 
> 
( 
)  
.  !

ReverseMap! +
(+ ,
), -
;- .
} 
} ¢
vD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Media\AudioProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Media! &
;& '
public 
class 
AudioProfile 
: 
Profile #
{ 
public		 

AudioProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Audio 
, 
AudioDTO !
>! "
(" #
)# $
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
MimeType$ ,
,, -
opt. 1
=>2 4
opt5 8
.8 9
MapFrom9 @
(@ A
srcA D
=>E G
srcH K
.K L
MimeTypeL T
)T U
)U V
;V W
	CreateMap 
< "
AudioFileBaseCreateDTO (
,( )
Audio* /
>/ 0
(0 1
)1 2
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
Title$ )
,) *
opt+ .
=>/ 1
opt2 5
.5 6
MapFrom6 =
(= >
src> A
=>B D
srcE H
.H I
TitleI N
)N O
)O P
. 
	ForMember 
( 
dest 
=> 
dest #
.# $
MimeType$ ,
,, -
opt. 1
=>2 4
opt5 8
.8 9
MapFrom9 @
(@ A
srcA D
=>E G
srcH K
.K L
MimeTypeL T
)T U
)U V
;V W
} 
} ı
|D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\Feedback\ResponseProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
Feedback! )
;) *
public 
class 
ResponseProfile 
: 
Profile &
{ 
public		 

ResponseProfile		 
(		 
)		 
{

 
	CreateMap 
< 
Response 
, 
ResponseDTO '
>' (
(( )
)) *
.* +

ReverseMap+ 5
(5 6
)6 7
;7 8
} 
} Ò
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\AdditionalContent\TagProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
AdditionalContent! 2
;2 3
public 
class 

TagProfile 
: 
Profile !
{		 
public

 


TagProfile

 
(

 
)

 
{ 
	CreateMap 
< 
Tag 
, 
TagDTO 
> 
( 
)  
.  !
	ForMember! *
(* +
x+ ,
=>- /
x0 1
.1 2
Streetcodes2 =
,= >
conf? C
=>D F
confG K
.K L
IgnoreL R
(R S
)S T
)T U
;U V
	CreateMap 
< 
Tag 
, 
StreetcodeTagDTO '
>' (
(( )
)) *
.* +

ReverseMap+ 5
(5 6
)6 7
;7 8
	CreateMap 
< 
StreetcodeTagIndex $
,$ %
StreetcodeTagDTO& 6
>6 7
(7 8
)8 9
. 
	ForMember 
( 
x 
=> 
x 
. 
Id  
,  !
conf" &
=>' )
conf* .
.. /
MapFrom/ 6
(6 7
ti7 9
=>: <
ti= ?
.? @
TagId@ E
)E F
)F G
. 
	ForMember 
( 
x 
=> 
x 
. 
Title #
,# $
conf% )
=>* ,
conf- 1
.1 2
MapFrom2 9
(9 :
ti: <
=>= ?
ti@ B
.B C
TagC F
.F G
TitleG L
??M O
$strP R
)R S
)S T
;T U
} 
} à
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\AdditionalContent\SubtitleProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
AdditionalContent! 2
;2 3
public 
class 
SubtitleProfile 
: 
Profile &
{ 
public		 	
SubtitleProfile		
 
(		 
)		 
{

 
	CreateMap 
< 
Subtitle 
, 
SubtitleDTO '
>' (
(( )
)) *
.* +

ReverseMap+ 5
(5 6
)6 7
;7 8
} 
} È
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\AdditionalContent\Coordinates\ToponymCoordinateProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
AdditionalContent! 2
.2 3
Coordinates3 >
;> ?
public 
class $
ToponymCoordinateProfile %
:& '
Profile( /
{ 
public		 
$
ToponymCoordinateProfile		 #
(		# $
)		$ %
{

 
	CreateMap 
< 
ToponymCoordinate #
,# $ 
ToponymCoordinateDTO% 9
>9 :
(: ;
); <
.< =

ReverseMap= G
(G H
)H I
;I J
} 
} ¯
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Mapping\AdditionalContent\Coordinates\StreetcodeCoordinateProfile.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Mapping  
.  !
AdditionalContent! 2
.2 3
Coordinates3 >
;> ?
public 
class '
StreetcodeCoordinateProfile (
:) *
Profile+ 2
{ 
public		 	'
StreetcodeCoordinateProfile		
 %
(		% &
)		& '
{

 
	CreateMap 
<  
StreetcodeCoordinate &
,& '#
StreetcodeCoordinateDTO( ?
>? @
(@ A
)A B
.B C

ReverseMapC M
(M N
)N O
;O P
} 
} —
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Interfaces\Users\ITokenService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 

Interfaces #
.# $
Users$ )
{ 
public 

	interface 
ITokenService "
{ 
public 
JwtSecurityToken 
GenerateJWTToken  0
(0 1
User1 5
user6 :
): ;
;; <
public		 
JwtSecurityToken		 
RefreshToken		  ,
(		, -
string		- 3
token		4 9
)		9 :
;		: ;
}

 
} º
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Interfaces\Text\ITextService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 

Interfaces #
.# $
Text$ (
{ 
public 

	interface 
ITextService !
{ 
Task 
< 
string 
> 
AddTermsTag  
(  !
string! '
text( ,
), -
;- .
} 
} ÿ
~D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Interfaces\Payment\IPaymentService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 

Interfaces #
.# $
Payment$ +
{ 
public 

	interface 
IPaymentService $
{ 
Task 
< 
InvoiceInfo 
> 
CreateInvoiceAsync ,
(, -
Invoice- 4
invoice5 <
)< =
;= >
} 
}		 Ω
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Interfaces\Logging\ILoggerService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 

Interfaces #
.# $
Logging$ +
{ 
public 

	interface 
ILoggerService #
{ 
void 
LogInformation 
( 
string "
msg# &
)& '
;' (
void 

LogWarning 
( 
string 
msg "
)" #
;# $
void 
LogTrace 
( 
string 
msg  
)  !
;! "
void 
LogDebug 
( 
string 
msg  
)  !
;! "
void		 
LogError		 
(		 
object		 
request		 $
,		$ %
string		& ,
errorMsg		- 5
)		5 6
;		6 7
}

 
} Î
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Interfaces\Instagram\IInstagramService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 

Interfaces #
.# $
	Instagram$ -
{ 
public 

	interface 
IInstagramService &
{ 
Task 
< 
IEnumerable 
< 
InstagramPost &
>& '
>' (
GetPostsAsync) 6
(6 7
)7 8
;8 9
} 
}		 ≈
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Interfaces\Email\IEmailService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 

Interfaces #
.# $
Email$ )
{ 
public 
	interface	 
IEmailService  
{ 
Task 
< 	
bool	 
> 
SendEmailAsync 
( 
Message %
message& -
)- .
;. /
} 
}		 ÿ
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Interfaces\BlobStorage\IBlobService.cs
	namespace 	

Streetcode
 
. 
BLL 
. 

Interfaces #
.# $
BlobStorage$ /
;/ 0
public 
	interface 
IBlobService 
{ 
public 

string 
SaveFileInStorage #
(# $
string$ *
base64+ 1
,1 2
string3 9
name: >
,> ?
string@ F
mimeTypeG O
)O P
;P Q
public 

MemoryStream +
FindFileInStorageAsMemoryStream 7
(7 8
string8 >
name? C
)C D
;D E
public 

string 
UpdateFileInStorage %
(% &
string 
previousBlobName 
,  
string		 
base64Format		 
,		 
string

 
newBlobName

 
,

 
string 
	extension 
) 
; 
public 

string %
FindFileInStorageAsBase64 +
(+ ,
string, 2
name3 7
)7 8
;8 9
public 

void 
DeleteFileInStorage #
(# $
string$ *
name+ /
)/ 0
;0 1
} ø
lD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\Enums\ModelState.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
Enums 
{ 
public 

enum 

ModelState 
{ 
Created 
, 
Deleted 
, 
Updated 
, 
} 
}		 „
rD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Users\UserLoginDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Users "
{ 
public 

class 
UserLoginDTO 
{ 
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public		 
string		 
Login		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
[

 	
Required

	 
]

 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} §
mD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Users\UserDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Users "
{ 
public 

class 
UserDTO 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[		 	
Required			 
]		 
[

 	
	MaxLength

	 
(

 
$num

 
)

 
]

 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Surname 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
] 
[ 	
EmailAddress	 
] 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Login 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
] 
public 
UserRole 
Role 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} √
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Users\RefreshTokenResponce.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Users "
{ 
public		 

class		  
RefreshTokenResponce		 %
{

 
public 
string 
Token 
{ 
get !
;! "
set# &
;& '
}( )
public 
DateTime 
ExpireAt  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} õ
uD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Users\RefreshTokenDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Users "
{ 
public		 

class		 
RefreshTokenDTO		  
{

 
public 
string 
Token 
{ 
get !
;! "
set# &
;& '
}( )
} 
} –
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Users\LoginResultDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Users "
{ 
public		 

class		 
LoginResultDTO		 
{

 
public 
UserDTO 
User 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Token 
{ 
get !
;! "
set# &
;& '
}( )
public 
DateTime 
ExpireAt  
{! "
get# &
;& '
set( +
;+ ,
}- .
} 
} Å
|D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Transactions\TransactLinkDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Transactions )
;) *
public 
class 
TransactLinkDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
? 
Url 
{ 
get 
; 
set !
;! "
}# $
public		 

string		 
?		 
	QrCodeUrl		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 

int

 
StreetcodeId

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
} ó
sD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Toponyms\ToponymDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Toponyms %
;% &
public 
class 

ToponymDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public		 

string		 
Oblast		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 

string

 
?

 
AdminRegionOld

 !
{

" #
get

$ '
;

' (
set

) ,
;

, -
}

. /
public 

string 
? 
AdminRegionNew !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 

string 
? 
Gromada 
{ 
get  
;  !
set" %
;% &
}' (
public 

string 
? 
	Community 
{ 
get "
;" #
set$ '
;' (
}) *
public 

string 

StreetName 
{ 
get "
;" #
set$ '
;' (
}) *
public 

string 

StreetType 
{ 
get "
;" #
set$ '
;' (
}) *
public 
 
ToponymCoordinateDTO 

Coordinate  *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 

IEnumerable 
< 
StreetcodeDTO $
>$ %
Streetcodes& 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
} ˘
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Toponyms\GetAllToponymsResponseDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Toponyms %
;% &
public 
class %
GetAllToponymsResponseDTO &
{ 
public 

int 
Pages 
{ 
get 
; 
set 
;  
}! "
public 

IEnumerable 
< 

ToponymDTO !
>! "
Toponyms# +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
} Ó
ÅD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Toponyms\GetAllToponymsRequestDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Toponyms %
;% &
public 
class $
GetAllToponymsRequestDTO %
{ 
public 

int 
Page 
{ 
get 
; 
set 
; 
}  !
=" #
$num$ %
;% &
public 

int 
Amount 
{ 
get 
; 
set  
;  !
}" #
=$ %
$num& (
;( )
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
=' (
null) -
;- .
} Ç
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Timeline\TimelineItemDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Timeline %
;% &
public 
class 
TimelineItemDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public		 

string		 
?		 
Description		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public

 

DateTime

 
Date

 
{

 
get

 
;

 
set

  #
;

# $
}

% &
public 

DateViewPattern 
DateViewPattern *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
public 

IEnumerable 
<  
HistoricalContextDTO +
>+ ,
HistoricalContexts- ?
{@ A
getB E
;E F
setG J
;J K
}L M
} ≠
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Timeline\HistoricalContextDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Timeline %
;% &
public 
class  
HistoricalContextDTO !
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
} Ú
vD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Team\TeamMemberLinkDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Team !
{ 
public 

class 
TeamMemberLinkDTO "
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
LogoTypeDTO 
LogoType #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public		 
string		 
	TargetUrl		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
int

 
TeamMemberId

 
{

  !
get

" %
;

% &
set

' *
;

* +
}

, -
} 
} ˘
rD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Team\TeamMemberDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Team !
{ 
public 

class 
TeamMemberDTO 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public		 
string		 
	FirstName		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
public

 
string

 
LastName

 
{

  
get

! $
;

$ %
set

& )
;

) *
}

+ ,
public 
string 
Description !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 
bool 
IsMain 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
ImageId 
{ 
get  
;  !
set" %
;% &
}' (
public 
List 
< 
TeamMemberLinkDTO %
>% &
TeamMemberLinks' 6
{7 8
get9 <
;< =
set> A
;A B
}C D
=E F
newG J
ListK O
<O P
TeamMemberLinkDTOP a
>a b
(b c
)c d
;d e
public 
List 
< 
PositionDTO 
>  
	Positions! *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
=9 :
new; >
List? C
<C D
PositionDTOD O
>O P
(P Q
)Q R
;R S
} 
} ß
pD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Team\PositionDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Team !
{ 
public 

class 
PositionDTO 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Position 
{  
get! $
;$ %
set& )
;) *
}+ ,
}		 
}

 ∂
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\Types\PersonStreetcodeDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
Types( -
;- .
public 
class 
PersonStreetcodeDTO  
:! "
StreetcodeDTO# 0
{ 
public 

string 
	FirstName 
{ 
get !
;! "
set# &
;& '
}( )
public 

string 
? 
Rank 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
LastName 
{ 
get  
;  !
set" %
;% &
}' (
} ‘
ÉD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\Types\EventStreetcodeDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
Types( -
;- .
public 
class 
EventStreetcodeDTO 
:  !
StreetcodeDTO" /
{ 
} ﬂ	
ÉD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\TextContent\Text\TextDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
TextContent( 3
.3 4
Text4 8
;8 9
public 
class 
TextDTO 
{ 
public 
int	 
Id 
{ 
get 
; 
set 
; 
} 
public 
string	 
Title 
{ 
get 
; 
set  
;  !
}" #
public 
string	 
TextContent 
{ 
get !
;! "
set# &
;& '
}( )
public 
int	 
StreetcodeId 
{ 
get 
;  
set! $
;$ %
}& '
public		 
string			 
?		 
AdditionalText		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
}

 Ã
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\TextContent\Text\TextCreateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
TextContent( 3
.3 4
Text4 8
{ 
public 
class	 
TextCreateDTO 
{ 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
TextContent 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
? 
AdditionalText !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
}		 Í
~D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\TextContent\TermDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
TextContent( 3
;3 4
public 
class 
TermDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
Description 
{ 
get  #
;# $
set% (
;( )
}* +
} Å
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\TextContent\RelatedTermDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
TextContent( 3
{ 
public		 

class		 
RelatedTermDTO		 
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Word 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
TermId 
{ 
get 
;  
set! $
;$ %
}& '
} 
} ∆
èD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\TextContent\Fact\FactUpdateCreateDto.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
TextContent( 3
.3 4
Fact4 8
{ 
public 

class 
FactUpdateCreateDto $
:% &
FactDto' .
{ 
public 
string 
? 
ImageDescription '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
} 
} ©
ÉD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\TextContent\Fact\FactDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
TextContent( 3
.3 4
Fact4 8
;8 9
public 
class 
FactDto 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public 

int 
ImageId 
{ 
get 
; 
set !
;! "
}# $
public 

string 
FactContent 
{ 
get  #
;# $
set% (
;( )
}* +
}		 æ
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\StreetcodeShortDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
{ 
public 

class 
StreetcodeShortDTO #
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ˚
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\StreetcodeMainPageDTO.cs
	namespace		 	

Streetcode		
 
.		 
BLL		 
.		 
DTO		 
.		 

Streetcode		 '
{

 
public 

class !
StreetcodeMainPageDTO &
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
? 
Alias 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
? 
Teaser 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
? 
Text 
{ 
get !
;! "
set# &
;& '
}( )
public 
int 
ImageId 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
TransliterationUrl (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} 
} ‰

ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\StreetcodeFilterResultDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
{ 
public 

class %
StreetcodeFilterResultDTO *
{ 
public 
int 
StreetcodeId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string (
StreetcodeTransliterationUrl 2
{3 4
get5 8
;8 9
set: =
;= >
}? @
public 
int 
StreetcodeIndex "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
	BlockName 
{  !
get" %
;% &
set' *
;* +
}, -
public		 
string		 
Content		 
{		 
get		  #
;		# $
set		% (
;		( )
}		* +
public

 
string

 

SourceName

  
{

! "
get

# &
;

& '
set

( +
;

+ ,
}

- .
} 
} ≠
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\StreetcodeDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
;' (
public 
class 
StreetcodeDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public		 

int		 
Index		 
{		 
get		 
;		 
set		 
;		  
}		! "
public

 

string

 
Title

 
{

 
get

 
;

 
set

 "
;

" #
}

$ %
public 

string 

DateString 
{ 
get "
;" #
set$ '
;' (
}) *
public 

string 
? 
Alias 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
TransliterationUrl $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 

StreetcodeStatus 
Status "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 

DateTime '
EventStartOrPersonBirthDate /
{0 1
get2 5
;5 6
set7 :
;: ;
}< =
public 

DateTime 
? %
EventEndOrPersonDeathDate .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
public 

int 
	ViewCount 
{ 
get 
; 
set  #
;# $
}% &
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
public 

DateTime 
	UpdatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
public 

IEnumerable 
< 
StreetcodeTagDTO '
>' (
Tags) -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
public 

string 
Teaser 
{ 
get 
; 
set  #
;# $
}% &
public 

StreetcodeType 
StreetcodeType (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ˝
éD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\RelatedFigure\RelatedFigureShortDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
RelatedFigure( 5
{ 
public 
class	 !
RelatedFigureShortDTO $
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
} 
} à
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\RelatedFigure\RelatedFigureDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
.' (
RelatedFigure( 5
;5 6
public 
class 
RelatedFigureDTO 
{ 
public 
int	 
Id 
{ 
get 
; 
set 
; 
} 
public 
string	 
Title 
{ 
get 
; 
set  
;  !
}" #
public		 
string			 
Url		 
{		 
get		 
;		 
set		 
;		 
}		  !
public

 
string

	 
?

 
Alias

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 
int	 
ImageId 
{ 
get 
; 
set 
;  
}! "
public 
IEnumerable	 
< 
TagDTO 
> 
Tags !
{" #
get$ '
;' (
set) ,
;, -
}. /
} â
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\GetAllStreetcodesResponseDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
;' (
public 
class (
GetAllStreetcodesResponseDTO )
{ 
public 

int 
Pages 
{ 
get 
; 
set 
;  
}! "
public 

IEnumerable 
< 
StreetcodeDTO $
>$ %
Streetcodes& 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
} ®
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Streetcode\GetAllStreetcodesRequestDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 

Streetcode '
;' (
public 
class '
GetAllStreetcodesRequestDTO (
{ 
public 

int 
Page 
{ 
get 
; 
set 
; 
}  !
=" #
$num$ %
;% &
public 

int 
Amount 
{ 
get 
; 
set  
;  !
}" #
=$ %
$num& (
;( )
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
=' (
null) -
;- .
public 

string 
? 
Sort 
{ 
get 
; 
set "
;" #
}$ %
=& '
null( ,
;, -
public		 

string		 
?		 
Filter		 
{		 
get		 
;		  
set		! $
;		$ %
}		& '
=		( )
null		* .
;		. /
}

 ¸
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Sources\StreetcodeCategoryContentDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Sources $
{		 
public

 

class

 (
StreetcodeCategoryContentDTO

 -
{ 
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
] 
public 
int  
SourceLinkCategoryId '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
[ 	
Required	 
] 
public 
int 
StreetcodeId 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} Ô
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Sources\SourceLinkCategoryDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Sources $
;$ %
public 
class !
SourceLinkCategoryDTO "
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public 

int 
ImageId 
{ 
get 
; 
set !
;! "
}# $
public		 

ImageDTO		 
?		 
Image		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
}

 ∫
{D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Sources\CategoryWithNameDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Sources $
{ 
public		 

class		 
CategoryWithNameDTO		 $
{

 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
} 
} §
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Sources\CategoryContentCreateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Sources $
{ 
public 
class	 $
CategoryContentCreateDTO '
{ 
public 

int 
? 
Id 
{ 
get 
; 
set 
; 
}  
public 

int  
SourceLinkCategoryId #
{$ %
get& )
;) *
set+ .
;. /
}0 1
public 

string 
? 
Text 
{ 
get 
; 
set "
;" #
}$ %
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
}		 
}

 ƒ
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Payment\PaymentResponseDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Payment $
{ 
public 

class 
PaymentResponseDTO #
{ 
public 
string 
	InvoiceId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
string 
PageUrl 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} ˆ
rD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Payment\PaymentDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Payment $
{ 
public 

class 

PaymentDTO 
{ 
[ 	
Required	 
] 
public 
long 
Amount 
{ 
get  
;  !
set" %
;% &
}' (
public

 
string

 
?

 
RedirectUrl

 "
{

# $
get

% (
;

( )
set

* -
;

- .
}

/ 0
} 
} “
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Partners\PartnerSourceLinkDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Partners %
;% &
public 
class  
PartnerSourceLinkDTO !
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

LogoTypeDTO 
LogoType 
{  !
get" %
;% &
set' *
;* +
}, -
public		 

UrlDTO		 
	TargetUrl		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
}

 ¥
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Partners\PartnerShortDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Partners %
{ 
public 

class 
PartnerShortDTO  
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
} 
} ™
sD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Partners\PartnerDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Partners %
;% &
public 
class 

PartnerDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public		 

bool		 
IsKeyPartner		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 

bool

 
IsVisibleEverywhere

 #
{

$ %
get

& )
;

) *
set

+ .
;

. /
}

0 1
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

int 
LogoId 
{ 
get 
; 
set  
;  !
}" #
public 

UrlDTO 
? 
	TargetUrl 
{ 
get "
;" #
set$ '
;' (
}) *
public 

List 
<  
PartnerSourceLinkDTO $
>$ %
?% &
PartnerSourceLinks' 9
{: ;
get< ?
;? @
setA D
;D E
}F G
public 

List 
< 
StreetcodeShortDTO "
>" #
?# $
Streetcodes% 0
{1 2
get3 6
;6 7
set8 ;
;; <
}= >
} ì
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Partners\LogoTypeDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Partners %
;% &
public 
enum 
LogoTypeDTO 
: 
byte 
{ 
Twitter 
, 
	Instagram 
, 
Facebook 
, 
YouTube 
}		 Û
|D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Partners\Create\CreateUrlDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Partners %
.% &
Create& ,
{ 
public 
class	 
CreateUrlDTO 
{ 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
Base64Photo 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} ó
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Partners\Create\CreatePartnerSourceLinkDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Partners %
.% &
Create& ,
{ 
public 
class	 &
CreatePartnerSourceLinkDTO )
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public		 

LogoType		 
LogoType		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public 

string 
	TargetUrl 
{ 
get !
;! "
set# &
;& '
}( )
} 
} é
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Partners\Create\CreatePartnerDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Partners %
{ 
public 

class 
CreatePartnerDTO !
{		 
public

 
int

 
Id

 
{

 
get

 
;

 
set

  
;

  !
}

" #
public 
bool 
IsKeyPartner  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
bool 
IsVisibleEverywhere '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
? 
	TargetUrl  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
LogoId 
{ 
get 
;  
set! $
;$ %
}& '
public 
string 
? 
UrlTitle 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< &
CreatePartnerSourceLinkDTO .
>. /
?/ 0
PartnerSourceLinks1 C
{D E
getF I
;I J
setK N
;N O
}P Q
public 
List 
< 
StreetcodeShortDTO &
>& '
Streetcodes( 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
=B C
newD G
ListH L
<L M
StreetcodeShortDTOM _
>_ `
(` a
)a b
;b c
} 
} ∂
rD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\News\RandomNewsDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
News !
{ 
public		 

class		 
RandomNewsDTO		 
{

 
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
RandomNewsUrl #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} 
} Û	
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\News\NewsDTOWithURLs.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
News !
{ 
public		 

class		 
NewsDTOWithURLs		  
{

 
public 
NewsDTO 
News 
{ 
get !
;! "
set# &
;& '
}( )
=* +
new, /
NewsDTO0 7
(7 8
)8 9
;9 :
public 
string 
? 
PrevNewsUrl "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
string 
? 
NextNewsUrl "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 
RandomNewsDTO 
? 

RandomNews (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
=7 8
new9 <
RandomNewsDTO= J
(J K
)K L
;L M
} 
} æ
lD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\News\NewsDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
News !
{ 
public 

class 
NewsDTO 
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public		 
string		 
Text		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 
int

 
?

 
ImageId

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 
string 
URL 
{ 
get 
;  
set! $
;$ %
}& '
public 
ImageDTO 
? 
Image 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 
DateTime 
CreationDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} è
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Video\VideoDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Video# (
;( )
public 
class 
VideoDTO 
{ 
public 
int	 
Id 
{ 
get 
; 
set 
; 
} 
public 
string	 
? 
Description 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
string			 
?		 
Url		 
{		 
get		 
;		 
set		 
;		  
}		! "
public

 
int

	 
StreetcodeId

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
} Á
nD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\VideoDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
;" #
public 
class 
VideoDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 

string		 
?		 
Url		 
{		 
get		 
;		 
set		 !
;		! "
}		# $
public

 

int

 
StreetcodeId

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
} ˛
ÉD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Images\ImageFileBaseCreateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Images# )
;) *
public 
class "
ImageFileBaseCreateDTO #
:$ %
FileBaseCreateDTO& 7
{ 
public 

string 
? 
Alt 
{ 
get 
; 
set !
;! "
}# $
} ◊	
uD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Images\ImageDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Images# )
;) *
public 
class 
ImageDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public		 

string		 
?		 
BlobName		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
public

 

string

 
?

 
Base64

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
public 

string 
? 
MimeType 
{ 
get !
;! "
set# &
;& '
}( )
public 

ImageDetailsDto 
? 
ImageDetails (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} Ô	
|D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Images\ImageDetailsDto.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Images# )
{ 
public 

class 
ImageDetailsDto  
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[		 	
	MaxLength			 
(		 
$num		 
)		 
]		 
public

 
string

 
?

 
Title

 
{

 
get

 "
;

" #
set

$ '
;

' (
}

) *
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Alt 
{ 
get  
;  !
set" %
;% &
}' (
public 
int 
ImageId 
{ 
get  
;  !
set" %
;% &
}' (
} 
} °
wD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\FileBaseCreateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
;" #
public 
class 
FileBaseCreateDTO 
{ 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
? 

BaseFormat 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
? 
MimeType 
{ 
get !
;! "
set# &
;& '
}( )
public 

string 
? 
	Extension 
{ 
get "
;" #
set$ '
;' (
}) *
}		 Ñ
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Audio\AudioFileBaseCreateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Audio# (
;( )
public 
class "
AudioFileBaseCreateDTO #
:$ %
FileBaseCreateDTO& 7
{ 
public 
string	 
? 
Description 
{ 
get "
;" #
set$ '
;' (
}) *
} û	
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Audio\AudioDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Audio# (
;( )
public 
class 
AudioDTO 
{ 
public 
int	 
Id 
{ 
get 
; 
set 
; 
} 
public 
string	 
? 
Description 
{ 
get "
;" #
set$ '
;' (
}) *
public		 
string			 
BlobName		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 
string

	 
Base64

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 
string	 
MimeType 
{ 
get 
; 
set  #
;# $
}% &
} Å
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Art\StreetcodeArtDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Art# &
{ 
public 

class 
StreetcodeArtDTO !
{ 
public 
int 
Index 
{ 
get 
; 
set  #
;# $
}% &
public 
int 
StreetcodeId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
ArtDTO 
? 
Art 
{ 
get  
;  !
set" %
;% &
}' (
} 
}		 Æ	
pD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Media\Art\ArtDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Media "
." #
Art# &
;& '
public 
class 
ArtDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 

string		 
?		 
Title		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public

 

int

 
ImageId

 
{

 
get

 
;

 
set

 !
;

! "
}

# $
public 

ImageDTO 
? 
Image 
{ 
get  
;  !
set" %
;% &
}' (
} 
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Feedback\ResponseDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Feedback %
;% &
public 
class 
ResponseDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
? 
Name 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
}		 Û
nD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\Email\EmailDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
Email "
{ 
public 

class 
EmailDTO 
{ 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
From 
{ 
get  
;  !
set" %
;% &
}' (
[

 	
Required

	 
]

 
[ 	
StringLength	 
( 
$num 
, 
MinimumLength (
=) *
$num+ ,
), -
]- .
public 
string 
Content 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} ∑
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\UrlDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
;. /
public 
class 
UrlDTO 
{ 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
Href 
{ 
get 
; 
set !
;! "
}# $
} „
ÅD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Tag\TagShortDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
Tag/ 2
{ 
public 
class	 
TagShortDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
} 
} Ñ
|D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Tag\TagDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
;. /
public 
class 
TagDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public		 

IEnumerable		 
<		 
StreetcodeDTO		 $
>		$ %
Streetcodes		& 1
{		2 3
get		4 7
;		7 8
set		9 <
;		< =
}		> ?
}

 û
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Tag\StreetcodeTagDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
Tag/ 2
{ 
public 

class 
StreetcodeTagDTO !
{ 
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
public 
bool 
	IsVisible 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
Index 
{ 
get 
; 
set  #
;# $
}% &
}		 
}

 “
ÇD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Tag\CreateTagDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
Tag/ 2
{ 
public 
class	 
CreateTagDTO 
{ 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
} 
} Ç
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Subtitles\SubtitleDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
	Subtitles/ 8
;8 9
public 
class 
SubtitleDTO 
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

string 
SubtitleText 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
} ˙
ìD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Filter\StreetcodeFilterRequestDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
Filter/ 5
{ 
public 

class &
StreetcodeFilterRequestDTO +
{ 
public 
string 
SearchQuery !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} ¥
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Coordinates\Types\ToponymCoordinateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
Coordinates/ :
.: ;
Types; @
;@ A
public 
class  
ToponymCoordinateDTO !
:" #
CoordinateDTO$ 1
{ 
public 

int 
	ToponymId 
{ 
get 
; 
set  #
;# $
}% &
} Ω
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Coordinates\Types\StreetcodeCoordinateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
Coordinates/ :
.: ;
Types; @
;@ A
public 
class #
StreetcodeCoordinateDTO $
:% &
CoordinateDTO' 4
{ 
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
} ü
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.BLL\DTO\AdditionalContent\Coordinates\CoordinateDTO.cs
	namespace 	

Streetcode
 
. 
BLL 
. 
DTO 
. 
AdditionalContent .
.. /
Coordinates/ :
;: ;
public 
abstract 
class 
CoordinateDTO #
{ 
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
public 

decimal 
Latitude 
{ 
get !
;! "
set# &
;& '
}( )
public 

decimal 

Longtitude 
{ 
get  #
;# $
set% (
;( )
}* +
} 