»
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Users\UserRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Users3 8
{ 
internal 
class 
UserRepository !
:" #
RepositoryBase$ 2
<2 3
User3 7
>7 8
,8 9
IUserRepository: I
{		 
public

 
UserRepository

 
(

 
StreetcodeDbContext

 1
context

2 9
)

9 :
: 
base 
( 
context 
) 
{ 	
} 	
} 
} ˆ
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Transactions\TransactLinksRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Transactions3 ?
;? @
public 
class #
TransactLinksRepository $
:% &
RepositoryBase' 5
<5 6
TransactionLink6 E
>E F
,F G$
ITransactLinksRepositoryH `
{		 
public

 
#
TransactLinksRepository

 "
(

" #
StreetcodeDbContext

# 6
	dbContext

7 @
)

@ A
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} Œ
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Toponyms\ToponymRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Toponyms3 ;
;; <
public 
class 
ToponymRepository 
:  
RepositoryBase! /
</ 0
Toponym0 7
>7 8
,8 9
IToponymRepository: L
{		 
public

 

ToponymRepository

 
(

 
StreetcodeDbContext

 0
	dbContext

1 :
)

: ;
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ç
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Toponyms\StreetcodeToponymRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Toponyms3 ;
{ 
public 
class '
StreetcodeToponymRepository )
:* +
RepositoryBase, :
<: ;
StreetcodeToponym; L
>L M
,M N(
IStreetcodeToponymRepositoryO k
{		 
public

 '
StreetcodeToponymRepository

	 $
(

$ %
StreetcodeDbContext

% 8
context

9 @
)

@ A
: 
base 	
(	 

context
 
) 
{ 
} 
} 
} ◊
ëD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Timeline\TimelineRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Timeline3 ;
;; <
public 
class 
TimelineRepository 
:  !
RepositoryBase" 0
<0 1
TimelineItem1 =
>= >
,> ?
ITimelineRepository@ S
{		 
public

 

TimelineRepository

 
(

 
StreetcodeDbContext

 1
	dbContext

2 ;
)

; <
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ∫
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Timeline\HistoricalContextTimelineRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Timeline3 ;
{ 
public 

class /
#HistoricalContextTimelineRepository 4
:5 6
RepositoryBase7 E
<E F%
HistoricalContextTimelineF _
>_ `
,` a1
$IHistoricalContextTimelineRepository	b Ü
{		 
public

 /
#HistoricalContextTimelineRepository

 2
(

2 3
StreetcodeDbContext

3 F
	dbContext

G P
)

P Q
: 	
base
 
( 
	dbContext 
) 
{ 	
} 	
} 
} ë
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Timeline\HistoricalContextRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Timeline3 ;
{ 
public 

class '
HistoricalContextRepository ,
:- .
RepositoryBase/ =
<= >
HistoricalContext> O
>O P
,P Q(
IHistoricalContextRepositoryR n
{		 
public

 '
HistoricalContextRepository

 *
(

* +
StreetcodeDbContext

+ >
	dbContext

? H
)

H I
: 	
base
 
( 
	dbContext 
) 
{ 	
} 	
} 
} Œ
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Team\TeamRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Team3 7
{		 
public

 

class

 
TeamRepository

 
:

  !
RepositoryBase

" 0
<

0 1

TeamMember

1 ;
>

; <
,

< =
ITeamRepository

> M
{ 
public 
TeamRepository 
( 
StreetcodeDbContext 1
	dbContext2 ;
); <
: 
base 
( 
	dbContext 
) 
{ 	
} 	
} 
} Û
ëD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Team\TeamPositionRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Team3 7
{		 
public

 

class

 "
TeamPositionRepository

 '
:

( )
RepositoryBase

* 8
<

8 9
TeamMemberPositions

9 L
>

L M
,

M N#
ITeamPositionRepository

O f
{ 
public "
TeamPositionRepository %
(% &
StreetcodeDbContext& 9
context: A
)A B
: 
base 
( 
context 
) 
{ 	
} 	
} 
} ‚
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Team\TeamLinkRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Team3 7
{ 
public 

class 
TeamLinkRepository #
:$ %
RepositoryBase& 4
<4 5
TeamMemberLink5 C
>C D
,D E
ITeamLinkRepositoryF Y
{		 
public

 
TeamLinkRepository

 !
(

! "
StreetcodeDbContext

" 5
	dbContext

6 ?
)

? @
: 
base 
( 
	dbContext 
) 
{ 	
} 	
} 
} ›
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Team\PositionRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Team3 7
{ 
public 

class 
PositionRepository #
:$ %
RepositoryBase& 4
<4 5
	Positions5 >
>> ?
,? @
IPositionRepositoryA T
{		 
public

 
PositionRepository

 !
(

! "
StreetcodeDbContext

" 5
	dbContext

6 ?
)

? @
: 
base 
( 
	dbContext 
) 
{ 	
} 	
} 
} ˜
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Streetcode\TextContent\TextRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3

Streetcode3 =
.= >
TextContent> I
;I J
public 
class 
TextRepository 
: 
RepositoryBase ,
<, -
Text- 1
>1 2
,2 3
ITextRepository4 C
{		 
public

 

TextRepository

 
(

 
StreetcodeDbContext

 -
	dbContext

. 7
)

7 8
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ã
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Streetcode\TextContent\TermRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3

Streetcode3 =
.= >
TextContent> I
;I J
public 
class 
TermRepository 
: 
RepositoryBase ,
<, -
Term- 1
>1 2
,2 3
ITermRepository4 C
{		 
public

 

TermRepository

 
(

 
StreetcodeDbContext

 -
streetcodeDbContext

. A
)

A B
: 	
base
 
( 
streetcodeDbContext "
)" #
{ 
} 
} ø
¢D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Streetcode\TextContent\RelatedTermRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3

Streetcode3 =
.= >
TextContent> I
{ 
public 

class !
RelatedTermRepository &
:' (
RepositoryBase) 7
<7 8
RelatedTerm8 C
>C D
,D E"
IRelatedTermRepositoryF \
{		 
public

 !
RelatedTermRepository

 $
(

$ %
StreetcodeDbContext

% 8
streetcodeDbContext

9 L
)

L M
: 	
base
 
( 
streetcodeDbContext "
)" #
{ 	
} 	
} 
} ã
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Streetcode\TextContent\FactRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3

Streetcode3 =
.= >
TextContent> I
;I J
public 
class 
FactRepository 
: 
RepositoryBase ,
<, -
Fact- 1
>1 2
,2 3
IFactRepository4 C
{		 
public

 

FactRepository

 
(

 
StreetcodeDbContext

 -
streetcodeDbContext

. A
)

A B
: 	
base
 
( 
streetcodeDbContext "
)" #
{ 
} 
} Ë
ïD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Streetcode\StreetcodeRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3

Streetcode3 =
;= >
public 
class  
StreetcodeRepository !
:" #
RepositoryBase$ 2
<2 3
StreetcodeContent3 D
>D E
,E F!
IStreetcodeRepositoryG \
{		 
public

 
 
StreetcodeRepository

 
(

  
StreetcodeDbContext

  3
	dbContext

4 =
)

= >
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} Ó
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Streetcode\RelatedFigureRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3

Streetcode3 =
;= >
internal 
class	 #
RelatedFigureRepository &
:' (
RepositoryBase) 7
<7 8
RelatedFigure8 E
>E F
,F G$
IRelatedFigureRepositoryH `
{		 
public

 
#
RelatedFigureRepository

 "
(

" #
StreetcodeDbContext

# 6
context

7 >
)

> ?
: 	
base
 
( 
context 
) 
{ 
} 
} ∂
†D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Source\StreetcodeCategoryContentRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Source3 9
{ 
public 

class /
#StreetcodeCategoryContentRepository 4
:5 6
RepositoryBase7 E
<E F%
StreetcodeCategoryContentF _
>_ `
,` a1
$IStreetcodeCategoryContentRepository	b Ü
{		 
public

 /
#StreetcodeCategoryContentRepository

 2
(

2 3
StreetcodeDbContext

3 F
	dbContext

G P
)

P Q
: 	
base
 
( 
	dbContext 
) 
{ 	
} 	
} 
} Ò
ïD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Source\SourceCategoryRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Source3 9
;9 :
public 
class $
SourceCategoryRepository %
:& '
RepositoryBase( 6
<6 7
SourceLinkCategory7 I
>I J
,J K%
ISourceCategoryRepositoryL e
{		 
public

 
$
SourceCategoryRepository

 #
(

# $
StreetcodeDbContext

$ 7
	dbContext

8 A
)

A B
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ä
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Partners\PartnerStreetodeRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Partners3 ;
{ 
public 

class &
PartnerStreetodeRepository +
:, -
RepositoryBase. <
<< =
StreetcodePartner= N
>N O
,O P(
IPartnerStreetcodeRepositoryQ m
{		 
public

 &
PartnerStreetodeRepository

 )
(

) *
StreetcodeDbContext

* =
context

> E
)

E F
: 
base 
( 
context 
) 
{ 	
} 	
} 
} “
ëD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Partners\PartnersRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Partners3 ;
;; <
public 
class 
PartnersRepository 
:  !
RepositoryBase" 0
<0 1
Partner1 8
>8 9
,9 :
IPartnersRepository; N
{		 
public

 

PartnersRepository

 
(

 
StreetcodeDbContext

 1
	dbContext

2 ;
)

; <
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ê
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Partners\PartnersourceLinksRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Partners3 ;
{ 
public 

class (
PartnersourceLinksRepository -
:. /
RepositoryBase0 >
<> ?
PartnerSourceLink? P
>P Q
,Q R(
IPartnerSourceLinkRepositoryS o
{		 
public

 (
PartnersourceLinksRepository

 +
(

+ ,
StreetcodeDbContext

, ?
context

@ G
)

G H
: 
base 
( 
context 
) 
{ 	
} 	
} 
}  
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Newss\NewsRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Newss3 8
{ 
public 

class 
NewsRepository 
:  !
RepositoryBase" 0
<0 1
News1 5
>5 6
,6 7
INewsRepository8 G
{		 
public

 
NewsRepository

 
(

 
StreetcodeDbContext

 1
	dbContext

2 ;
)

; <
: 	
base
 
( 
	dbContext 
) 
{ 	
} 	
} 
} æ
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Media\VideoRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Media3 8
;8 9
public 
class 
VideoRepository 
: 
RepositoryBase -
<- .
Video. 3
>3 4
,4 5
IVideoRepository6 F
{		 
public

 

VideoRepository

 
(

 
StreetcodeDbContext

 .
	dbContext

/ 8
)

8 9
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ß
úD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Media\Images\StreetcodeImageRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Media3 8
.8 9
Images9 ?
{ 
public 
class %
StreetcodeImageRepository '
:( )
RepositoryBase* 8
<8 9
StreetcodeImage9 H
>H I
,I J&
IStreetcodeImageRepositoryK e
{		 
public

 %
StreetcodeImageRepository

	 "
(

" #
StreetcodeDbContext

# 6
context

7 >
)

> ?
: 
base 	
(	 

context
 
) 
{ 
} 
} 
} ê
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Media\Images\StreetcodeArtRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Media3 8
.8 9
Images9 ?
;? @
public 
class #
StreetcodeArtRepository $
:% &
RepositoryBase' 5
<5 6
StreetcodeArt6 C
>C D
,D E$
IStreetcodeArtRepositoryF ^
{		 
public

 
#
StreetcodeArtRepository

 "
(

" #
StreetcodeDbContext

# 6
	dbContext

7 @
)

@ A
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} Ë
íD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Media\Images\ImageRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Media3 8
.8 9
Images9 ?
;? @
public 
class 
ImageRepository 
: 
RepositoryBase -
<- .
Image. 3
>3 4
,4 5
IImageRepository6 F
{		 
public

 

ImageRepository

 
(

 
StreetcodeDbContext

 .
	dbContext

/ 8
)

8 9
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ú
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Media\Images\ImageDetailsRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Media3 8
.8 9
Images9 ?
{ 
public 

class "
ImageDetailsRepository '
:( )
RepositoryBase* 8
<8 9
ImageDetails9 E
>E F
,F G#
IImageDetailsRepositoryH _
{		 
public

 "
ImageDetailsRepository

 %
(

% &
StreetcodeDbContext

& 9
	dbContext

: C
)

C D
: 	
base
 
( 
	dbContext 
) 
{ 	
} 	
} 
} ﬁ
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Media\Images\ArtRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Media3 8
.8 9
Images9 ?
;? @
public 
class 
ArtRepository 
: 
RepositoryBase +
<+ ,
Art, /
>/ 0
,0 1
IArtRepository2 @
{		 
public

 

ArtRepository

 
(

 
StreetcodeDbContext

 ,
	dbContext

- 6
)

6 7
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} æ
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Media\AudioRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
Media3 8
;8 9
public 
class 
AudioRepository 
: 
RepositoryBase -
<- .
Audio. 3
>3 4
,4 5
IAudioRepository6 F
{		 
public

 

AudioRepository

 
(

 
StreetcodeDbContext

 .
	dbContext

/ 8
)

8 9
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} Ç”
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Base\RepositoryWrapper.cs
	namespace!! 	

Streetcode!!
 
.!! 
DAL!! 
.!! 
Repositories!! %
.!!% &
Realizations!!& 2
.!!2 3
Base!!3 7
;!!7 8
public## 
class## 
RepositoryWrapper## 
:##  
IRepositoryWrapper##! 3
{$$ 
private%% 
readonly%% 
StreetcodeDbContext%% ( 
_streetcodeDbContext%%) =
;%%= >
private'' 
IVideoRepository'' 
_videoRepository'' -
;''- .
private)) 
IAudioRepository)) 
_audioRepository)) -
;))- .
private++ +
IStreetcodeCoordinateRepository++ ++
_streetcodeCoordinateRepository++, K
;++K L
private-- 
IImageRepository-- 
_imageRepository-- -
;--- .
private// #
IImageDetailsRepository// ##
_imageDetailsRepository//$ ;
;//; <
private11 
IArtRepository11 
_artRepository11 )
;11) *
private33 $
IStreetcodeArtRepository33 $$
_streetcodeArtRepository33% =
;33= >
private55 
IFactRepository55 
_factRepository55 +
;55+ ,
private77 
IPartnersRepository77 
_partnersRepository77  3
;773 4
private99 %
ISourceCategoryRepository99 %%
_sourceCategoryRepository99& ?
;99? @
private;; 0
$IStreetcodeCategoryContentRepository;; 00
$_streetcodeCategoryContentRepository;;1 U
;;;U V
private== $
IRelatedFigureRepository== $$
_relatedFigureRepository==% =
;=== >
private?? "
IRelatedTermRepository?? ""
_relatedTermRepository??# 9
;??9 :
privateAA !
IStreetcodeRepositoryAA !!
_streetcodeRepositoryAA" 7
;AA7 8
privateCC 
ISubtitleRepositoryCC 
_subtitleRepositoryCC  3
;CC3 4
privateEE &
IStatisticRecordRepositoryEE &&
_statisticRecordRepositoryEE' A
;EEA B
privateGG 
ITagRepositoryGG 
_tagRepositoryGG )
;GG) *
privateII 
ITermRepositoryII 
_termRepositoryII +
;II+ ,
privateKK 
ITeamRepositoryKK 
_teamRepositoryKK +
;KK+ ,
privateMM 
IPositionRepositoryMM 
_positionRepositoryMM  3
;MM3 4
privateOO 
ITextRepositoryOO 
_textRepositoryOO +
;OO+ ,
privateQQ 
ITimelineRepositoryQQ 
_timelineRepositoryQQ  3
;QQ3 4
privateSS 
IToponymRepositorySS 
_toponymRepositorySS 1
;SS1 2
privateUU $
ITransactLinksRepositoryUU $$
_transactLinksRepositoryUU% =
;UU= >
privateWW (
IHistoricalContextRepositoryWW (%
_historyContextRepositoryWW) B
;WWB C
privateYY (
IPartnerSourceLinkRepositoryYY ((
_partnerSourceLinkRepositoryYY) E
;YYE F
private[[ 
IUserRepository[[ 
_userRepository[[ +
;[[+ ,
private]] )
IStreetcodeTagIndexRepository]] ))
_streetcodeTagIndexRepository]]* G
;]]G H
private__ (
IPartnerStreetcodeRepository__ ((
_partnerStreetcodeRepository__) E
;__E F
privateaa 
INewsRepositoryaa 
_newsRepositoryaa +
;aa+ ,
privatecc 
ITeamLinkRepositorycc 
_teamLinkRepositorycc  3
;cc3 4
privateee #
ITeamPositionRepositoryee ##
_teamPositionRepositoryee$ ;
;ee; <
privategg 0
$IHistoricalContextTimelineRepositorygg 00
$_historicalContextTimelineRepositorygg1 U
;ggU V
privateii (
IStreetcodeToponymRepositoryii ((
_streetcodeToponymRepositoryii) E
;iiE F
privatekk &
IStreetcodeImageRepositorykk &&
_streetcodeImageRepositorykk' A
;kkA B
publicmm 

RepositoryWrappermm 
(mm 
StreetcodeDbContextmm 0
streetcodeDbContextmm1 D
)mmD E
{nn  
_streetcodeDbContextoo 
=oo 
streetcodeDbContextoo 2
;oo2 3
}pp 
publicrr 

INewsRepositoryrr 
NewsRepositoryrr )
{ss 
gettt 
{uu 	
ifvv 
(vv 
_newsRepositoryvv 
isvv  "
nullvv# '
)vv' (
{ww 
_newsRepositoryxx 
=xx  !
newxx" %
NewsRepositoryxx& 4
(xx4 5 
_streetcodeDbContextxx5 I
)xxI J
;xxJ K
}yy 
return{{ 
_newsRepository{{ "
;{{" #
}|| 	
}}} 
public 

IFactRepository 
FactRepository )
{
ÄÄ 
get
ÅÅ 
{
ÇÇ 	
if
ÉÉ 
(
ÉÉ 
_factRepository
ÉÉ 
is
ÉÉ  "
null
ÉÉ# '
)
ÉÉ' (
{
ÑÑ 
_factRepository
ÖÖ 
=
ÖÖ  !
new
ÖÖ" %
FactRepository
ÖÖ& 4
(
ÖÖ4 5"
_streetcodeDbContext
ÖÖ5 I
)
ÖÖI J
;
ÖÖJ K
}
ÜÜ 
return
àà 
_factRepository
àà "
;
àà" #
}
ââ 	
}
ää 
public
åå 

IImageRepository
åå 
ImageRepository
åå +
{
çç 
get
éé 
{
èè 	
if
êê 
(
êê 
_imageRepository
êê  
is
êê! #
null
êê$ (
)
êê( )
{
ëë 
_imageRepository
íí  
=
íí! "
new
íí# &
ImageRepository
íí' 6
(
íí6 7"
_streetcodeDbContext
íí7 K
)
ííK L
;
ííL M
}
ìì 
return
ïï 
_imageRepository
ïï #
;
ïï# $
}
ññ 	
}
óó 
public
ôô 

ITeamRepository
ôô 
TeamRepository
ôô )
{
öö 
get
õõ 
{
úú 	
if
ùù 
(
ùù 
_teamRepository
ùù 
is
ùù  "
null
ùù# '
)
ùù' (
{
ûû 
_teamRepository
üü 
=
üü  !
new
üü" %
TeamRepository
üü& 4
(
üü4 5"
_streetcodeDbContext
üü5 I
)
üüI J
;
üüJ K
}
†† 
return
¢¢ 
_teamRepository
¢¢ "
;
¢¢" #
}
££ 	
}
§§ 
public
¶¶ 
%
ITeamPositionRepository
¶¶ "$
TeamPositionRepository
¶¶# 9
{
ßß 
get
®® 
{
©© 	
if
™™ 
(
™™ %
_teamPositionRepository
™™ '
is
™™( *
null
™™+ /
)
™™/ 0
{
´´ %
_teamPositionRepository
¨¨ '
=
¨¨( )
new
¨¨* -$
TeamPositionRepository
¨¨. D
(
¨¨D E"
_streetcodeDbContext
¨¨E Y
)
¨¨Y Z
;
¨¨Z [
}
≠≠ 
return
ØØ %
_teamPositionRepository
ØØ *
;
ØØ* +
}
∞∞ 	
}
±± 
public
≥≥ 

IAudioRepository
≥≥ 
AudioRepository
≥≥ +
{
¥¥ 
get
µµ 
{
∂∂ 	
if
∑∑ 
(
∑∑ 
_audioRepository
∑∑  
is
∑∑! #
null
∑∑$ (
)
∑∑( )
{
∏∏ 
_audioRepository
ππ  
=
ππ! "
new
ππ# &
AudioRepository
ππ' 6
(
ππ6 7"
_streetcodeDbContext
ππ7 K
)
ππK L
;
ππL M
}
∫∫ 
return
ºº 
_audioRepository
ºº #
;
ºº# $
}
ΩΩ 	
}
ææ 
public
¿¿ 
-
IStreetcodeCoordinateRepository
¿¿ *,
StreetcodeCoordinateRepository
¿¿+ I
{
¡¡ 
get
¬¬ 
{
√√ 	
if
ƒƒ 
(
ƒƒ -
_streetcodeCoordinateRepository
ƒƒ /
is
ƒƒ0 2
null
ƒƒ3 7
)
ƒƒ7 8
{
≈≈ -
_streetcodeCoordinateRepository
∆∆ /
=
∆∆0 1
new
∆∆2 5,
StreetcodeCoordinateRepository
∆∆6 T
(
∆∆T U"
_streetcodeDbContext
∆∆U i
)
∆∆i j
;
∆∆j k
}
«« 
return
…… -
_streetcodeCoordinateRepository
…… 2
;
……2 3
}
   	
}
ÀÀ 
public
ÕÕ 

IVideoRepository
ÕÕ 
VideoRepository
ÕÕ +
{
ŒŒ 
get
œœ 
{
–– 	
if
—— 
(
—— 
_videoRepository
——  
is
——! #
null
——$ (
)
——( )
{
““ 
_videoRepository
””  
=
””! "
new
””# &
VideoRepository
””' 6
(
””6 7"
_streetcodeDbContext
””7 K
)
””K L
;
””L M
}
‘‘ 
return
÷÷ 
_videoRepository
÷÷ #
;
÷÷# $
}
◊◊ 	
}
ÿÿ 
public
⁄⁄ 

IArtRepository
⁄⁄ 
ArtRepository
⁄⁄ '
{
€€ 
get
‹‹ 
{
›› 	
if
ﬁﬁ 
(
ﬁﬁ 
_artRepository
ﬁﬁ 
is
ﬁﬁ !
null
ﬁﬁ" &
)
ﬁﬁ& '
{
ﬂﬂ 
_artRepository
‡‡ 
=
‡‡  
new
‡‡! $
ArtRepository
‡‡% 2
(
‡‡2 3"
_streetcodeDbContext
‡‡3 G
)
‡‡G H
;
‡‡H I
}
·· 
return
„„ 
_artRepository
„„ !
;
„„! "
}
‰‰ 	
}
ÂÂ 
public
ÁÁ 
&
IStreetcodeArtRepository
ÁÁ #%
StreetcodeArtRepository
ÁÁ$ ;
{
ËË 
get
ÈÈ 
{
ÍÍ 	
if
ÎÎ 
(
ÎÎ &
_streetcodeArtRepository
ÎÎ (
is
ÎÎ) +
null
ÎÎ, 0
)
ÎÎ0 1
{
ÏÏ &
_streetcodeArtRepository
ÌÌ (
=
ÌÌ) *
new
ÌÌ+ .%
StreetcodeArtRepository
ÌÌ/ F
(
ÌÌF G"
_streetcodeDbContext
ÌÌG [
)
ÌÌ[ \
;
ÌÌ\ ]
}
ÓÓ 
return
 &
_streetcodeArtRepository
 +
;
+ ,
}
ÒÒ 	
}
ÚÚ 
public
ÙÙ 
!
IPartnersRepository
ÙÙ  
PartnersRepository
ÙÙ 1
{
ıı 
get
ˆˆ 
{
˜˜ 	
if
¯¯ 
(
¯¯ !
_partnersRepository
¯¯ #
is
¯¯$ &
null
¯¯' +
)
¯¯+ ,
{
˘˘ !
_partnersRepository
˙˙ #
=
˙˙$ %
new
˙˙& ) 
PartnersRepository
˙˙* <
(
˙˙< ="
_streetcodeDbContext
˙˙= Q
)
˙˙Q R
;
˙˙R S
}
˚˚ 
return
˝˝ !
_partnersRepository
˝˝ &
;
˝˝& '
}
˛˛ 	
}
ˇˇ 
public
ÅÅ 
'
ISourceCategoryRepository
ÅÅ $&
SourceCategoryRepository
ÅÅ% =
{
ÇÇ 
get
ÉÉ 
{
ÑÑ 	
if
ÖÖ 
(
ÖÖ '
_sourceCategoryRepository
ÖÖ )
is
ÖÖ* ,
null
ÖÖ- 1
)
ÖÖ1 2
{
ÜÜ '
_sourceCategoryRepository
áá )
=
áá* +
new
áá, /&
SourceCategoryRepository
áá0 H
(
ááH I"
_streetcodeDbContext
ááI ]
)
áá] ^
;
áá^ _
}
àà 
return
ää '
_sourceCategoryRepository
ää ,
;
ää, -
}
ãã 	
}
åå 
public
éé 
2
$IStreetcodeCategoryContentRepository
éé /1
#StreetcodeCategoryContentRepository
éé0 S
{
èè 
get
êê 
{
ëë 	
if
íí 
(
íí 2
$_streetcodeCategoryContentRepository
íí 4
is
íí5 7
null
íí8 <
)
íí< =
{
ìì 2
$_streetcodeCategoryContentRepository
îî 4
=
îî5 6
new
îî7 :1
#StreetcodeCategoryContentRepository
îî; ^
(
îî^ _"
_streetcodeDbContext
îî_ s
)
îîs t
;
îît u
}
ïï 
return
óó 2
$_streetcodeCategoryContentRepository
óó 7
;
óó7 8
}
òò 	
}
ôô 
public
õõ 
&
IRelatedFigureRepository
õõ #%
RelatedFigureRepository
õõ$ ;
{
úú 
get
ùù 
{
ûû 	
if
üü 
(
üü &
_relatedFigureRepository
üü (
is
üü) +
null
üü, 0
)
üü0 1
{
†† &
_relatedFigureRepository
°° (
=
°°) *
new
°°+ .%
RelatedFigureRepository
°°/ F
(
°°F G"
_streetcodeDbContext
°°G [
)
°°[ \
;
°°\ ]
}
¢¢ 
return
§§ &
_relatedFigureRepository
§§ +
;
§§+ ,
}
•• 	
}
¶¶ 
public
®® 
#
IStreetcodeRepository
®®  "
StreetcodeRepository
®®! 5
{
©© 
get
™™ 
{
´´ 	
if
¨¨ 
(
¨¨ #
_streetcodeRepository
¨¨ %
is
¨¨& (
null
¨¨) -
)
¨¨- .
{
≠≠ #
_streetcodeRepository
ÆÆ %
=
ÆÆ& '
new
ÆÆ( +"
StreetcodeRepository
ÆÆ, @
(
ÆÆ@ A"
_streetcodeDbContext
ÆÆA U
)
ÆÆU V
;
ÆÆV W
}
ØØ 
return
±± #
_streetcodeRepository
±± (
;
±±( )
}
≤≤ 	
}
≥≥ 
public
µµ 
!
ISubtitleRepository
µµ  
SubtitleRepository
µµ 1
{
∂∂ 
get
∑∑ 
{
∏∏ 	
if
ππ 
(
ππ !
_subtitleRepository
ππ #
is
ππ$ &
null
ππ' +
)
ππ+ ,
{
∫∫ !
_subtitleRepository
ªª #
=
ªª$ %
new
ªª& ) 
SubtitleRepository
ªª* <
(
ªª< ="
_streetcodeDbContext
ªª= Q
)
ªªQ R
;
ªªR S
}
ºº 
return
ææ !
_subtitleRepository
ææ &
;
ææ& '
}
øø 	
}
¿¿ 
public
¬¬ 
(
IStatisticRecordRepository
¬¬ %'
StatisticRecordRepository
¬¬& ?
{
√√ 
get
ƒƒ 
{
≈≈ 	
if
∆∆ 
(
∆∆ (
_statisticRecordRepository
∆∆ *
is
∆∆+ -
null
∆∆. 2
)
∆∆2 3
{
«« (
_statisticRecordRepository
»» *
=
»»+ ,
new
»»- 0(
StatisticRecordsRepository
»»1 K
(
»»K L"
_streetcodeDbContext
»»L `
)
»»` a
;
»»a b
}
…… 
return
ÀÀ (
_statisticRecordRepository
ÀÀ -
;
ÀÀ- .
}
ÃÃ 	
}
ÕÕ 
public
œœ 

ITagRepository
œœ 
TagRepository
œœ '
{
–– 
get
—— 
{
““ 	
if
”” 
(
”” 
_tagRepository
”” 
is
”” !
null
””" &
)
””& '
{
‘‘ 
_tagRepository
’’ 
=
’’  
new
’’! $
TagRepository
’’% 2
(
’’2 3"
_streetcodeDbContext
’’3 G
)
’’G H
;
’’H I
}
÷÷ 
return
ÿÿ 
_tagRepository
ÿÿ !
;
ÿÿ! "
}
ŸŸ 	
}
⁄⁄ 
public
‹‹ 

ITermRepository
‹‹ 
TermRepository
‹‹ )
{
›› 
get
ﬁﬁ 
{
ﬂﬂ 	
if
‡‡ 
(
‡‡ 
_termRepository
‡‡ 
is
‡‡  "
null
‡‡# '
)
‡‡' (
{
·· 
_termRepository
‚‚ 
=
‚‚  !
new
‚‚" %
TermRepository
‚‚& 4
(
‚‚4 5"
_streetcodeDbContext
‚‚5 I
)
‚‚I J
;
‚‚J K
}
„„ 
return
ÂÂ 
_termRepository
ÂÂ "
;
ÂÂ" #
}
ÊÊ 	
}
ÁÁ 
public
ÈÈ 

ITextRepository
ÈÈ 
TextRepository
ÈÈ )
{
ÍÍ 
get
ÎÎ 
{
ÏÏ 	
if
ÌÌ 
(
ÌÌ 
_textRepository
ÌÌ 
is
ÌÌ  "
null
ÌÌ# '
)
ÌÌ' (
{
ÓÓ 
_textRepository
ÔÔ 
=
ÔÔ  !
new
ÔÔ" %
TextRepository
ÔÔ& 4
(
ÔÔ4 5"
_streetcodeDbContext
ÔÔ5 I
)
ÔÔI J
;
ÔÔJ K
}
 
return
ÚÚ 
_textRepository
ÚÚ "
;
ÚÚ" #
}
ÛÛ 	
}
ÙÙ 
public
ˆˆ 
!
ITimelineRepository
ˆˆ  
TimelineRepository
ˆˆ 1
{
˜˜ 
get
¯¯ 
{
˘˘ 	
if
˙˙ 
(
˙˙ !
_timelineRepository
˙˙ #
is
˙˙$ &
null
˙˙' +
)
˙˙+ ,
{
˚˚ !
_timelineRepository
¸¸ #
=
¸¸$ %
new
¸¸& ) 
TimelineRepository
¸¸* <
(
¸¸< ="
_streetcodeDbContext
¸¸= Q
)
¸¸Q R
;
¸¸R S
}
˝˝ 
return
ˇˇ !
_timelineRepository
ˇˇ &
;
ˇˇ& '
}
ÄÄ 	
}
ÅÅ 
public
ÉÉ 
 
IToponymRepository
ÉÉ 
ToponymRepository
ÉÉ /
{
ÑÑ 
get
ÖÖ 
{
ÜÜ 	
if
áá 
(
áá  
_toponymRepository
áá "
is
áá# %
null
áá& *
)
áá* +
{
àà  
_toponymRepository
ââ "
=
ââ# $
new
ââ% (
ToponymRepository
ââ) :
(
ââ: ;"
_streetcodeDbContext
ââ; O
)
ââO P
;
ââP Q
}
ää 
return
åå  
_toponymRepository
åå %
;
åå% &
}
çç 	
}
éé 
public
êê 
&
ITransactLinksRepository
êê #%
TransactLinksRepository
êê$ ;
{
ëë 
get
íí 
{
ìì 	
if
îî 
(
îî &
_transactLinksRepository
îî (
is
îî) +
null
îî, 0
)
îî0 1
{
ïï &
_transactLinksRepository
ññ (
=
ññ) *
new
ññ+ .%
TransactLinksRepository
ññ/ F
(
ññF G"
_streetcodeDbContext
ññG [
)
ññ[ \
;
ññ\ ]
}
óó 
return
ôô &
_transactLinksRepository
ôô +
;
ôô+ ,
}
öö 	
}
õõ 
public
ùù 
*
IHistoricalContextRepository
ùù ')
HistoricalContextRepository
ùù( C
{
ûû 
get
üü 
{
†† 	
if
°° 
(
°° '
_historyContextRepository
°° )
is
°°* ,
null
°°- 1
)
°°1 2
{
¢¢ '
_historyContextRepository
££ )
=
££* +
new
££, /)
HistoricalContextRepository
££0 K
(
££K L"
_streetcodeDbContext
££L `
)
££` a
;
££a b
}
§§ 
return
¶¶ '
_historyContextRepository
¶¶ ,
;
¶¶, -
}
ßß 	
}
®® 
public
™™ 
*
IPartnerSourceLinkRepository
™™ ')
PartnerSourceLinkRepository
™™( C
{
´´ 
get
¨¨ 
{
≠≠ 	
if
ÆÆ 
(
ÆÆ *
_partnerSourceLinkRepository
ÆÆ ,
is
ÆÆ- /
null
ÆÆ0 4
)
ÆÆ4 5
{
ØØ *
_partnerSourceLinkRepository
∞∞ ,
=
∞∞- .
new
∞∞/ 2*
PartnersourceLinksRepository
∞∞3 O
(
∞∞O P"
_streetcodeDbContext
∞∞P d
)
∞∞d e
;
∞∞e f
}
±± 
return
≥≥ *
_partnerSourceLinkRepository
≥≥ /
;
≥≥/ 0
}
¥¥ 	
}
µµ 
public
∑∑ 
$
IRelatedTermRepository
∑∑ !#
RelatedTermRepository
∑∑" 7
{
∏∏ 
get
ππ 
{
∫∫ 	
if
ªª 
(
ªª $
_relatedTermRepository
ªª %
is
ªª& (
null
ªª) -
)
ªª- .
{
ºº $
_relatedTermRepository
ΩΩ &
=
ΩΩ' (
new
ΩΩ) ,#
RelatedTermRepository
ΩΩ- B
(
ΩΩB C"
_streetcodeDbContext
ΩΩC W
)
ΩΩW X
;
ΩΩX Y
}
ææ 
return
¿¿ $
_relatedTermRepository
¿¿ )
;
¿¿) *
}
¡¡ 	
}
¬¬ 
public
ƒƒ 

IUserRepository
ƒƒ 
UserRepository
ƒƒ )
{
≈≈ 
get
∆∆ 
{
«« 	
if
»» 
(
»» 
_userRepository
»» 
is
»»  "
null
»»# '
)
»»' (
{
…… 
_userRepository
   
=
    !
new
  " %
UserRepository
  & 4
(
  4 5"
_streetcodeDbContext
  5 I
)
  I J
;
  J K
}
ÀÀ 
return
ÕÕ 
_userRepository
ÕÕ "
;
ÕÕ" #
}
ŒŒ 	
}
œœ 
public
—— 
+
IStreetcodeTagIndexRepository
—— (*
StreetcodeTagIndexRepository
——) E
{
““ 
get
”” 
{
‘‘ 	
if
’’ 
(
’’ +
_streetcodeTagIndexRepository
’’ -
is
’’. 0
null
’’1 5
)
’’5 6
{
÷÷ +
_streetcodeTagIndexRepository
◊◊ -
=
◊◊. /
new
◊◊0 3*
StreetcodeTagIndexRepository
◊◊4 P
(
◊◊P Q"
_streetcodeDbContext
◊◊Q e
)
◊◊e f
;
◊◊f g
}
ÿÿ 
return
⁄⁄ +
_streetcodeTagIndexRepository
⁄⁄ 0
;
⁄⁄0 1
}
€€ 	
}
‹‹ 
public
ﬁﬁ 
*
IPartnerStreetcodeRepository
ﬁﬁ ')
PartnerStreetcodeRepository
ﬁﬁ( C
{
ﬂﬂ 
get
‡‡ 
{
·· 	
if
‚‚ 
(
‚‚ *
_partnerStreetcodeRepository
‚‚ +
is
‚‚, .
null
‚‚/ 3
)
‚‚3 4
{
„„ *
_partnerStreetcodeRepository
‰‰ ,
=
‰‰- .
new
‰‰/ 2(
PartnerStreetodeRepository
‰‰3 M
(
‰‰M N"
_streetcodeDbContext
‰‰N b
)
‰‰b c
;
‰‰c d
}
ÂÂ 
return
ÁÁ *
_partnerStreetcodeRepository
ÁÁ /
;
ÁÁ/ 0
}
ËË 	
}
ÈÈ 
public
ÎÎ 
!
IPositionRepository
ÎÎ  
PositionRepository
ÎÎ 1
{
ÏÏ 
get
ÌÌ 
{
ÓÓ 	
if
ÔÔ 
(
ÔÔ !
_positionRepository
ÔÔ #
is
ÔÔ$ &
null
ÔÔ' +
)
ÔÔ+ ,
{
 !
_positionRepository
ÒÒ #
=
ÒÒ$ %
new
ÒÒ& ) 
PositionRepository
ÒÒ* <
(
ÒÒ< ="
_streetcodeDbContext
ÒÒ= Q
)
ÒÒQ R
;
ÒÒR S
}
ÚÚ 
return
ÙÙ !
_positionRepository
ÙÙ &
;
ÙÙ& '
}
ıı 	
}
ˆˆ 
public
¯¯ 
!
ITeamLinkRepository
¯¯  
TeamLinkRepository
¯¯ 1
{
˘˘ 
get
˙˙ 
{
˚˚ 	
if
¸¸ 
(
¸¸ !
_teamLinkRepository
¸¸ #
is
¸¸$ &
null
¸¸' +
)
¸¸+ ,
{
˝˝ !
_teamLinkRepository
˛˛ #
=
˛˛$ %
new
˛˛& ) 
TeamLinkRepository
˛˛* <
(
˛˛< ="
_streetcodeDbContext
˛˛= Q
)
˛˛Q R
;
˛˛R S
}
ˇˇ 
return
ÅÅ !
_teamLinkRepository
ÅÅ &
;
ÅÅ& '
}
ÇÇ 	
}
ÉÉ 
public
ÖÖ 
%
IImageDetailsRepository
ÖÖ "$
ImageDetailsRepository
ÖÖ# 9
=>
ÖÖ: <%
_imageDetailsRepository
ÖÖ= T
??=
ÖÖT W
new
ÖÖW Z$
ImageDetailsRepository
ÖÖ[ q
(
ÖÖq r#
_streetcodeDbContextÖÖr Ü
)ÖÖÜ á
;ÖÖá à
public
áá 
2
$IHistoricalContextTimelineRepository
áá /1
#HistoricalContextTimelineRepository
áá0 S
{
àà 
get
ââ 
{
ää 	
if
ãã 
(
ãã 2
$_historicalContextTimelineRepository
ãã 4
is
ãã5 7
null
ãã8 <
)
ãã< =
{
åå 2
$_historicalContextTimelineRepository
çç 4
=
çç5 6
new
çç7 :1
#HistoricalContextTimelineRepository
çç; ^
(
çç^ _"
_streetcodeDbContext
çç_ s
)
ççs t
;
ççt u
}
éé 
return
êê 2
$_historicalContextTimelineRepository
êê 7
;
êê7 8
}
ëë 	
}
íí 
public
îî 
*
IStreetcodeToponymRepository
îî ')
StreetcodeToponymRepository
îî( C
{
ïï 
get
ññ 
{
óó 
if
òò 
(
òò *
_streetcodeToponymRepository
òò #
is
òò$ &
null
òò' +
)
òò+ ,
{
ôô *
_streetcodeToponymRepository
öö  
=
öö! "
new
öö# &)
StreetcodeToponymRepository
öö' B
(
ööB C"
_streetcodeDbContext
ööC W
)
ööW X
;
ööX Y
}
õõ 
return
ùù 	*
_streetcodeToponymRepository
ùù
 &
;
ùù& '
}
ûû 
}
üü 
public
°° 
(
IStreetcodeImageRepository
°° %'
StreetcodeImageRepository
°°& ?
{
¢¢ 
get
££ 
{
§§ 
if
•• 
(
•• (
_streetcodeImageRepository
•• !
is
••" $
null
••% )
)
••) *
{
¶¶ (
_streetcodeImageRepository
ßß 
=
ßß  
new
ßß! $'
StreetcodeImageRepository
ßß% >
(
ßß> ?"
_streetcodeDbContext
ßß? S
)
ßßS T
;
ßßT U
}
®® 
return
™™ 	(
_streetcodeImageRepository
™™
 $
;
™™$ %
}
´´ 
}
¨¨ 
public
ÆÆ 

int
ÆÆ 
SaveChanges
ÆÆ 
(
ÆÆ 
)
ÆÆ 
{
ØØ 
return
∞∞ "
_streetcodeDbContext
∞∞ #
.
∞∞# $
SaveChanges
∞∞$ /
(
∞∞/ 0
)
∞∞0 1
;
∞∞1 2
}
±± 
public
≥≥ 

async
≥≥ 
Task
≥≥ 
<
≥≥ 
int
≥≥ 
>
≥≥ 
SaveChangesAsync
≥≥ +
(
≥≥+ ,
)
≥≥, -
{
¥¥ 
return
µµ 
await
µµ "
_streetcodeDbContext
µµ )
.
µµ) *
SaveChangesAsync
µµ* :
(
µµ: ;
)
µµ; <
;
µµ< =
}
∂∂ 
public
∏∏ 

TransactionScope
∏∏ 
BeginTransaction
∏∏ ,
(
∏∏, -
)
∏∏- .
{
ππ 
return
∫∫ 
new
∫∫ 
TransactionScope
∫∫ #
(
∫∫# $-
TransactionScopeAsyncFlowOption
∫∫$ C
.
∫∫C D
Enabled
∫∫D K
)
∫∫K L
;
∫∫L M
}
ªª 
}ºº çÇ
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Base\RepositoryBase.cs
	namespace		 	

Streetcode		
 
.		 
DAL		 
.		 
Repositories		 %
.		% &
Realizations		& 2
.		2 3
Base		3 7
;		7 8
public 
abstract 
class 
RepositoryBase $
<$ %
T% &
>& '
:( )
IRepositoryBase* 9
<9 :
T: ;
>; <
where 	
T
 
: 
class 
{ 
private 
readonly 
StreetcodeDbContext (

_dbContext) 3
;3 4
	protected 
RepositoryBase 
( 
StreetcodeDbContext 0
context1 8
)8 9
{ 

_dbContext 
= 
context 
; 
} 
public 


IQueryable 
< 
T 
> 
FindAll  
(  !

Expression! +
<+ ,
Func, 0
<0 1
T1 2
,2 3
bool4 8
>8 9
>9 :
?: ;
	predicate< E
=F G
defaultH O
)O P
{ 
return 
GetQueryable 
( 
	predicate %
)% &
.& '
AsNoTracking' 3
(3 4
)4 5
;5 6
} 
public 

T 
Create 
( 
T 
entity 
) 
{ 
return 

_dbContext 
. 
Set 
< 
T 
>  
(  !
)! "
." #
Add# &
(& '
entity' -
)- .
.. /
Entity/ 5
;5 6
} 
public 

async 
Task 
< 
T 
> 
CreateAsync $
($ %
T% &
entity' -
)- .
{   
var!! 
tmp!! 
=!! 
await!! 

_dbContext!! "
.!!" #
Set!!# &
<!!& '
T!!' (
>!!( )
(!!) *
)!!* +
.!!+ ,
AddAsync!!, 4
(!!4 5
entity!!5 ;
)!!; <
;!!< =
return"" 
tmp"" 
."" 
Entity"" 
;"" 
}## 
public%% 

Task%% 
CreateRangeAsync%%  
(%%  !
IEnumerable%%! ,
<%%, -
T%%- .
>%%. /
items%%0 5
)%%5 6
{&& 
return'' 

_dbContext'' 
.'' 
Set'' 
<'' 
T'' 
>''  
(''  !
)''! "
.''" #
AddRangeAsync''# 0
(''0 1
items''1 6
)''6 7
;''7 8
}(( 
public** 

EntityEntry** 
<** 
T** 
>** 
Update**  
(**  !
T**! "
entity**# )
)**) *
{++ 
return,, 

_dbContext,, 
.,, 
Set,, 
<,, 
T,, 
>,,  
(,,  !
),,! "
.,," #
Update,,# )
(,,) *
entity,,* 0
),,0 1
;,,1 2
}-- 
public// 

void// 
UpdateRange// 
(// 
IEnumerable// '
<//' (
T//( )
>//) *
items//+ 0
)//0 1
{00 

_dbContext11 
.11 
Set11 
<11 
T11 
>11 
(11 
)11 
.11 
UpdateRange11 '
(11' (
items11( -
)11- .
;11. /
}22 
public44 

void44 
Delete44 
(44 
T44 
entity44 
)44  
{55 

_dbContext66 
.66 
Set66 
<66 
T66 
>66 
(66 
)66 
.66 
Remove66 "
(66" #
entity66# )
)66) *
;66* +
}77 
public99 

void99 
DeleteRange99 
(99 
IEnumerable99 '
<99' (
T99( )
>99) *
items99+ 0
)990 1
{:: 

_dbContext;; 
.;; 
Set;; 
<;; 
T;; 
>;; 
(;; 
);; 
.;; 
RemoveRange;; '
(;;' (
items;;( -
);;- .
;;;. /
}<< 
public>> 

void>> 
Attach>> 
(>> 
T>> 
entity>> 
)>>  
{?? 

_dbContext@@ 
.@@ 
Set@@ 
<@@ 
T@@ 
>@@ 
(@@ 
)@@ 
.@@ 
Attach@@ "
(@@" #
entity@@# )
)@@) *
;@@* +
}AA 
publicCC 

EntityEntryCC 
<CC 
TCC 
>CC 
EntryCC 
(CC  
TCC  !
entityCC" (
)CC( )
{DD 
returnEE 

_dbContextEE 
.EE 
EntryEE 
(EE  
entityEE  &
)EE& '
;EE' (
}FF 
publicHH 

voidHH 
DetachHH 
(HH 
THH 
entityHH 
)HH  
{II 

_dbContextJJ 
.JJ 
EntryJJ 
(JJ 
entityJJ 
)JJ  
.JJ  !
StateJJ! &
=JJ' (
EntityStateJJ) 4
.JJ4 5
DetachedJJ5 =
;JJ= >
}KK 
publicMM 

TaskMM 
ExecuteSqlRawMM 
(MM 
stringMM $
queryMM% *
)MM* +
{NN 
returnOO 

_dbContextOO 
.OO 
DatabaseOO "
.OO" #
ExecuteSqlRawAsyncOO# 5
(OO5 6
queryOO6 ;
)OO; <
;OO< =
}PP 
publicRR 


IQueryableRR 
<RR 
TRR 
>RR 
IncludeRR  
(RR  !
paramsRR! '

ExpressionRR( 2
<RR2 3
FuncRR3 7
<RR7 8
TRR8 9
,RR9 :
objectRR; A
>RRA B
>RRB C
[RRC D
]RRD E
includesRRF N
)RRN O
{SS  
IIncludableQueryableTT 
<TT 
TTT 
,TT 
objectTT  &
>TT& '
?TT' (
queryTT) .
=TT/ 0
defaultTT1 8
;TT8 9
ifVV 

(VV 
includesVV 
.VV 
AnyVV 
(VV 
)VV 
)VV 
{WW 	
queryXX 
=XX 

_dbContextXX 
.XX 
SetXX "
<XX" #
TXX# $
>XX$ %
(XX% &
)XX& '
.XX' (
IncludeXX( /
(XX/ 0
includesXX0 8
[XX8 9
$numXX9 :
]XX: ;
)XX; <
;XX< =
}YY 	
for[[ 
([[ 
int[[ 

queryIndex[[ 
=[[ 
$num[[ 
;[[  

queryIndex[[! +
<[[, -
includes[[. 6
.[[6 7
Length[[7 =
;[[= >
++[[? A

queryIndex[[A K
)[[K L
{\\ 	
query]] 
=]] 
query]] 
!]] 
.]] 
Include]] "
(]]" #
includes]]# +
[]]+ ,

queryIndex]], 6
]]]6 7
)]]7 8
;]]8 9
}^^ 	
return`` 
(`` 
query`` 
is`` 
null`` 
)`` 
?``  

_dbContext``! +
.``+ ,
Set``, /
<``/ 0
T``0 1
>``1 2
(``2 3
)``3 4
:``5 6
query``7 <
.``< =
AsQueryable``= H
(``H I
)``I J
;``J K
}aa 
publiccc 

asynccc 
Taskcc 
<cc 
IEnumerablecc !
<cc! "
Tcc" #
>cc# $
>cc$ %
GetAllAsynccc& 1
(cc1 2

Expressiondd 
<dd 
Funcdd 
<dd 
Tdd 
,dd 
booldd 
>dd  
>dd  !
?dd! "
	predicatedd# ,
=dd- .
defaultdd/ 6
,dd6 7
Funcee 
<ee 

IQueryableee 
<ee 
Tee 
>ee 
,ee  
IIncludableQueryableee 0
<ee0 1
Tee1 2
,ee2 3
objectee4 :
>ee: ;
>ee; <
?ee< =
includeee> E
=eeF G
defaulteeH O
)eeO P
{ff 
returngg 
awaitgg 
GetQueryablegg !
(gg! "
	predicategg" +
,gg+ ,
includegg- 4
)gg4 5
.gg5 6
ToListAsyncgg6 A
(ggA B
)ggB C
;ggC D
}hh 
publicjj 

asyncjj 
Taskjj 
<jj 
IEnumerablejj !
<jj! "
Tjj" #
>jj# $
?jj$ %
>jj% &
GetAllAsyncjj' 2
(jj2 3

Expressionkk 
<kk 
Funckk 
<kk 
Tkk 
,kk 
Tkk 
>kk 
>kk 
selectorkk '
,kk' (

Expressionll 
<ll 
Funcll 
<ll 
Tll 
,ll 
boolll 
>ll  
>ll  !
?ll! "
	predicatell# ,
=ll- .
defaultll/ 6
,ll6 7
Funcmm 
<mm 

IQueryablemm 
<mm 
Tmm 
>mm 
,mm  
IIncludableQueryablemm 0
<mm0 1
Tmm1 2
,mm2 3
objectmm4 :
>mm: ;
>mm; <
?mm< =
includemm> E
=mmF G
defaultmmH O
)mmO P
{nn 
returnoo 
awaitoo 
GetQueryableoo !
(oo! "
	predicateoo" +
,oo+ ,
includeoo- 4
,oo4 5
selectoroo6 >
)oo> ?
.oo? @
ToListAsyncoo@ K
(ooK L
)ooL M
??ooN P
newooQ T
ListooU Y
<ooY Z
TooZ [
>oo[ \
(oo\ ]
)oo] ^
;oo^ _
}pp 
publicrr 

asyncrr 
Taskrr 
<rr 
Trr 
?rr 
>rr #
GetSingleOrDefaultAsyncrr 1
(rr1 2

Expressionss 
<ss 
Funcss 
<ss 
Tss 
,ss 
boolss 
>ss  
>ss  !
?ss! "
	predicatess# ,
=ss- .
defaultss/ 6
,ss6 7
Functt 
<tt 

IQueryablett 
<tt 
Ttt 
>tt 
,tt  
IIncludableQueryablett 0
<tt0 1
Ttt1 2
,tt2 3
objecttt4 :
>tt: ;
>tt; <
?tt< =
includett> E
=ttF G
defaultttH O
)ttO P
{uu 
returnvv 
awaitvv 
GetQueryablevv !
(vv! "
	predicatevv" +
,vv+ ,
includevv- 4
)vv4 5
.vv5 6 
SingleOrDefaultAsyncvv6 J
(vvJ K
)vvK L
;vvL M
}ww 
publicyy 

asyncyy 
Taskyy 
<yy 
Tyy 
?yy 
>yy "
GetFirstOrDefaultAsyncyy 0
(yy0 1

Expressionzz 
<zz 
Funczz 
<zz 
Tzz 
,zz 
boolzz 
>zz  
>zz  !
?zz! "
	predicatezz# ,
=zz- .
defaultzz/ 6
,zz6 7
Func{{ 
<{{ 

IQueryable{{ 
<{{ 
T{{ 
>{{ 
,{{  
IIncludableQueryable{{ 0
<{{0 1
T{{1 2
,{{2 3
object{{4 :
>{{: ;
>{{; <
?{{< =
include{{> E
={{F G
default{{H O
){{O P
{|| 
return}} 
await}} 
GetQueryable}} !
(}}! "
	predicate}}" +
,}}+ ,
include}}- 4
)}}4 5
.}}5 6
FirstOrDefaultAsync}}6 I
(}}I J
)}}J K
;}}K L
}~~ 
public
ÄÄ 

async
ÄÄ 
Task
ÄÄ 
<
ÄÄ 
T
ÄÄ 
?
ÄÄ 
>
ÄÄ $
GetFirstOrDefaultAsync
ÄÄ 0
(
ÄÄ0 1

Expression
ÅÅ 
<
ÅÅ 
Func
ÅÅ 
<
ÅÅ 
T
ÅÅ 
,
ÅÅ 
T
ÅÅ 
>
ÅÅ 
>
ÅÅ 
selector
ÅÅ '
,
ÅÅ' (

Expression
ÇÇ 
<
ÇÇ 
Func
ÇÇ 
<
ÇÇ 
T
ÇÇ 
,
ÇÇ 
bool
ÇÇ 
>
ÇÇ  
>
ÇÇ  !
?
ÇÇ! "
	predicate
ÇÇ# ,
=
ÇÇ- .
default
ÇÇ/ 6
,
ÇÇ6 7
Func
ÉÉ 
<
ÉÉ 

IQueryable
ÉÉ 
<
ÉÉ 
T
ÉÉ 
>
ÉÉ 
,
ÉÉ "
IIncludableQueryable
ÉÉ 0
<
ÉÉ0 1
T
ÉÉ1 2
,
ÉÉ2 3
object
ÉÉ4 :
>
ÉÉ: ;
>
ÉÉ; <
?
ÉÉ< =
include
ÉÉ> E
=
ÉÉF G
default
ÉÉH O
)
ÉÉO P
{
ÑÑ 
return
ÖÖ 
await
ÖÖ 
GetQueryable
ÖÖ !
(
ÖÖ! "
	predicate
ÖÖ" +
,
ÖÖ+ ,
include
ÖÖ- 4
,
ÖÖ4 5
selector
ÖÖ6 >
)
ÖÖ> ?
.
ÖÖ? @!
FirstOrDefaultAsync
ÖÖ@ S
(
ÖÖS T
)
ÖÖT U
;
ÖÖU V
}
ÜÜ 
private
àà 

IQueryable
àà 
<
àà 
T
àà 
>
àà 
GetQueryable
àà &
(
àà& '

Expression
ââ 
<
ââ 
Func
ââ 
<
ââ 
T
ââ 
,
ââ 
bool
ââ 
>
ââ  
>
ââ  !
?
ââ! "
	predicate
ââ# ,
=
ââ- .
default
ââ/ 6
,
ââ6 7
Func
ää 
<
ää 

IQueryable
ää 
<
ää 
T
ää 
>
ää 
,
ää "
IIncludableQueryable
ää 0
<
ää0 1
T
ää1 2
,
ää2 3
object
ää4 :
>
ää: ;
>
ää; <
?
ää< =
include
ää> E
=
ääF G
default
ääH O
,
ääO P

Expression
ãã 
<
ãã 
Func
ãã 
<
ãã 
T
ãã 
,
ãã 
T
ãã 
>
ãã 
>
ãã 
?
ãã 
selector
ãã  (
=
ãã) *
default
ãã+ 2
)
ãã2 3
{
åå 
var
çç 
query
çç 
=
çç 

_dbContext
çç 
.
çç 
Set
çç "
<
çç" #
T
çç# $
>
çç$ %
(
çç% &
)
çç& '
.
çç' (
AsNoTracking
çç( 4
(
çç4 5
)
çç5 6
;
çç6 7
if
èè 

(
èè 
include
èè 
is
èè 
not
èè 
null
èè 
)
èè  
{
êê 	
query
ëë 
=
ëë 
include
ëë 
(
ëë 
query
ëë !
)
ëë! "
;
ëë" #
}
íí 	
if
îî 

(
îî 
	predicate
îî 
is
îî 
not
îî 
null
îî !
)
îî! "
{
ïï 	
query
ññ 
=
ññ 
query
ññ 
.
ññ 
Where
ññ 
(
ññ  
	predicate
ññ  )
)
ññ) *
;
ññ* +
}
óó 	
if
ôô 

(
ôô 
selector
ôô 
is
ôô 
not
ôô 
null
ôô  
)
ôô  !
{
öö 	
query
õõ 
=
õõ 
query
õõ 
.
õõ 
Select
õõ  
(
õõ  !
selector
õõ! )
)
õõ) *
;
õõ* +
}
úú 	
return
ûû 
query
ûû 
.
ûû 
AsNoTracking
ûû !
(
ûû! "
)
ûû" #
;
ûû# $
}
üü 
}†† à
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\Analytics\StatisticRecordsRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
	Analytics3 <
{ 
public 

class &
StatisticRecordsRepository +
:, -
RepositoryBase. <
<< =
StatisticRecord= L
>L M
,M N&
IStatisticRecordRepositoryO i
{		 
public

 &
StatisticRecordsRepository

 )
(

) *
StreetcodeDbContext

* =
context

> E
)

E F
: 
base 
( 
context 
) 
{ 	
} 	
} 
} Ã
ïD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\AdditionalContent\TagRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
AdditionalContent3 D
;D E
public 
class 
TagRepository 
: 
RepositoryBase +
<+ ,
Tag, /
>/ 0
,0 1
ITagRepository2 @
{		 
public

 

TagRepository

 
(

 
StreetcodeDbContext

 ,
	dbContext

- 6
)

6 7
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} Â
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\AdditionalContent\SubtitleRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
AdditionalContent3 D
;D E
public 
class 
SubtitleRepository 
:  !
RepositoryBase" 0
<0 1
Subtitle1 9
>9 :
,: ;
ISubtitleRepository< O
{		 
public

 

SubtitleRepository

 
(

 
StreetcodeDbContext

 1
	dbContext

2 ;
)

; <
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ¶
§D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\AdditionalContent\StreetcodeTagIndexRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
AdditionalContent3 D
{ 
internal 
class (
StreetcodeTagIndexRepository /
:0 1
RepositoryBase2 @
<@ A
StreetcodeTagIndexA S
>S T
,T U)
IStreetcodeTagIndexRepositoryV s
{		 
public

 (
StreetcodeTagIndexRepository

 +
(

+ ,
StreetcodeDbContext

, ?
context

@ G
)

G H
: 
base 
( 
context 
) 
{ 	
} 	
} 
} °
¶D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Realizations\AdditionalContent\StreetcodeCoordinateRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &
Realizations& 2
.2 3
AdditionalContent3 D
;D E
public 
class *
StreetcodeCoordinateRepository +
:, -
RepositoryBase. <
<< = 
StreetcodeCoordinate= Q
>Q R
,R S+
IStreetcodeCoordinateRepositoryT s
{		 
public

 
*
StreetcodeCoordinateRepository

 )
(

) *
StreetcodeDbContext

* =
	dbContext

> G
)

G H
: 	
base
 
( 
	dbContext 
) 
{ 
} 
} ß
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Users\IUserRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Users1 6
{ 
public 

	interface 
IUserRepository $
:% &
IRepositoryBase' 6
<6 7
User7 ;
>; <
{ 
} 
}		 ¡
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Transactions\ITransactLinksRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Transactions1 =
;= >
public 
	interface $
ITransactLinksRepository )
:* +
IRepositoryBase, ;
<; <
TransactionLink< K
>K L
{ 
} •
èD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Toponyms\IToponymRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Toponyms1 9
;9 :
public 
	interface 
IToponymRepository #
:$ %
IRepositoryBase& 5
<5 6
Toponym6 =
>= >
{ 
} ‘
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Toponyms\IStreetcodeToponymRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Toponyms1 9
{ 
public 
	interface (
IStreetcodeToponymRepository .
:/ 0
IRepositoryBase1 @
<@ A
StreetcodeToponymA R
>R S
{ 
} 
}		 ¨
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Timeline\ITimelineRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Timeline1 9
;9 :
public 
	interface 
ITimelineRepository $
:% &
IRepositoryBase' 6
<6 7
TimelineItem7 C
>C D
{ 
} Ï
°D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Timeline\IHistoricalContextTimelineRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Timeline1 9
{ 
public 

	interface 0
$IHistoricalContextTimelineRepository 9
:: ;
IRepositoryBase< K
<K L%
HistoricalContextTimelineL e
>e f
{ 
} 
}		 ‘
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Timeline\IHistoricalContextRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Timeline1 9
{ 
public 

	interface (
IHistoricalContextRepository 1
:2 3
IRepositoryBase4 C
<C D
HistoricalContextD U
>U V
{ 
} 
}		 ´
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Team\ITeamRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Team1 5
{ 
public 

	interface 
ITeamRepository $
:% &
IRepositoryBase' 6
<6 7

TeamMember7 A
>A B
{ 
} 
}		 ƒ
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Team\ITeamPositionRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Team1 5
{ 
public 

	interface #
ITeamPositionRepository ,
:- .
IRepositoryBase/ >
<> ?
TeamMemberPositions? R
>R S
{ 
}		 
}

 ∑
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Team\ITeamLinkRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Team1 5
{ 
public 

	interface 
ITeamLinkRepository (
:) *
IRepositoryBase+ :
<: ;
TeamMemberLink; I
>I J
{ 
} 
}		 ≤
åD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Team\IPositionRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Team1 5
{ 
public 

	interface 
IPositionRepository (
:) *
IRepositoryBase+ :
<: ;
	Positions; D
>D E
{ 
} 
}		 ‘
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Streetcode\TextContent\ITextRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1

Streetcode1 ;
.; <
TextContent< G
;G H
public 
	interface 
ITextRepository  
:! "
IRepositoryBase# 2
<2 3
Text3 7
>7 8
{ 
} ‘
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Streetcode\TextContent\ITermRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1

Streetcode1 ;
.; <
TextContent< G
;G H
public 
	interface 
ITermRepository  
:! "
IRepositoryBase# 2
<2 3
Term3 7
>7 8
{ 
} ˙
°D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Streetcode\TextContent\IRelatedTermRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1

Streetcode1 ;
.; <
TextContent< G
{ 
public 

	interface "
IRelatedTermRepository +
:, -
IRepositoryBase. =
<= >
RelatedTerm> I
>I J
{ 
} 
}		 ‘
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Streetcode\TextContent\IFactRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1

Streetcode1 ;
.; <
TextContent< G
;G H
public 
	interface 
IFactRepository  
:! "
IRepositoryBase# 2
<2 3
Fact3 7
>7 8
{ 
} π
îD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Streetcode\IStreetcodeRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1

Streetcode1 ;
;; <
public 
	interface !
IStreetcodeRepository &
:' (
IRepositoryBase) 8
<8 9
StreetcodeContent9 J
>J K
{ 
} ª
óD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Streetcode\IRelatedFigureRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1

Streetcode1 ;
;; <
public 
	interface $
IRelatedFigureRepository )
:* +
IRepositoryBase, ;
<; <
RelatedFigure< I
>I J
{ 
} Ë
üD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Source\IStreetcodeCategoryContentRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Source1 7
{ 
public 

	interface 0
$IStreetcodeCategoryContentRepository 9
:: ;
IRepositoryBase< K
<K L%
StreetcodeCategoryContentL e
>e f
{ 
} 
}		 ∫
îD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Source\ISourceCategoryRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Source1 7
;7 8
public 
	interface %
ISourceCategoryRepository *
:+ ,
IRepositoryBase- <
<< =
SourceLinkCategory= O
>O P
{ 
} ‘
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Partners\IPartnerStreetcodeRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Partners1 9
{ 
public 

	interface (
IPartnerStreetcodeRepository 1
:2 3
IRepositoryBase4 C
<C D
StreetcodePartnerD U
>U V
{ 
} 
}		 ß
êD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Partners\IPartnersRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Partners1 9
;9 :
public 
	interface 
IPartnersRepository $
:% &
IRepositoryBase' 6
<6 7
Partner7 >
>> ?
{ 
} ‘
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Partners\IPartnerSourceLinkRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Partners1 9
{ 
public 

	interface (
IPartnerSourceLinkRepository 1
:2 3
IRepositoryBase4 C
<C D
PartnerSourceLinkD U
>U V
{ 
} 
}		 ß
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Newss\INewsRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Newss1 6
{ 
public 

	interface 
INewsRepository $
:% &
IRepositoryBase' 6
<6 7
News7 ;
>; <
{ 
} 
}		 ∞
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Media\IVideoRepository.cs
	namespace 	
Repositories
 
. 

Interfaces !
;! "
public 
	interface 
IVideoRepository !
:" #
IRepositoryBase$ 3
<3 4
Video4 9
>9 :
{ 
} Ú
õD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Media\Images\IStreetcodeImageRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Media1 6
.6 7
Images7 =
{ 
public 
	interface &
IStreetcodeImageRepository ,
:- .
IRepositoryBase/ >
<> ?
StreetcodeImage? N
>N O
{ 
} 
}		 €
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Media\Images\IStreetcodeArtRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Media1 6
.6 7
Images7 =
;= >
public 
	interface $
IStreetcodeArtRepository )
:* +
IRepositoryBase, ;
<; <
StreetcodeArt< I
>I J
{ 
} ∑
ëD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Media\Images\IImageRepository.cs
	namespace 	
Repositories
 
. 

Interfaces !
;! "
public 
	interface 
IImageRepository !
:" #
IRepositoryBase$ 3
<3 4
Image4 9
>9 :
{ 
} È
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Media\Images\IImageDetailsRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Media1 6
.6 7
Images7 =
{ 
public 

	interface #
IImageDetailsRepository ,
:- .
IRepositoryBase/ >
<> ?
ImageDetails? K
>K L
{ 
} 
}		 ±
èD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Media\Images\IArtRepository.cs
	namespace 	
Repositories
 
. 

Interfaces !
;! "
public 
	interface 
IArtRepository 
:  !
IRepositoryBase" 1
<1 2
Art2 5
>5 6
{ 
} ∞
äD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Media\IAudioRepository.cs
	namespace 	
Repositories
 
. 

Interfaces !
;! "
public 
	interface 
IAudioRepository !
:" #
IRepositoryBase$ 3
<3 4
Audio4 9
>9 :
{ 
} „*
ãD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Base\IRepositoryWrapper.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Base1 5
;5 6
public 
	interface 
IRepositoryWrapper #
{ 
IFactRepository 
FactRepository "
{# $
get% (
;( )
}* +
IArtRepository 
ArtRepository  
{! "
get# &
;& '
}( )$
IStreetcodeArtRepository #
StreetcodeArtRepository 4
{5 6
get7 :
;: ;
}< =
IVideoRepository 
VideoRepository $
{% &
get' *
;* +
}, -
IImageRepository 
ImageRepository $
{% &
get' *
;* +
}, -#
IImageDetailsRepository "
ImageDetailsRepository 2
{3 4
get5 8
;8 9
}: ;
IAudioRepository 
AudioRepository $
{% &
get' *
;* +
}, -+
IStreetcodeCoordinateRepository #*
StreetcodeCoordinateRepository$ B
{C D
getE H
;H I
}J K
IPartnersRepository 
PartnersRepository *
{+ ,
get- 0
;0 1
}2 3%
ISourceCategoryRepository $
SourceCategoryRepository 6
{7 8
get9 <
;< =
}> ?0
$IStreetcodeCategoryContentRepository (/
#StreetcodeCategoryContentRepository) L
{M N
getO R
;R S
}T U$
IRelatedFigureRepository #
RelatedFigureRepository 4
{5 6
get7 :
;: ;
}< =!
IStreetcodeRepository    
StreetcodeRepository   .
{  / 0
get  1 4
;  4 5
}  6 7
ISubtitleRepository!! 
SubtitleRepository!! *
{!!+ ,
get!!- 0
;!!0 1
}!!2 3&
IStatisticRecordRepository"" %
StatisticRecordRepository"" 8
{""9 :
get""; >
;""> ?
}""@ A
ITagRepository## 
TagRepository##  
{##! "
get### &
;##& '
}##( )
ITeamRepository$$ 
TeamRepository$$ "
{$$# $
get$$% (
;$$( )
}$$* +#
ITeamPositionRepository%% "
TeamPositionRepository%% 2
{%%3 4
get%%5 8
;%%8 9
}%%: ;
ITeamLinkRepository&& 
TeamLinkRepository&& *
{&&+ ,
get&&- 0
;&&0 1
}&&2 3
ITermRepository'' 
TermRepository'' "
{''# $
get''% (
;''( )
}''* +"
IRelatedTermRepository(( !
RelatedTermRepository(( 0
{((1 2
get((3 6
;((6 7
}((8 9
ITextRepository)) 
TextRepository)) "
{))# $
get))% (
;))( )
}))* +
ITimelineRepository** 
TimelineRepository** *
{**+ ,
get**- 0
;**0 1
}**2 3
IToponymRepository++ 
ToponymRepository++ (
{++) *
get+++ .
;++. /
}++0 1$
ITransactLinksRepository,, #
TransactLinksRepository,, 4
{,,5 6
get,,7 :
;,,: ;
},,< =(
IHistoricalContextRepository--  '
HistoricalContextRepository--! <
{--= >
get--? B
;--B C
}--D E(
IPartnerSourceLinkRepository..  '
PartnerSourceLinkRepository..! <
{..= >
get..? B
;..B C
}..D E
IUserRepository// 
UserRepository// "
{//# $
get//% (
;//( )
}//* +)
IStreetcodeTagIndexRepository00 !(
StreetcodeTagIndexRepository00" >
{00? @
get00A D
;00D E
}00F G(
IPartnerStreetcodeRepository11  '
PartnerStreetcodeRepository11! <
{11= >
get11? B
;11B C
}11E F
INewsRepository22 
NewsRepository22 "
{22# $
get22% (
;22( )
}22* +
IPositionRepository33 
PositionRepository33 *
{33+ ,
get33- 0
;330 1
}332 30
$IHistoricalContextTimelineRepository44 (/
#HistoricalContextTimelineRepository44) L
{44M N
get44O R
;44R S
}44T U(
IStreetcodeToponymRepository55  '
StreetcodeToponymRepository55! <
{55= >
get55? B
;55B C
}55D E&
IStreetcodeImageRepository66 %
StreetcodeImageRepository66 8
{669 :
get66; >
;66> ?
}66@ A
public77 

int77 
SaveChanges77 
(77 
)77 
;77 
public99 

Task99 
<99 
int99 
>99 
SaveChangesAsync99 %
(99% &
)99& '
;99' (
public;; 

TransactionScope;; 
BeginTransaction;; ,
(;;, -
);;- .
;;;. /
}<< æ6
àD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Base\IRepositoryBase.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
Base1 5
;5 6
public		 
	interface		 
IRepositoryBase		  
<		  !
T		! "
>		" #
where

 	
T


 
:

 
class

 
{ 

IQueryable 
< 
T 
> 
FindAll 
( 

Expression $
<$ %
Func% )
<) *
T* +
,+ ,
bool- 1
>1 2
>2 3
?3 4
	predicate5 >
=? @
defaultA H
)H I
;I J
T 
Create 
( 
T 
entity 
) 
; 
Task 
< 	
T	 

>
 
CreateAsync 
( 
T 
entity  
)  !
;! "
Task 
CreateRangeAsync	 
( 
IEnumerable %
<% &
T& '
>' (
items) .
). /
;/ 0
EntityEntry 
< 
T 
> 
Update 
( 
T 
entity "
)" #
;# $
public 

void 
UpdateRange 
( 
IEnumerable '
<' (
T( )
>) *
items+ 0
)0 1
;1 2
void 
Delete	 
( 
T 
entity 
) 
; 
void 
DeleteRange	 
( 
IEnumerable  
<  !
T! "
>" #
items$ )
)) *
;* +
void 
Attach	 
( 
T 
entity 
) 
; 
void 
Detach	 
( 
T 
entity 
) 
; 
EntityEntry   
<   
T   
>   
Entry   
(   
T   
entity   !
)  ! "
;  " #
public"" 

Task"" 
ExecuteSqlRaw"" 
("" 
string"" $
query""% *
)""* +
;""+ ,

IQueryable$$ 
<$$ 
T$$ 
>$$ 
Include$$ 
($$ 
params$$  

Expression$$! +
<$$+ ,
Func$$, 0
<$$0 1
T$$1 2
,$$2 3
object$$4 :
>$$: ;
>$$; <
[$$< =
]$$= >
includes$$? G
)$$G H
;$$H I
Task&& 
<&& 	
IEnumerable&&	 
<&& 
T&& 
>&& 
>&& 
GetAllAsync&& $
(&&$ %

Expression'' 
<'' 
Func'' 
<'' 
T'' 
,'' 
bool'' 
>''  
>''  !
?''! "
	predicate''# ,
=''- .
default''/ 6
,''6 7
Func(( 
<(( 

IQueryable(( 
<(( 
T(( 
>(( 
,((  
IIncludableQueryable(( 0
<((0 1
T((1 2
,((2 3
object((4 :
>((: ;
>((; <
?((< =
include((> E
=((F G
default((H O
)((O P
;((P Q
Task** 
<** 	
IEnumerable**	 
<** 
T** 
>** 
?** 
>** 
GetAllAsync** %
(**% &

Expression++ 
<++ 
Func++ 
<++ 
T++ 
,++ 
T++ 
>++ 
>++ 
selector++ '
,++' (

Expression,, 
<,, 
Func,, 
<,, 
T,, 
,,, 
bool,, 
>,,  
>,,  !
?,,! "
	predicate,,# ,
=,,- .
default,,/ 6
,,,6 7
Func-- 
<-- 

IQueryable-- 
<-- 
T-- 
>-- 
,--  
IIncludableQueryable-- 0
<--0 1
T--1 2
,--2 3
object--4 :
>--: ;
>--; <
?--< =
include--> E
=--F G
default--H O
)--O P
;--P Q
Task// 
<// 	
T//	 

?//
 
>// #
GetSingleOrDefaultAsync// $
(//$ %

Expression00 
<00 
Func00 
<00 
T00 
,00 
bool00 
>00  
>00  !
?00! "
	predicate00# ,
=00- .
default00/ 6
,006 7
Func11 
<11 

IQueryable11 
<11 
T11 
>11 
,11  
IIncludableQueryable11 0
<110 1
T111 2
,112 3
object114 :
>11: ;
>11; <
?11< =
include11> E
=11F G
default11H O
)11O P
;11P Q
Task33 
<33 	
T33	 

?33
 
>33 "
GetFirstOrDefaultAsync33 #
(33# $

Expression44 
<44 
Func44 
<44 
T44 
,44 
bool44 
>44  
>44  !
?44! "
	predicate44# ,
=44- .
default44/ 6
,446 7
Func55 
<55 

IQueryable55 
<55 
T55 
>55 
,55  
IIncludableQueryable55 0
<550 1
T551 2
,552 3
object554 :
>55: ;
>55; <
?55< =
include55> E
=55F G
default55H O
)55O P
;55P Q
Task77 
<77 	
T77	 

?77
 
>77 "
GetFirstOrDefaultAsync77 #
(77# $

Expression88 
<88 
Func88 
<88 
T88 
,88 
T88 
>88 
>88 
selector88 '
,88' (

Expression99 
<99 
Func99 
<99 
T99 
,99 
bool99 
>99  
>99  !
?99! "
	predicate99# ,
=99- .
default99/ 6
,996 7
Func:: 
<:: 

IQueryable:: 
<:: 
T:: 
>:: 
,::  
IIncludableQueryable:: 0
<::0 1
T::1 2
,::2 3
object::4 :
>::: ;
>::; <
?::< =
include::> E
=::F G
default::H O
)::O P
;::P Q
};; –
òD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\Analytics\IStatisticRecordRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
	Analytics1 :
{ 
public 

	interface &
IStatisticRecordRepository /
:0 1
IRepositoryBase2 A
<A B
StatisticRecordB Q
>Q R
{ 
} 
}		 ´
îD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\AdditionalContent\ITagRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
AdditionalContent1 B
;B C
public 
	interface 
ITagRepository 
:  !
IRepositoryBase" 1
<1 2
Tag2 5
>5 6
{ 
} ∫
ôD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\AdditionalContent\ISubtitleRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
AdditionalContent1 B
;B C
public 
	interface 
ISubtitleRepository $
:% &
IRepositoryBase' 6
<6 7
Subtitle7 ?
>? @
{ 
} È
£D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\AdditionalContent\IStreetcodeTagIndexRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
AdditionalContent1 B
{ 
public 

	interface )
IStreetcodeTagIndexRepository 2
:3 4
IRepositoryBase5 D
<D E
StreetcodeTagIndexE W
>W X
{ 
} 
}		 ﬁ
•D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Repositories\Interfaces\AdditionalContent\IStreetcodeCoordinateRepository.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Repositories %
.% &

Interfaces& 0
.0 1
AdditionalContent1 B
;B C
public 
	interface +
IStreetcodeCoordinateRepository 0
:1 2
IRepositoryBase3 B
<B C 
StreetcodeCoordinateC W
>W X
{ 
} ∂›
{D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Persistence\StreetcodeDbContext.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Persistence $
;$ %
public 
class 
StreetcodeDbContext  
:! "
	DbContext# ,
{ 
public 

StreetcodeDbContext 
( 
)  
{ 
} 
public 

StreetcodeDbContext 
( 
DbContextOptions /
</ 0
StreetcodeDbContext0 C
>C D
optionsE L
)L M
:   	
base  
 
(   
options   
)   
{!! 
}"" 
public$$ 

DbSet$$ 
<$$ 
Art$$ 
>$$ 
Arts$$ 
{$$ 
get$$  
;$$  !
set$$" %
;$$% &
}$$' (
public%% 

DbSet%% 
<%% 
Audio%% 
>%% 
Audios%% 
{%%  
get%%! $
;%%$ %
set%%& )
;%%) *
}%%+ ,
public&& 

DbSet&& 
<&& 
ToponymCoordinate&& "
>&&" #
ToponymCoordinates&&$ 6
{&&7 8
get&&9 <
;&&< =
set&&> A
;&&A B
}&&C D
public'' 

DbSet'' 
<''  
StreetcodeCoordinate'' %
>''% &!
StreetcodeCoordinates''' <
{''= >
get''? B
;''B C
set''D G
;''G H
}''I J
public(( 

DbSet(( 
<(( 
Fact(( 
>(( 
Facts(( 
{(( 
get(( "
;((" #
set(($ '
;((' (
}(() *
public)) 

DbSet)) 
<)) 
HistoricalContext)) "
>))" #
HistoricalContexts))$ 6
{))7 8
get))9 <
;))< =
set))> A
;))A B
}))C D
public** 

DbSet** 
<** 
Image** 
>** 
Images** 
{**  
get**! $
;**$ %
set**& )
;**) *
}**+ ,
public++ 

DbSet++ 
<++ 
ImageDetails++ 
>++ 
ImageDetailses++ -
{++. /
get++0 3
;++3 4
set++5 8
;++8 9
}++: ;
public,, 

DbSet,, 
<,, 
Partner,, 
>,, 
Partners,, "
{,,# $
get,,% (
;,,( )
set,,* -
;,,- .
},,/ 0
public-- 

DbSet-- 
<-- 
PartnerSourceLink-- "
>--" #
PartnerSourceLinks--$ 6
{--7 8
get--9 <
;--< =
set--> A
;--A B
}--C D
public.. 

DbSet.. 
<.. 
RelatedFigure.. 
>.. 
RelatedFigures..  .
{../ 0
get..1 4
;..4 5
set..6 9
;..9 :
}..; <
public// 

DbSet// 
<// 
Response// 
>// 
	Responses// $
{//% &
get//' *
;//* +
set//, /
;/// 0
}//1 2
public00 

DbSet00 
<00 
StreetcodeContent00 "
>00" #
Streetcodes00$ /
{000 1
get002 5
;005 6
set007 :
;00: ;
}00< =
public11 

DbSet11 
<11 
Subtitle11 
>11 
	Subtitles11 $
{11% &
get11' *
;11* +
set11, /
;11/ 0
}111 2
public22 

DbSet22 
<22 
StatisticRecord22  
>22  !
StatisticRecords22" 2
{223 4
get225 8
;228 9
set22: =
;22= >
}22? @
public33 

DbSet33 
<33 
Tag33 
>33 
Tags33 
{33 
get33  
;33  !
set33" %
;33% &
}33' (
public44 

DbSet44 
<44 
Term44 
>44 
Terms44 
{44 
get44 "
;44" #
set44$ '
;44' (
}44) *
public55 

DbSet55 
<55 
RelatedTerm55 
>55 
RelatedTerms55 *
{55+ ,
get55- 0
;550 1
set552 5
;555 6
}557 8
public66 

DbSet66 
<66 
Text66 
>66 
Texts66 
{66 
get66 "
;66" #
set66$ '
;66' (
}66) *
public77 

DbSet77 
<77 
TimelineItem77 
>77 
TimelineItems77 ,
{77- .
get77/ 2
;772 3
set774 7
;777 8
}779 :
public88 

DbSet88 
<88 
Toponym88 
>88 
Toponyms88 "
{88# $
get88% (
;88( )
set88* -
;88- .
}88/ 0
public99 

DbSet99 
<99 
TransactionLink99  
>99  !
TransactionLinks99" 2
{993 4
get995 8
;998 9
set99: =
;99= >
}99? @
public:: 

DbSet:: 
<:: 
Video:: 
>:: 
Videos:: 
{::  
get::! $
;::$ %
set::& )
;::) *
}::+ ,
public;; 

DbSet;; 
<;; %
StreetcodeCategoryContent;; *
>;;* +%
StreetcodeCategoryContent;;, E
{;;F G
get;;H K
;;;K L
set;;M P
;;;P Q
};;R S
public<< 

DbSet<< 
<<< 
StreetcodeArt<< 
><< 
StreetcodeArts<<  .
{<</ 0
get<<1 4
;<<4 5
set<<6 9
;<<9 :
}<<; <
public== 

DbSet== 
<== 
User== 
>== 
Users== 
{== 
get== "
;==" #
set==$ '
;==' (
}==) *
public>> 

DbSet>> 
<>> 
StreetcodeTagIndex>> #
>>># $ 
StreetcodeTagIndices>>% 9
{>>: ;
get>>< ?
;>>? @
set>>A D
;>>D E
}>>F G
public?? 

DbSet?? 
<?? 

TeamMember?? 
>?? 
TeamMembers?? (
{??) *
get??+ .
;??. /
set??0 3
;??3 4
}??5 6
public@@ 

DbSet@@ 
<@@ 
TeamMemberLink@@ 
>@@  
TeamMemberLinks@@! 0
{@@1 2
get@@3 6
;@@6 7
set@@8 ;
;@@; <
}@@= >
publicAA 

DbSetAA 
<AA 
	PositionsAA 
>AA 
	PositionsAA %
{AA& '
getAA( +
;AA+ ,
setAA- 0
;AA0 1
}AA2 3
publicBB 

DbSetBB 
<BB 
NewsBB 
>BB 
NewsBB 
{BB 
getBB !
;BB! "
setBB# &
;BB& '
}BB( )
publicCC 

DbSetCC 
<CC 
SourceLinkCategoryCC #
>CC# $
SourceLinksCC% 0
{CC1 2
getCC3 6
;CC6 7
setCC8 ;
;CC; <
}CC= >
publicDD 

DbSetDD 
<DD 
StreetcodeImageDD  
>DD  !
StreetcodeImagesDD" 2
{DD3 4
getDD5 8
;DD8 9
setDD: =
;DD= >
}DD? @
publicEE 

DbSetEE 
<EE %
HistoricalContextTimelineEE *
>EE* +'
HistoricalContextsTimelinesEE, G
{EEH I
getEEJ M
;EEM N
setEEO R
;EER S
}EET U
publicFF 

DbSetFF 
<FF 
StreetcodePartnerFF "
>FF" #
StreetcodePartnersFF$ 6
{FF7 8
getFF9 <
;FF< =
setFF> A
;FFA B
}FFC D
publicGG 

DbSetGG 
<GG 
TeamMemberPositionsGG $
>GG$ %
TeamMemberPositionGG& 8
{GG9 :
getGG; >
;GG> ?
setGG@ C
;GGC D
}GGE F
	protectedII 
overrideII 
voidII 
OnModelCreatingII +
(II+ ,
ModelBuilderII, 8
modelBuilderII9 E
)IIE F
{JJ 
baseKK 
.KK 
OnModelCreatingKK 
(KK 
modelBuilderKK )
)KK) *
;KK* +
modelBuilderMM 
.MM 
UseCollationMM !
(MM! "
$strMM" >
)MM> ?
;MM? @
modelBuilderOO 
.OO 
EntityOO 
<OO 
StatisticRecordOO +
>OO+ ,
(OO, -
)OO- .
.PP 
HasOnePP 
(PP 
xPP 
=>PP 
xPP 
.PP  
StreetcodeCoordinatePP 1
)PP1 2
.QQ 
WithOneQQ 
(QQ 
xQQ 
=>QQ 
xQQ 
.QQ 
StatisticRecordQQ -
)QQ- .
.RR 
HasForeignKeyRR 
<RR 
StatisticRecordRR ,
>RR, -
(RR- .
xRR. /
=>RR0 2
xRR3 4
.RR4 5"
StreetcodeCoordinateIdRR5 K
)RRK L
;RRL M
modelBuilderTT 
.TT 
EntityTT 
<TT 
NewsTT  
>TT  !
(TT! "
)TT" #
.UU 
HasOneUU 
(UU 
xUU 
=>UU 
xUU 
.UU 
ImageUU  
)UU  !
.VV 
WithOneVV 
(VV 
xVV 
=>VV 
xVV 
.VV 
NewsVV  
)VV  !
.WW 
HasForeignKeyWW 
<WW 
NewsWW 
>WW  
(WW  !
xWW! "
=>WW# %
xWW& '
.WW' (
ImageIdWW( /
)WW/ 0
;WW0 1
modelBuilderYY 
.YY 
EntityYY 
<YY 

TeamMemberYY &
>YY& '
(YY' (
)YY( )
.ZZ 
HasOneZZ 
(ZZ 
xZZ 
=>ZZ 
xZZ 
.ZZ 
ImageZZ  
)ZZ  !
.[[ 
WithOne[[ 
([[ 
x[[ 
=>[[ 
x[[ 
.[[ 

TeamMember[[ &
)[[& '
.\\ 
HasForeignKey\\ 
<\\ 

TeamMember\\ %
>\\% &
(\\& '
x\\' (
=>\\) +
x\\, -
.\\- .
ImageId\\. 5
)\\5 6
;\\6 7
modelBuilder^^ 
.^^ 
Entity^^ 
<^^ 

TeamMember^^ &
>^^& '
(^^' (
)^^( )
.__ 
HasMany__ 
(__ 
x__ 
=>__ 
x__ 
.__ 
	Positions__ %
)__% &
.`` 
WithMany`` 
(`` 
x`` 
=>`` 
x`` 
.`` 
TeamMembers`` (
)``( )
.aa 
UsingEntityaa 
<aa 
TeamMemberPositionsaa ,
>aa, -
(aa- .
tpbb 
=>bb 
tpbb 
.bb 
HasOnebb 
(bb 
xbb 
=>bb  
xbb! "
.bb" #
	Positionsbb# ,
)bb, -
.bb- .
WithManybb. 6
(bb6 7
)bb7 8
.bb8 9
HasForeignKeybb9 F
(bbF G
xbbG H
=>bbI K
xbbL M
.bbM N
PositionsIdbbN Y
)bbY Z
,bbZ [
tpcc 
=>cc 
tpcc 
.cc 
HasOnecc 
(cc 
xcc 
=>cc  
xcc! "
.cc" #

TeamMembercc# -
)cc- .
.cc. /
WithManycc/ 7
(cc7 8
)cc8 9
.cc9 :
HasForeignKeycc: G
(ccG H
xccH I
=>ccJ L
xccM N
.ccN O
TeamMemberIdccO [
)cc[ \
)cc\ ]
;cc] ^
modelBuilderee 
.ee 
Entityee 
<ee 

TeamMemberee &
>ee& '
(ee' (
)ee( )
.ff 
HasManyff 
(ff 
xff 
=>ff 
xff 
.ff 
TeamMemberLinksff +
)ff+ ,
.gg 
WithOnegg 
(gg 
xgg 
=>gg 
xgg 
.gg 

TeamMembergg &
)gg& '
.hh 
HasForeignKeyhh 
(hh 
xhh 
=>hh 
xhh  !
.hh! "
TeamMemberIdhh" .
)hh. /
;hh/ 0
modelBuilderjj 
.jj 
Entityjj 
<jj 
TeamMemberPositionsjj /
>jj/ 0
(jj0 1
)jj1 2
.kk 
HasKeykk 
(kk 
nameofkk 
(kk 
TeamMemberPositionskk .
.kk. /
TeamMemberIdkk/ ;
)kk; <
,kk< =
nameofkk> D
(kkD E
TeamMemberPositionskkE X
.kkX Y
PositionsIdkkY d
)kkd e
)kke f
;kkf g
modelBuildermm 
.mm 
Entitymm 
<mm 
Tagmm 
>mm  
(mm  !
)mm! "
.nn 
HasManynn 
(nn 
tnn 
=>nn 
tnn 
.nn 
Streetcodesnn '
)nn' (
.oo 
WithManyoo 
(oo 
soo 
=>oo 
soo 
.oo 
Tagsoo !
)oo! "
.pp 
UsingEntitypp 
<pp 
StreetcodeTagIndexpp +
>pp+ ,
(pp, -
spqq 
=>qq 
spqq 
.qq 
HasOneqq 
(qq 
xqq 
=>qq  
xqq! "
.qq" #

Streetcodeqq# -
)qq- .
.qq. /
WithManyqq/ 7
(qq7 8
xqq8 9
=>qq: <
xqq= >
.qq> ? 
StreetcodeTagIndicesqq? S
)qqS T
.qqT U
HasForeignKeyqqU b
(qqb c
xqqc d
=>qqe g
xqqh i
.qqi j
StreetcodeIdqqj v
)qqv w
,qqw x
sprr 
=>rr 
sprr 
.rr 
HasOnerr 
(rr 
xrr 
=>rr  
xrr! "
.rr" #
Tagrr# &
)rr& '
.rr' (
WithManyrr( 0
(rr0 1
xrr1 2
=>rr3 5
xrr6 7
.rr7 8 
StreetcodeTagIndicesrr8 L
)rrL M
.rrM N
HasForeignKeyrrN [
(rr[ \
xrr\ ]
=>rr^ `
xrra b
.rrb c
TagIdrrc h
)rrh i
)rri j
;rrj k
modelBuildertt 
.tt 
Entitytt 
<tt 
StreetcodeTagIndextt .
>tt. /
(tt/ 0
)tt0 1
.uu 
HasKeyuu 
(uu 
nameofuu 
(uu 
StreetcodeTagIndexuu ,
.uu, -
StreetcodeIduu- 9
)uu9 :
,uu: ;
nameofuu< B
(uuB C
StreetcodeTagIndexuuC U
.uuU V
TagIduuV [
)uu[ \
)uu\ ]
;uu] ^
modelBuilderww 
.ww 
Entityww 
<ww 
Toponymww #
>ww# $
(ww$ %
)ww% &
.xx 
HasOnexx 
(xx 
dxx 
=>xx 
dxx 
.xx 

Coordinatexx %
)xx% &
.yy 
WithOneyy 
(yy 
pyy 
=>yy 
pyy 
.yy 
Toponymyy #
)yy# $
.zz 
OnDeletezz 
(zz 
DeleteBehaviorzz $
.zz$ %
Cascadezz% ,
)zz, -
;zz- .
modelBuilder|| 
.|| 
Entity|| 
<|| 
Partner|| #
>||# $
(||$ %
entity||% +
=>||, .
{}} 	
entity~~ 
.~~ 
HasMany~~ 
(~~ 
d~~ 
=>~~ 
d~~  !
.~~! "
PartnerSourceLinks~~" 4
)~~4 5
. 
WithOne 
( 
p 
=> 
p 
.  
Partner  '
)' (
.
ÄÄ 
HasForeignKey
ÄÄ 
(
ÄÄ 
d
ÄÄ  
=>
ÄÄ! #
d
ÄÄ$ %
.
ÄÄ% &
	PartnerId
ÄÄ& /
)
ÄÄ/ 0
.
ÅÅ 
OnDelete
ÅÅ 
(
ÅÅ 
DeleteBehavior
ÅÅ (
.
ÅÅ( )
Cascade
ÅÅ) 0
)
ÅÅ0 1
;
ÅÅ1 2
entity
ÉÉ 
.
ÉÉ 
Property
ÉÉ 
(
ÉÉ 
p
ÉÉ 
=>
ÉÉ  
p
ÉÉ! "
.
ÉÉ" #
IsKeyPartner
ÉÉ# /
)
ÉÉ/ 0
.
ÑÑ 
HasDefaultValue
ÑÑ  
(
ÑÑ  !
$str
ÑÑ! (
)
ÑÑ( )
;
ÑÑ) *
}
ÖÖ 	
)
ÖÖ	 

;
ÖÖ
 
modelBuilder
áá 
.
áá 
Entity
áá 
<
áá '
HistoricalContextTimeline
áá 5
>
áá5 6
(
áá6 7
)
áá7 8
.
àà 
HasKey
àà 
(
àà 
ht
àà 
=>
àà 
new
àà 
{
àà  
ht
àà! #
.
àà# $

TimelineId
àà$ .
,
àà. /
ht
àà0 2
.
àà2 3!
HistoricalContextId
àà3 F
}
ààG H
)
ààH I
;
ààI J
modelBuilder
ââ 
.
ââ 
Entity
ââ 
<
ââ '
HistoricalContextTimeline
ââ 5
>
ââ5 6
(
ââ6 7
)
ââ7 8
.
ää 
HasOne
ää 
(
ää 
ht
ää 
=>
ää 
ht
ää 
.
ää 
Timeline
ää %
)
ää% &
.
ãã 
WithMany
ãã 
(
ãã 
x
ãã 
=>
ãã 
x
ãã 
.
ãã (
HistoricalContextTimelines
ãã 7
)
ãã7 8
.
åå 
HasForeignKey
åå 
(
åå 
x
åå 
=>
åå 
x
åå  !
.
åå! "

TimelineId
åå" ,
)
åå, -
;
åå- .
modelBuilder
çç 
.
çç 
Entity
çç 
<
çç '
HistoricalContextTimeline
çç 5
>
çç5 6
(
çç6 7
)
çç7 8
.
éé 
HasOne
éé 
(
éé 
ht
éé 
=>
éé 
ht
éé 
.
éé 
HistoricalContext
éé .
)
éé. /
.
èè 
WithMany
èè 
(
èè 
x
èè 
=>
èè 
x
èè 
.
èè (
HistoricalContextTimelines
èè 7
)
èè7 8
.
êê 
HasForeignKey
êê 
(
êê 
x
êê 
=>
êê 
x
êê  !
.
êê! "!
HistoricalContextId
êê" 5
)
êê5 6
;
êê6 7
modelBuilder
íí 
.
íí 
Entity
íí 
<
íí  
SourceLinkCategory
íí .
>
íí. /
(
íí/ 0
)
íí0 1
.
ìì 
HasMany
ìì 
(
ìì 
d
ìì 
=>
ìì 
d
ìì 
.
ìì (
StreetcodeCategoryContents
ìì 6
)
ìì6 7
.
îî 
WithOne
îî 
(
îî 
p
îî 
=>
îî 
p
îî 
.
îî  
SourceLinkCategory
îî .
)
îî. /
.
ïï 
HasForeignKey
ïï 
(
ïï 
d
ïï 
=>
ïï 
d
ïï  !
.
ïï! ""
SourceLinkCategoryId
ïï" 6
)
ïï6 7
.
ññ 
OnDelete
ññ 
(
ññ 
DeleteBehavior
ññ $
.
ññ$ %
Cascade
ññ% ,
)
ññ, -
;
ññ- .
modelBuilder
òò 
.
òò 
Entity
òò 
<
òò 
Image
òò !
>
òò! "
(
òò" #
entity
òò# )
=>
òò* ,
{
ôô 	
entity
öö 
.
öö 
HasOne
öö 
(
öö 
d
öö 
=>
öö 
d
öö  
.
öö  !
Art
öö! $
)
öö$ %
.
õõ 
WithOne
õõ 
(
õõ 
a
õõ 
=>
õõ 
a
õõ 
.
õõ  
Image
õõ  %
)
õõ% &
.
úú 
HasForeignKey
úú 
<
úú 
Art
úú "
>
úú" #
(
úú# $
a
úú$ %
=>
úú& (
a
úú) *
.
úú* +
ImageId
úú+ 2
)
úú2 3
.
ùù 
OnDelete
ùù 
(
ùù 
DeleteBehavior
ùù (
.
ùù( )
Cascade
ùù) 0
)
ùù0 1
;
ùù1 2
entity
üü 
.
üü 
HasOne
üü 
(
üü 
im
üü 
=>
üü 
im
üü  "
.
üü" #
ImageDetails
üü# /
)
üü/ 0
.
†† 
WithOne
†† 
(
†† 
info
†† 
=>
††  
info
††! %
.
††% &
Image
††& +
)
††+ ,
.
°° 
HasForeignKey
°° 
<
°° 
ImageDetails
°° +
>
°°+ ,
(
°°, -
a
°°- .
=>
°°/ 1
a
°°2 3
.
°°3 4
ImageId
°°4 ;
)
°°; <
.
¢¢ 
OnDelete
¢¢ 
(
¢¢ 
DeleteBehavior
¢¢ (
.
¢¢( )
Cascade
¢¢) 0
)
¢¢0 1
;
¢¢1 2
entity
§§ 
.
§§ 
HasOne
§§ 
(
§§ 
d
§§ 
=>
§§ 
d
§§  
.
§§  !
Partner
§§! (
)
§§( )
.
•• 
WithOne
•• 
(
•• 
p
•• 
=>
•• 
p
•• 
.
••  
Logo
••  $
)
••$ %
.
¶¶ 
HasForeignKey
¶¶ 
<
¶¶ 
Partner
¶¶ &
>
¶¶& '
(
¶¶' (
d
¶¶( )
=>
¶¶* ,
d
¶¶- .
.
¶¶. /
LogoId
¶¶/ 5
)
¶¶5 6
.
ßß 
OnDelete
ßß 
(
ßß 
DeleteBehavior
ßß (
.
ßß( )
Cascade
ßß) 0
)
ßß0 1
;
ßß1 2
entity
©© 
.
©© 
HasMany
©© 
(
©© 
d
©© 
=>
©© 
d
©©  !
.
©©! "
Facts
©©" '
)
©©' (
.
™™ 
WithOne
™™ 
(
™™ 
p
™™ 
=>
™™ 
p
™™ 
.
™™  
Image
™™  %
)
™™% &
.
´´ 
HasForeignKey
´´ 
(
´´ 
d
´´  
=>
´´! #
d
´´$ %
.
´´% &
ImageId
´´& -
)
´´- .
.
¨¨ 
OnDelete
¨¨ 
(
¨¨ 
DeleteBehavior
¨¨ (
.
¨¨( )
Cascade
¨¨) 0
)
¨¨0 1
;
¨¨1 2
entity
ÆÆ 
.
ÆÆ 
HasMany
ÆÆ 
(
ÆÆ 
i
ÆÆ 
=>
ÆÆ 
i
ÆÆ  !
.
ÆÆ! ""
SourceLinkCategories
ÆÆ" 6
)
ÆÆ6 7
.
ØØ 
WithOne
ØØ 
(
ØØ 
s
ØØ 
=>
ØØ 
s
ØØ 
.
ØØ  
Image
ØØ  %
)
ØØ% &
.
∞∞ 
HasForeignKey
∞∞ 
(
∞∞ 
d
∞∞  
=>
∞∞! #
d
∞∞$ %
.
∞∞% &
ImageId
∞∞& -
)
∞∞- .
.
±± 
OnDelete
±± 
(
±± 
DeleteBehavior
±± (
.
±±( )
Cascade
±±) 0
)
±±0 1
;
±±1 2
}
≤≤ 	
)
≤≤	 

;
≤≤
 
modelBuilder
¥¥ 
.
¥¥ 
Entity
¥¥ 
<
¥¥ 
RelatedFigure
¥¥ )
>
¥¥) *
(
¥¥* +
entity
¥¥+ 1
=>
¥¥2 4
{
µµ 	
entity
∂∂ 
.
∂∂ 
HasKey
∂∂ 
(
∂∂ 
d
∂∂ 
=>
∂∂ 
new
∂∂ "
{
∂∂# $
d
∂∂% &
.
∂∂& '

ObserverId
∂∂' 1
,
∂∂1 2
d
∂∂3 4
.
∂∂4 5
TargetId
∂∂5 =
}
∂∂> ?
)
∂∂? @
;
∂∂@ A
entity
∏∏ 
.
∏∏ 
HasOne
∏∏ 
(
∏∏ 
d
∏∏ 
=>
∏∏ 
d
∏∏  
.
∏∏  !
Observer
∏∏! )
)
∏∏) *
.
ππ 
WithMany
ππ 
(
ππ 
d
ππ 
=>
ππ 
d
ππ  
.
ππ  !
	Observers
ππ! *
)
ππ* +
.
∫∫ 
HasForeignKey
∫∫ 
(
∫∫ 
d
∫∫  
=>
∫∫! #
d
∫∫$ %
.
∫∫% &

ObserverId
∫∫& 0
)
∫∫0 1
.
ªª 
OnDelete
ªª 
(
ªª 
DeleteBehavior
ªª (
.
ªª( )
Restrict
ªª) 1
)
ªª1 2
;
ªª2 3
entity
ΩΩ 
.
ΩΩ 
HasOne
ΩΩ 
(
ΩΩ 
d
ΩΩ 
=>
ΩΩ 
d
ΩΩ  
.
ΩΩ  !
Target
ΩΩ! '
)
ΩΩ' (
.
ææ 
WithMany
ææ 
(
ææ 
d
ææ 
=>
ææ 
d
ææ  
.
ææ  !
Targets
ææ! (
)
ææ( )
.
øø 
HasForeignKey
øø 
(
øø 
d
øø  
=>
øø! #
d
øø$ %
.
øø% &
TargetId
øø& .
)
øø. /
.
¿¿ 
OnDelete
¿¿ 
(
¿¿ 
DeleteBehavior
¿¿ (
.
¿¿( )
Cascade
¿¿) 0
)
¿¿0 1
;
¿¿1 2
}
¡¡ 	
)
¡¡	 

;
¡¡
 
modelBuilder
√√ 
.
√√ 
Entity
√√ 
<
√√ 
StreetcodeArt
√√ )
>
√√) *
(
√√* +
entity
√√+ 1
=>
√√2 4
{
ƒƒ 	
entity
≈≈ 
.
≈≈ 
HasKey
≈≈ 
(
≈≈ 
d
≈≈ 
=>
≈≈ 
new
≈≈ "
{
≈≈# $
d
≈≈% &
.
≈≈& '
ArtId
≈≈' ,
,
≈≈, -
d
≈≈. /
.
≈≈/ 0
StreetcodeId
≈≈0 <
}
≈≈= >
)
≈≈> ?
;
≈≈? @
entity
«« 
.
«« 
HasOne
«« 
(
«« 
d
«« 
=>
«« 
d
««  
.
««  !

Streetcode
««! +
)
««+ ,
.
»» 
WithMany
»» 
(
»» 
d
»» 
=>
»» 
d
»»  
.
»»  !
StreetcodeArts
»»! /
)
»»/ 0
.
…… 
HasForeignKey
…… 
(
…… 
d
……  
=>
……! #
d
……$ %
.
……% &
StreetcodeId
……& 2
)
……2 3
.
   
OnDelete
   
(
   
DeleteBehavior
   (
.
  ( )
Cascade
  ) 0
)
  0 1
;
  1 2
entity
ÃÃ 
.
ÃÃ 
HasOne
ÃÃ 
(
ÃÃ 
d
ÃÃ 
=>
ÃÃ 
d
ÃÃ  
.
ÃÃ  !
Art
ÃÃ! $
)
ÃÃ$ %
.
ÕÕ 
WithMany
ÕÕ 
(
ÕÕ 
d
ÕÕ 
=>
ÕÕ 
d
ÕÕ  
.
ÕÕ  !
StreetcodeArts
ÕÕ! /
)
ÕÕ/ 0
.
ŒŒ 
HasForeignKey
ŒŒ 
(
ŒŒ 
d
ŒŒ  
=>
ŒŒ! #
d
ŒŒ$ %
.
ŒŒ% &
ArtId
ŒŒ& +
)
ŒŒ+ ,
.
œœ 
OnDelete
œœ 
(
œœ 
DeleteBehavior
œœ (
.
œœ( )
Cascade
œœ) 0
)
œœ0 1
;
œœ1 2
entity
—— 
.
—— 
Property
—— 
(
—— 
e
—— 
=>
——  
e
——! "
.
——" #
Index
——# (
)
——( )
.
““ 
HasDefaultValue
““  
(
““  !
$num
““! "
)
““" #
;
““# $
entity
‘‘ 
.
’’ 
HasIndex
’’ 
(
’’ 
d
’’ 
=>
’’ 
new
’’ "
{
’’# $
d
’’% &
.
’’& '
ArtId
’’' ,
,
’’, -
d
’’. /
.
’’/ 0
StreetcodeId
’’0 <
}
’’= >
)
’’> ?
.
÷÷ 
IsUnique
÷÷ 
(
÷÷ 
false
÷÷ 
)
÷÷  
;
÷÷  !
}
◊◊ 	
)
◊◊	 

;
◊◊
 
modelBuilder
ŸŸ 
.
ŸŸ 
Entity
ŸŸ 
<
ŸŸ 
StreetcodeContent
ŸŸ -
>
ŸŸ- .
(
ŸŸ. /
entity
ŸŸ/ 5
=>
ŸŸ6 8
{
⁄⁄ 	
entity
€€ 
.
€€ 
Property
€€ 
(
€€ 
s
€€ 
=>
€€  
s
€€! "
.
€€" #
	CreatedAt
€€# ,
)
€€, -
.
‹‹  
HasDefaultValueSql
‹‹ #
(
‹‹# $
$str
‹‹$ /
)
‹‹/ 0
;
‹‹0 1
entity
ﬁﬁ 
.
ﬁﬁ 
Property
ﬁﬁ 
(
ﬁﬁ 
s
ﬁﬁ 
=>
ﬁﬁ  
s
ﬁﬁ! "
.
ﬁﬁ" #
	UpdatedAt
ﬁﬁ# ,
)
ﬁﬁ, -
.
ﬂﬂ  
HasDefaultValueSql
ﬂﬂ #
(
ﬂﬂ# $
$str
ﬂﬂ$ /
)
ﬂﬂ/ 0
;
ﬂﬂ0 1
entity
·· 
.
·· 
Property
·· 
(
·· 
s
·· 
=>
··  
s
··! "
.
··" #
	ViewCount
··# ,
)
··, -
.
‚‚ 
HasDefaultValue
‚‚  
(
‚‚  !
$num
‚‚! "
)
‚‚" #
;
‚‚# $
entity
‰‰ 
.
‰‰ 
HasDiscriminator
‰‰ #
<
‰‰# $
string
‰‰$ *
>
‰‰* +
(
‰‰+ ,*
StreetcodeTypeDiscriminators
‰‰, H
.
‰‰H I
DiscriminatorName
‰‰I Z
)
‰‰Z [
.
ÂÂ 
HasValue
ÂÂ 
<
ÂÂ 
StreetcodeContent
ÂÂ +
>
ÂÂ+ ,
(
ÂÂ, -*
StreetcodeTypeDiscriminators
ÂÂ- I
.
ÂÂI J 
StreetcodeBaseType
ÂÂJ \
)
ÂÂ\ ]
.
ÊÊ 
HasValue
ÊÊ 
<
ÊÊ 
PersonStreetcode
ÊÊ *
>
ÊÊ* +
(
ÊÊ+ ,*
StreetcodeTypeDiscriminators
ÊÊ, H
.
ÊÊH I"
StreetcodePersonType
ÊÊI ]
)
ÊÊ] ^
.
ÁÁ 
HasValue
ÁÁ 
<
ÁÁ 
EventStreetcode
ÁÁ )
>
ÁÁ) *
(
ÁÁ* +*
StreetcodeTypeDiscriminators
ÁÁ+ G
.
ÁÁG H!
StreetcodeEventType
ÁÁH [
)
ÁÁ[ \
;
ÁÁ\ ]
entity
ÈÈ 
.
ÈÈ 
Property
ÈÈ 
<
ÈÈ 
string
ÈÈ "
>
ÈÈ" #
(
ÈÈ# $
$str
ÈÈ$ 4
)
ÈÈ4 5
.
ÈÈ5 6
Metadata
ÈÈ6 >
.
ÈÈ> ?"
SetAfterSaveBehavior
ÈÈ? S
(
ÈÈS T"
PropertySaveBehavior
ÈÈT h
.
ÈÈh i
Save
ÈÈi m
)
ÈÈm n
;
ÈÈn o
entity
ÎÎ 
.
ÎÎ 
HasMany
ÎÎ 
(
ÎÎ 
d
ÎÎ 
=>
ÎÎ 
d
ÎÎ  !
.
ÎÎ! "
Coordinates
ÎÎ" -
)
ÎÎ- .
.
ÏÏ 
WithOne
ÏÏ 
(
ÏÏ 
c
ÏÏ 
=>
ÏÏ 
c
ÏÏ 
.
ÏÏ  

Streetcode
ÏÏ  *
)
ÏÏ* +
.
ÌÌ 
OnDelete
ÌÌ 
(
ÌÌ 
DeleteBehavior
ÌÌ (
.
ÌÌ( )
Cascade
ÌÌ) 0
)
ÌÌ0 1
;
ÌÌ1 2
entity
ÔÔ 
.
ÔÔ 
HasMany
ÔÔ 
(
ÔÔ 
d
ÔÔ 
=>
ÔÔ 
d
ÔÔ  !
.
ÔÔ! "
Facts
ÔÔ" '
)
ÔÔ' (
.
 
WithOne
 
(
 
f
 
=>
 
f
 
.
  

Streetcode
  *
)
* +
.
ÒÒ 
OnDelete
ÒÒ 
(
ÒÒ 
DeleteBehavior
ÒÒ (
.
ÒÒ( )
Cascade
ÒÒ) 0
)
ÒÒ0 1
;
ÒÒ1 2
entity
ÛÛ 
.
ÛÛ 
HasMany
ÛÛ 
(
ÛÛ 
d
ÛÛ 
=>
ÛÛ 
d
ÛÛ  !
.
ÛÛ! "
Images
ÛÛ" (
)
ÛÛ( )
.
ÙÙ 
WithMany
ÙÙ 
(
ÙÙ 
i
ÙÙ 
=>
ÙÙ 
i
ÙÙ  
.
ÙÙ  !
Streetcodes
ÙÙ! ,
)
ÙÙ, -
.
ıı 
UsingEntity
ıı 
<
ıı 
StreetcodeImage
ıı ,
>
ıı, -
(
ıı- .
si
ˆˆ 
=>
ˆˆ 
si
ˆˆ 
.
ˆˆ 
HasOne
ˆˆ #
(
ˆˆ# $
i
ˆˆ$ %
=>
ˆˆ& (
i
ˆˆ) *
.
ˆˆ* +
Image
ˆˆ+ 0
)
ˆˆ0 1
.
ˆˆ1 2
WithMany
ˆˆ2 :
(
ˆˆ: ;
)
ˆˆ; <
.
ˆˆ< =
HasForeignKey
ˆˆ= J
(
ˆˆJ K
i
ˆˆK L
=>
ˆˆM O
i
ˆˆP Q
.
ˆˆQ R
ImageId
ˆˆR Y
)
ˆˆY Z
,
ˆˆZ [
si
˜˜ 
=>
˜˜ 
si
˜˜ 
.
˜˜ 
HasOne
˜˜ #
(
˜˜# $
i
˜˜$ %
=>
˜˜& (
i
˜˜) *
.
˜˜* +

Streetcode
˜˜+ 5
)
˜˜5 6
.
˜˜6 7
WithMany
˜˜7 ?
(
˜˜? @
)
˜˜@ A
.
˜˜A B
HasForeignKey
˜˜B O
(
˜˜O P
i
˜˜P Q
=>
˜˜R T
i
˜˜U V
.
˜˜V W
StreetcodeId
˜˜W c
)
˜˜c d
)
˜˜d e
.
¯¯ 
ToTable
¯¯ 
(
¯¯ 
$str
¯¯ +
,
¯¯+ ,
$str
¯¯- 9
)
¯¯9 :
;
¯¯: ;
entity
˙˙ 
.
˙˙ 
HasMany
˙˙ 
(
˙˙ 
d
˙˙ 
=>
˙˙ 
d
˙˙  !
.
˙˙! "
TimelineItems
˙˙" /
)
˙˙/ 0
.
˚˚ 
WithOne
˚˚ 
(
˚˚ 
t
˚˚ 
=>
˚˚ 
t
˚˚ 
.
˚˚  

Streetcode
˚˚  *
)
˚˚* +
.
¸¸ 
OnDelete
¸¸ 
(
¸¸ 
DeleteBehavior
¸¸ (
.
¸¸( )
Cascade
¸¸) 0
)
¸¸0 1
;
¸¸1 2
entity
˛˛ 
.
˛˛ 
HasMany
˛˛ 
(
˛˛ 
d
˛˛ 
=>
˛˛ 
d
˛˛  !
.
˛˛! "
Toponyms
˛˛" *
)
˛˛* +
.
ˇˇ 
WithMany
ˇˇ 
(
ˇˇ 
t
ˇˇ 
=>
ˇˇ 
t
ˇˇ  
.
ˇˇ  !
Streetcodes
ˇˇ! ,
)
ˇˇ, -
.
ÄÄ 
UsingEntity
ÄÄ 
<
ÄÄ 
StreetcodeToponym
ÄÄ .
>
ÄÄ. /
(
ÄÄ/ 0
st
ÅÅ 
=>
ÅÅ 
st
ÅÅ 
.
ÅÅ 
HasOne
ÅÅ #
(
ÅÅ# $
s
ÅÅ$ %
=>
ÅÅ& (
s
ÅÅ) *
.
ÅÅ* +
Toponym
ÅÅ+ 2
)
ÅÅ2 3
.
ÅÅ3 4
WithMany
ÅÅ4 <
(
ÅÅ< =
)
ÅÅ= >
.
ÅÅ> ?
HasForeignKey
ÅÅ? L
(
ÅÅL M
x
ÅÅM N
=>
ÅÅO Q
x
ÅÅR S
.
ÅÅS T
	ToponymId
ÅÅT ]
)
ÅÅ] ^
,
ÅÅ^ _
st
ÇÇ 
=>
ÇÇ 
st
ÇÇ 
.
ÇÇ 
HasOne
ÇÇ #
(
ÇÇ# $
s
ÇÇ$ %
=>
ÇÇ& (
s
ÇÇ) *
.
ÇÇ* +

Streetcode
ÇÇ+ 5
)
ÇÇ5 6
.
ÇÇ6 7
WithMany
ÇÇ7 ?
(
ÇÇ? @
)
ÇÇ@ A
.
ÇÇA B
HasForeignKey
ÇÇB O
(
ÇÇO P
x
ÇÇP Q
=>
ÇÇR T
x
ÇÇU V
.
ÇÇV W
StreetcodeId
ÇÇW c
)
ÇÇc d
)
ÇÇd e
.
ÉÉ 
ToTable
ÉÉ 
(
ÉÉ 
$str
ÉÉ -
,
ÉÉ- .
$str
ÉÉ/ ;
)
ÉÉ; <
;
ÉÉ< =
entity
ÖÖ 
.
ÖÖ 
HasMany
ÖÖ 
(
ÖÖ 
d
ÖÖ 
=>
ÖÖ 
d
ÖÖ  !
.
ÖÖ! ""
SourceLinkCategories
ÖÖ" 6
)
ÖÖ6 7
.
ÜÜ 
WithMany
ÜÜ 
(
ÜÜ 
c
ÜÜ 
=>
ÜÜ  "
c
ÜÜ# $
.
ÜÜ$ %
Streetcodes
ÜÜ% 0
)
ÜÜ0 1
.
áá 
UsingEntity
áá  
<
áá  !'
StreetcodeCategoryContent
áá! :
>
áá: ;
(
áá; <
scat
àà 
=>
àà 
scat
àà  $
.
àà$ %
HasOne
àà% +
(
àà+ ,
i
àà, -
=>
àà. 0
i
àà1 2
.
àà2 3 
SourceLinkCategory
àà3 E
)
ààE F
.
ààF G
WithMany
ààG O
(
ààO P
s
ààP Q
=>
ààR T
s
ààU V
.
ààV W(
StreetcodeCategoryContents
ààW q
)
ààq r
.
ààr s
HasForeignKeyààs Ä
(ààÄ Å
iààÅ Ç
=>ààÉ Ö
iààÜ á
.ààá à$
SourceLinkCategoryIdààà ú
)ààú ù
,ààù û
scat
ââ 
=>
ââ 
scat
ââ  $
.
ââ$ %
HasOne
ââ% +
(
ââ+ ,
i
ââ, -
=>
ââ. 0
i
ââ1 2
.
ââ2 3

Streetcode
ââ3 =
)
ââ= >
.
ââ> ?
WithMany
ââ? G
(
ââG H
s
ââH I
=>
ââJ L
s
ââM N
.
ââN O(
StreetcodeCategoryContents
ââO i
)
ââi j
.
ââj k
HasForeignKey
ââk x
(
ââx y
i
âây z
=>
ââ{ }
i
ââ~ 
.ââ Ä
StreetcodeIdââÄ å
)ââå ç
)ââç é
.
ää 
ToTable
ää 
(
ää 
$str
ää @
,
ää@ A
$str
ääB K
)
ääK L
;
ääL M
entity
åå 
.
åå 
HasMany
åå 
(
åå 
d
åå 
=>
åå 
d
åå  !
.
åå! "
Partners
åå" *
)
åå* +
.
çç 
WithMany
çç 
(
çç 
p
çç 
=>
çç  "
p
çç# $
.
çç$ %
Streetcodes
çç% 0
)
çç0 1
.
éé 
UsingEntity
éé  
<
éé  !
StreetcodePartner
éé! 2
>
éé2 3
(
éé3 4
sp
èè 
=>
èè 
sp
èè  
.
èè  !
HasOne
èè! '
(
èè' (
i
èè( )
=>
èè* ,
i
èè- .
.
èè. /
Partner
èè/ 6
)
èè6 7
.
èè7 8
WithMany
èè8 @
(
èè@ A
)
èèA B
.
èèB C
HasForeignKey
èèC P
(
èèP Q
x
èèQ R
=>
èèS U
x
èèV W
.
èèW X
	PartnerId
èèX a
)
èèa b
,
èèb c
sp
êê 
=>
êê 
sp
êê  
.
êê  !
HasOne
êê! '
(
êê' (
i
êê( )
=>
êê* ,
i
êê- .
.
êê. /

Streetcode
êê/ 9
)
êê9 :
.
êê: ;
WithMany
êê; C
(
êêC D
)
êêD E
.
êêE F
HasForeignKey
êêF S
(
êêS T
x
êêT U
=>
êêV X
x
êêY Z
.
êêZ [
StreetcodeId
êê[ g
)
êêg h
)
êêh i
.
ëë 
ToTable
ëë 
(
ëë 
$str
ëë 1
,
ëë1 2
$str
ëë3 ?
)
ëë? @
;
ëë@ A
entity
ìì 
.
ìì 
HasMany
ìì 
(
ìì 
d
ìì 
=>
ìì 
d
ìì  !
.
ìì! "
Videos
ìì" (
)
ìì( )
.
îî 
WithOne
îî 
(
îî 
p
îî 
=>
îî !
p
îî" #
.
îî# $

Streetcode
îî$ .
)
îî. /
.
ïï 
HasForeignKey
ïï "
(
ïï" #
d
ïï# $
=>
ïï% '
d
ïï( )
.
ïï) *
StreetcodeId
ïï* 6
)
ïï6 7
.
ññ 
OnDelete
ññ 
(
ññ 
DeleteBehavior
ññ ,
.
ññ, -
Cascade
ññ- 4
)
ññ4 5
;
ññ5 6
entity
òò 
.
òò 
HasOne
òò 
(
òò 
d
òò 
=>
òò 
d
òò  
.
òò  !
Audio
òò! &
)
òò& '
.
ôô 
WithOne
ôô 
(
ôô 
p
ôô 
=>
ôô !
p
ôô" #
.
ôô# $

Streetcode
ôô$ .
)
ôô. /
.
öö 
OnDelete
öö 
(
öö 
DeleteBehavior
öö ,
.
öö, -
Cascade
öö- 4
)
öö4 5
;
öö5 6
entity
úú 
.
úú 
HasOne
úú 
(
úú 
d
úú 
=>
úú 
d
úú  
.
úú  !
Text
úú! %
)
úú% &
.
ùù 
WithOne
ùù 
(
ùù 
p
ùù 
=>
ùù !
p
ùù" #
.
ùù# $

Streetcode
ùù$ .
)
ùù. /
.
ûû 
HasForeignKey
ûû "
<
ûû" #
Text
ûû# '
>
ûû' (
(
ûû( )
d
ûû) *
=>
ûû+ -
d
ûû. /
.
ûû/ 0
StreetcodeId
ûû0 <
)
ûû< =
.
üü 
OnDelete
üü 
(
üü 
DeleteBehavior
üü ,
.
üü, -
Cascade
üü- 4
)
üü4 5
;
üü5 6
entity
°° 
.
°° 
HasOne
°° 
(
°° 
d
°° 
=>
°° 
d
°°  
.
°°  !
TransactionLink
°°! 0
)
°°0 1
.
¢¢ 
WithOne
¢¢ 
(
¢¢ 
p
¢¢ 
=>
¢¢ !
p
¢¢" #
.
¢¢# $

Streetcode
¢¢$ .
)
¢¢. /
.
££ 
HasForeignKey
££ "
<
££" #
TransactionLink
££# 2
>
££2 3
(
££3 4
d
££4 5
=>
££6 8
d
££9 :
.
££: ;
StreetcodeId
££; G
)
££G H
.
§§ 
OnDelete
§§ 
(
§§ 
DeleteBehavior
§§ ,
.
§§, -
Cascade
§§- 4
)
§§4 5
;
§§5 6
entity
¶¶ 
.
¶¶ 
HasMany
¶¶ 
(
¶¶ 
d
¶¶ 
=>
¶¶ 
d
¶¶  !
.
¶¶! "
StatisticRecords
¶¶" 2
)
¶¶2 3
.
ßß 
WithOne
ßß 
(
ßß 
t
ßß 
=>
ßß !
t
ßß" #
.
ßß# $

Streetcode
ßß$ .
)
ßß. /
.
®® 
HasForeignKey
®® "
(
®®" #
t
®®# $
=>
®®% '
t
®®( )
.
®®) *
StreetcodeId
®®* 6
)
®®6 7
.
©© 
OnDelete
©© 
(
©© 
DeleteBehavior
©© ,
.
©©, -
NoAction
©©- 5
)
©©5 6
;
©©6 7
}
™™ 	
)
™™	 

;
™™
 
modelBuilder
¨¨ 
.
¨¨ 
Entity
¨¨ 
<
¨¨ 
RelatedTerm
¨¨ '
>
¨¨' (
(
¨¨( )
)
¨¨) *
.
≠≠ 
HasOne
≠≠ 
(
≠≠ 
rt
≠≠ 
=>
≠≠ 
rt
≠≠ 
.
≠≠ 
Term
≠≠ !
)
≠≠! "
.
ÆÆ 
WithMany
ÆÆ 
(
ÆÆ 
t
ÆÆ 
=>
ÆÆ 
t
ÆÆ 
.
ÆÆ 
RelatedTerms
ÆÆ )
)
ÆÆ) *
.
ØØ 
HasForeignKey
ØØ 
(
ØØ 
rt
ØØ 
=>
ØØ  
rt
ØØ! #
.
ØØ# $
TermId
ØØ$ *
)
ØØ* +
;
ØØ+ ,
modelBuilder
±± 
.
±± 
Entity
±± 
<
±± 

Coordinate
±± &
>
±±& '
(
±±' (
)
±±( )
.
≤≤ 
HasDiscriminator
≤≤ 
<
≤≤ 
string
≤≤ $
>
≤≤$ %
(
≤≤% &
$str
≤≤& 6
)
≤≤6 7
.
≥≥ 
HasValue
≥≥ 
<
≥≥ 

Coordinate
≥≥  
>
≥≥  !
(
≥≥! "
$str
≥≥" 3
)
≥≥3 4
.
¥¥ 
HasValue
¥¥ 
<
¥¥ "
StreetcodeCoordinate
¥¥ *
>
¥¥* +
(
¥¥+ ,
$str
¥¥, C
)
¥¥C D
.
µµ 
HasValue
µµ 
<
µµ 
ToponymCoordinate
µµ '
>
µµ' (
(
µµ( )
$str
µµ) =
)
µµ= >
;
µµ> ?
}
∂∂ 
}∑∑ ¶
îD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Persistence\Migrations\20230703154732_UpdatePartnerModel.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Persistence $
.$ %

Migrations% /
{ 
public 

partial 
class 
UpdatePartnerModel +
:, -
	Migration. 7
{ 
	protected		 
override		 
void		 
Up		  "
(		" #
MigrationBuilder		# 3
migrationBuilder		4 D
)		D E
{

 	
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str 
, 
schema 
: 
$str "
," #
table 
: 
$str -
)- .
;. /
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str !
,! "
schema 
: 
$str "
," #
table 
: 
$str !
,! "
type 
: 
$str %
,% &
	maxLength 
: 
$num 
, 
nullable 
: 
true 
, 

oldClrType 
: 
typeof "
(" #
string# )
)) *
,* +
oldType 
: 
$str (
,( )
oldMaxLength 
: 
$num !
)! "
;" #
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 
AlterColumn (
<( )
string) /
>/ 0
(0 1
name 
: 
$str !
,! "
schema   
:   
$str   "
,  " #
table!! 
:!! 
$str!! !
,!!! "
type"" 
:"" 
$str"" %
,""% &
	maxLength## 
:## 
$num## 
,## 
nullable$$ 
:$$ 
false$$ 
,$$  
defaultValue%% 
:%% 
$str%%  
,%%  !

oldClrType&& 
:&& 
typeof&& "
(&&" #
string&&# )
)&&) *
,&&* +
oldType'' 
:'' 
$str'' (
,''( )
oldMaxLength(( 
:(( 
$num(( !
,((! "
oldNullable)) 
:)) 
true)) !
)))! "
;))" #
migrationBuilder++ 
.++ 
	AddColumn++ &
<++& '
string++' -
>++- .
(++. /
name,, 
:,, 
$str,, 
,,, 
schema-- 
:-- 
$str-- "
,--" #
table.. 
:.. 
$str.. -
,..- .
type// 
:// 
$str// $
,//$ %
	maxLength00 
:00 
$num00 
,00 
nullable11 
:11 
false11 
,11  
defaultValue22 
:22 
$str22  
)22  !
;22! "
}33 	
}44 
}55 Ô
jD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Enums\UserRole.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Enums 
{ 
[ 
Flags 

]
 
public 

enum 
UserRole 
{ 
MainAdministrator 
, 
Administrator 
, 
	Moderator 
}		 
}

 úÒ
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Persistence\Migrations\20230622110726_Initial.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Persistence $
.$ %

Migrations% /
{ 
public 

partial 
class 
Initial  
:! "
	Migration# ,
{		 
	protected

 
override

 
void

 
Up

  "
(

" #
MigrationBuilder

# 3
migrationBuilder

4 D
)

D E
{ 	
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str 
) 
; 
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str #
)# $
;$ %
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str "
)" #
;# $
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str  
)  !
;! "
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str 
) 
; 
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str  
)  !
;! "
migrationBuilder 
. 
EnsureSchema )
() *
name 
: 
$str 
) 
; 
migrationBuilder!! 
.!! 
EnsureSchema!! )
(!!) *
name"" 
:"" 
$str"" #
)""# $
;""$ %
migrationBuilder$$ 
.$$ 
EnsureSchema$$ )
($$) *
name%% 
:%% 
$str%%  
)%%  !
;%%! "
migrationBuilder'' 
.'' 
EnsureSchema'' )
('') *
name(( 
:(( 
$str(( 
)((  
;((  !
migrationBuilder** 
.** 
EnsureSchema** )
(**) *
name++ 
:++ 
$str++  
)++  !
;++! "
migrationBuilder-- 
.-- 
EnsureSchema-- )
(--) *
name.. 
:.. 
$str.. $
)..$ %
;..% &
migrationBuilder00 
.00 
EnsureSchema00 )
(00) *
name11 
:11 
$str11 
)11 
;11 
migrationBuilder33 
.33 
CreateTable33 (
(33( )
name44 
:44 
$str44 
,44 
schema55 
:55 
$str55 
,55  
columns66 
:66 
table66 
=>66 !
new66" %
{77 
Id88 
=88 
table88 
.88 
Column88 %
<88% &
int88& )
>88) *
(88* +
type88+ /
:88/ 0
$str881 6
,886 7
nullable888 @
:88@ A
false88B G
)88G H
.99 

Annotation99 #
(99# $
$str99$ 8
,998 9
$str99: @
)99@ A
,99A B
Title:: 
=:: 
table:: !
.::! "
Column::" (
<::( )
string::) /
>::/ 0
(::0 1
type::1 5
:::5 6
$str::7 F
,::F G
	maxLength::H Q
:::Q R
$num::S V
,::V W
nullable::X `
:::` a
true::b f
)::f g
,::g h
BlobName;; 
=;; 
table;; $
.;;$ %
Column;;% +
<;;+ ,
string;;, 2
>;;2 3
(;;3 4
type;;4 8
:;;8 9
$str;;: I
,;;I J
	maxLength;;K T
:;;T U
$num;;V Y
,;;Y Z
nullable;;[ c
:;;c d
false;;e j
);;j k
,;;k l
MimeType<< 
=<< 
table<< $
.<<$ %
Column<<% +
<<<+ ,
string<<, 2
><<2 3
(<<3 4
type<<4 8
:<<8 9
$str<<: H
,<<H I
	maxLength<<J S
:<<S T
$num<<U W
,<<W X
nullable<<Y a
:<<a b
false<<c h
)<<h i
}== 
,== 
constraints>> 
:>> 
table>> "
=>>># %
{?? 
table@@ 
.@@ 

PrimaryKey@@ $
(@@$ %
$str@@% 0
,@@0 1
x@@2 3
=>@@4 6
x@@7 8
.@@8 9
Id@@9 ;
)@@; <
;@@< =
}AA 
)AA 
;AA 
migrationBuilderCC 
.CC 
CreateTableCC (
(CC( )
nameDD 
:DD 
$strDD +
,DD+ ,
schemaEE 
:EE 
$strEE "
,EE" #
columnsFF 
:FF 
tableFF 
=>FF !
newFF" %
{GG 
IdHH 
=HH 
tableHH 
.HH 
ColumnHH %
<HH% &
intHH& )
>HH) *
(HH* +
typeHH+ /
:HH/ 0
$strHH1 6
,HH6 7
nullableHH8 @
:HH@ A
falseHHB G
)HHG H
.II 

AnnotationII #
(II# $
$strII$ 8
,II8 9
$strII: @
)II@ A
,IIA B
TitleJJ 
=JJ 
tableJJ !
.JJ! "
ColumnJJ" (
<JJ( )
stringJJ) /
>JJ/ 0
(JJ0 1
typeJJ1 5
:JJ5 6
$strJJ7 E
,JJE F
	maxLengthJJG P
:JJP Q
$numJJR T
,JJT U
nullableJJV ^
:JJ^ _
falseJJ` e
)JJe f
}KK 
,KK 
constraintsLL 
:LL 
tableLL "
=>LL# %
{MM 
tableNN 
.NN 

PrimaryKeyNN $
(NN$ %
$strNN% =
,NN= >
xNN? @
=>NNA C
xNND E
.NNE F
IdNNF H
)NNH I
;NNI J
}OO 
)OO 
;OO 
migrationBuilderQQ 
.QQ 
CreateTableQQ (
(QQ( )
nameRR 
:RR 
$strRR 
,RR 
schemaSS 
:SS 
$strSS 
,SS  
columnsTT 
:TT 
tableTT 
=>TT !
newTT" %
{UU 
IdVV 
=VV 
tableVV 
.VV 
ColumnVV %
<VV% &
intVV& )
>VV) *
(VV* +
typeVV+ /
:VV/ 0
$strVV1 6
,VV6 7
nullableVV8 @
:VV@ A
falseVVB G
)VVG H
.WW 

AnnotationWW #
(WW# $
$strWW$ 8
,WW8 9
$strWW: @
)WW@ A
,WWA B
BlobNameXX 
=XX 
tableXX $
.XX$ %
ColumnXX% +
<XX+ ,
stringXX, 2
>XX2 3
(XX3 4
typeXX4 8
:XX8 9
$strXX: I
,XXI J
	maxLengthXXK T
:XXT U
$numXXV Y
,XXY Z
nullableXX[ c
:XXc d
falseXXe j
)XXj k
,XXk l
MimeTypeYY 
=YY 
tableYY $
.YY$ %
ColumnYY% +
<YY+ ,
stringYY, 2
>YY2 3
(YY3 4
typeYY4 8
:YY8 9
$strYY: H
,YYH I
	maxLengthYYJ S
:YYS T
$numYYU W
,YYW X
nullableYYY a
:YYa b
falseYYc h
)YYh i
}ZZ 
,ZZ 
constraints[[ 
:[[ 
table[[ "
=>[[# %
{\\ 
table]] 
.]] 

PrimaryKey]] $
(]]$ %
$str]]% 0
,]]0 1
x]]2 3
=>]]4 6
x]]7 8
.]]8 9
Id]]9 ;
)]]; <
;]]< =
}^^ 
)^^ 
;^^ 
migrationBuilder`` 
.`` 
CreateTable`` (
(``( )
nameaa 
:aa 
$straa !
,aa! "
schemabb 
:bb 
$strbb 
,bb 
columnscc 
:cc 
tablecc 
=>cc !
newcc" %
{dd 
Idee 
=ee 
tableee 
.ee 
Columnee %
<ee% &
intee& )
>ee) *
(ee* +
typeee+ /
:ee/ 0
$stree1 6
,ee6 7
nullableee8 @
:ee@ A
falseeeB G
)eeG H
.ff 

Annotationff #
(ff# $
$strff$ 8
,ff8 9
$strff: @
)ff@ A
,ffA B
Positiongg 
=gg 
tablegg $
.gg$ %
Columngg% +
<gg+ ,
stringgg, 2
>gg2 3
(gg3 4
typegg4 8
:gg8 9
$strgg: H
,ggH I
	maxLengthggJ S
:ggS T
$numggU W
,ggW X
nullableggY a
:gga b
falseggc h
)ggh i
}hh 
,hh 
constraintsii 
:ii 
tableii "
=>ii# %
{jj 
tablekk 
.kk 

PrimaryKeykk $
(kk$ %
$strkk% 3
,kk3 4
xkk5 6
=>kk7 9
xkk: ;
.kk; <
Idkk< >
)kk> ?
;kk? @
}ll 
)ll 
;ll 
migrationBuildernn 
.nn 
CreateTablenn (
(nn( )
nameoo 
:oo 
$stroo !
,oo! "
schemapp 
:pp 
$strpp "
,pp" #
columnsqq 
:qq 
tableqq 
=>qq !
newqq" %
{rr 
Idss 
=ss 
tabless 
.ss 
Columnss %
<ss% &
intss& )
>ss) *
(ss* +
typess+ /
:ss/ 0
$strss1 6
,ss6 7
nullabless8 @
:ss@ A
falsessB G
)ssG H
.tt 

Annotationtt #
(tt# $
$strtt$ 8
,tt8 9
$strtt: @
)tt@ A
,ttA B
Nameuu 
=uu 
tableuu  
.uu  !
Columnuu! '
<uu' (
stringuu( .
>uu. /
(uu/ 0
typeuu0 4
:uu4 5
$struu6 D
,uuD E
	maxLengthuuF O
:uuO P
$numuuQ S
,uuS T
nullableuuU ]
:uu] ^
trueuu_ c
)uuc d
,uud e
Emailvv 
=vv 
tablevv !
.vv! "
Columnvv" (
<vv( )
stringvv) /
>vv/ 0
(vv0 1
typevv1 5
:vv5 6
$strvv7 E
,vvE F
	maxLengthvvG P
:vvP Q
$numvvR T
,vvT U
nullablevvV ^
:vv^ _
falsevv` e
)vve f
,vvf g
Descriptionww 
=ww  !
tableww" '
.ww' (
Columnww( .
<ww. /
stringww/ 5
>ww5 6
(ww6 7
typeww7 ;
:ww; <
$strww= M
,wwM N
	maxLengthwwO X
:wwX Y
$numwwZ ^
,ww^ _
nullableww` h
:wwh i
truewwj n
)wwn o
}xx 
,xx 
constraintsyy 
:yy 
tableyy "
=>yy# %
{zz 
table{{ 
.{{ 

PrimaryKey{{ $
({{$ %
$str{{% 3
,{{3 4
x{{5 6
=>{{7 9
x{{: ;
.{{; <
Id{{< >
){{> ?
;{{? @
}|| 
)|| 
;|| 
migrationBuilder~~ 
.~~ 
CreateTable~~ (
(~~( )
name 
: 
$str 
, 
schema
ÄÄ 
:
ÄÄ 
$str
ÄÄ %
,
ÄÄ% &
columns
ÅÅ 
:
ÅÅ 
table
ÅÅ 
=>
ÅÅ !
new
ÅÅ" %
{
ÇÇ 
Id
ÉÉ 
=
ÉÉ 
table
ÉÉ 
.
ÉÉ 
Column
ÉÉ %
<
ÉÉ% &
int
ÉÉ& )
>
ÉÉ) *
(
ÉÉ* +
type
ÉÉ+ /
:
ÉÉ/ 0
$str
ÉÉ1 6
,
ÉÉ6 7
nullable
ÉÉ8 @
:
ÉÉ@ A
false
ÉÉB G
)
ÉÉG H
.
ÑÑ 

Annotation
ÑÑ #
(
ÑÑ# $
$str
ÑÑ$ 8
,
ÑÑ8 9
$str
ÑÑ: @
)
ÑÑ@ A
,
ÑÑA B
Title
ÖÖ 
=
ÖÖ 
table
ÖÖ !
.
ÖÖ! "
Column
ÖÖ" (
<
ÖÖ( )
string
ÖÖ) /
>
ÖÖ/ 0
(
ÖÖ0 1
type
ÖÖ1 5
:
ÖÖ5 6
$str
ÖÖ7 E
,
ÖÖE F
	maxLength
ÖÖG P
:
ÖÖP Q
$num
ÖÖR T
,
ÖÖT U
nullable
ÖÖV ^
:
ÖÖ^ _
false
ÖÖ` e
)
ÖÖe f
}
ÜÜ 
,
ÜÜ 
constraints
áá 
:
áá 
table
áá "
=>
áá# %
{
àà 
table
ââ 
.
ââ 

PrimaryKey
ââ $
(
ââ$ %
$str
ââ% .
,
ââ. /
x
ââ0 1
=>
ââ2 4
x
ââ5 6
.
ââ6 7
Id
ââ7 9
)
ââ9 :
;
ââ: ;
}
ää 
)
ää 
;
ää 
migrationBuilder
åå 
.
åå 
CreateTable
åå (
(
åå( )
name
çç 
:
çç 
$str
çç 
,
çç 
schema
éé 
:
éé 
$str
éé $
,
éé$ %
columns
èè 
:
èè 
table
èè 
=>
èè !
new
èè" %
{
êê 
Id
ëë 
=
ëë 
table
ëë 
.
ëë 
Column
ëë %
<
ëë% &
int
ëë& )
>
ëë) *
(
ëë* +
type
ëë+ /
:
ëë/ 0
$str
ëë1 6
,
ëë6 7
nullable
ëë8 @
:
ëë@ A
false
ëëB G
)
ëëG H
.
íí 

Annotation
íí #
(
íí# $
$str
íí$ 8
,
íí8 9
$str
íí: @
)
íí@ A
,
ííA B
Title
ìì 
=
ìì 
table
ìì !
.
ìì! "
Column
ìì" (
<
ìì( )
string
ìì) /
>
ìì/ 0
(
ìì0 1
type
ìì1 5
:
ìì5 6
$str
ìì7 E
,
ììE F
	maxLength
ììG P
:
ììP Q
$num
ììR T
,
ììT U
nullable
ììV ^
:
ìì^ _
false
ìì` e
)
ììe f
,
ììf g
Description
îî 
=
îî  !
table
îî" '
.
îî' (
Column
îî( .
<
îî. /
string
îî/ 5
>
îî5 6
(
îî6 7
type
îî7 ;
:
îî; <
$str
îî= L
,
îîL M
	maxLength
îîN W
:
îîW X
$num
îîY \
,
îî\ ]
nullable
îî^ f
:
îîf g
false
îîh m
)
îîm n
}
ïï 
,
ïï 
constraints
ññ 
:
ññ 
table
ññ "
=>
ññ# %
{
óó 
table
òò 
.
òò 

PrimaryKey
òò $
(
òò$ %
$str
òò% /
,
òò/ 0
x
òò1 2
=>
òò3 5
x
òò6 7
.
òò7 8
Id
òò8 :
)
òò: ;
;
òò; <
}
ôô 
)
ôô 
;
ôô 
migrationBuilder
õõ 
.
õõ 
CreateTable
õõ (
(
õõ( )
name
úú 
:
úú 
$str
úú  
,
úú  !
schema
ùù 
:
ùù 
$str
ùù "
,
ùù" #
columns
ûû 
:
ûû 
table
ûû 
=>
ûû !
new
ûû" %
{
üü 
Id
†† 
=
†† 
table
†† 
.
†† 
Column
†† %
<
††% &
int
††& )
>
††) *
(
††* +
type
††+ /
:
††/ 0
$str
††1 6
,
††6 7
nullable
††8 @
:
††@ A
false
††B G
)
††G H
.
°° 

Annotation
°° #
(
°°# $
$str
°°$ 8
,
°°8 9
$str
°°: @
)
°°@ A
,
°°A B
Oblast
¢¢ 
=
¢¢ 
table
¢¢ "
.
¢¢" #
Column
¢¢# )
<
¢¢) *
string
¢¢* 0
>
¢¢0 1
(
¢¢1 2
type
¢¢2 6
:
¢¢6 7
$str
¢¢8 G
,
¢¢G H
	maxLength
¢¢I R
:
¢¢R S
$num
¢¢T W
,
¢¢W X
nullable
¢¢Y a
:
¢¢a b
false
¢¢c h
)
¢¢h i
,
¢¢i j
AdminRegionOld
££ "
=
££# $
table
££% *
.
££* +
Column
££+ 1
<
££1 2
string
££2 8
>
££8 9
(
££9 :
type
££: >
:
££> ?
$str
££@ O
,
££O P
	maxLength
££Q Z
:
££Z [
$num
££\ _
,
££_ `
nullable
££a i
:
££i j
true
££k o
)
££o p
,
££p q
AdminRegionNew
§§ "
=
§§# $
table
§§% *
.
§§* +
Column
§§+ 1
<
§§1 2
string
§§2 8
>
§§8 9
(
§§9 :
type
§§: >
:
§§> ?
$str
§§@ O
,
§§O P
	maxLength
§§Q Z
:
§§Z [
$num
§§\ _
,
§§_ `
nullable
§§a i
:
§§i j
true
§§k o
)
§§o p
,
§§p q
Gromada
•• 
=
•• 
table
•• #
.
••# $
Column
••$ *
<
••* +
string
••+ 1
>
••1 2
(
••2 3
type
••3 7
:
••7 8
$str
••9 H
,
••H I
	maxLength
••J S
:
••S T
$num
••U X
,
••X Y
nullable
••Z b
:
••b c
true
••d h
)
••h i
,
••i j
	Community
¶¶ 
=
¶¶ 
table
¶¶  %
.
¶¶% &
Column
¶¶& ,
<
¶¶, -
string
¶¶- 3
>
¶¶3 4
(
¶¶4 5
type
¶¶5 9
:
¶¶9 :
$str
¶¶; J
,
¶¶J K
	maxLength
¶¶L U
:
¶¶U V
$num
¶¶W Z
,
¶¶Z [
nullable
¶¶\ d
:
¶¶d e
true
¶¶f j
)
¶¶j k
,
¶¶k l

StreetName
ßß 
=
ßß  
table
ßß! &
.
ßß& '
Column
ßß' -
<
ßß- .
string
ßß. 4
>
ßß4 5
(
ßß5 6
type
ßß6 :
:
ßß: ;
$str
ßß< K
,
ßßK L
	maxLength
ßßM V
:
ßßV W
$num
ßßX [
,
ßß[ \
nullable
ßß] e
:
ßße f
false
ßßg l
)
ßßl m
,
ßßm n

StreetType
®® 
=
®®  
table
®®! &
.
®®& '
Column
®®' -
<
®®- .
string
®®. 4
>
®®4 5
(
®®5 6
type
®®6 :
:
®®: ;
$str
®®< J
,
®®J K
	maxLength
®®L U
:
®®U V
$num
®®W Y
,
®®Y Z
nullable
®®[ c
:
®®c d
true
®®e i
)
®®i j
}
©© 
,
©© 
constraints
™™ 
:
™™ 
table
™™ "
=>
™™# %
{
´´ 
table
¨¨ 
.
¨¨ 

PrimaryKey
¨¨ $
(
¨¨$ %
$str
¨¨% 2
,
¨¨2 3
x
¨¨4 5
=>
¨¨6 8
x
¨¨9 :
.
¨¨: ;
Id
¨¨; =
)
¨¨= >
;
¨¨> ?
}
≠≠ 
)
≠≠ 
;
≠≠ 
migrationBuilder
ØØ 
.
ØØ 
CreateTable
ØØ (
(
ØØ( )
name
∞∞ 
:
∞∞ 
$str
∞∞ 
,
∞∞ 
schema
±± 
:
±± 
$str
±± 
,
±±  
columns
≤≤ 
:
≤≤ 
table
≤≤ 
=>
≤≤ !
new
≤≤" %
{
≥≥ 
Id
¥¥ 
=
¥¥ 
table
¥¥ 
.
¥¥ 
Column
¥¥ %
<
¥¥% &
int
¥¥& )
>
¥¥) *
(
¥¥* +
type
¥¥+ /
:
¥¥/ 0
$str
¥¥1 6
,
¥¥6 7
nullable
¥¥8 @
:
¥¥@ A
false
¥¥B G
)
¥¥G H
.
µµ 

Annotation
µµ #
(
µµ# $
$str
µµ$ 8
,
µµ8 9
$str
µµ: @
)
µµ@ A
,
µµA B
Name
∂∂ 
=
∂∂ 
table
∂∂  
.
∂∂  !
Column
∂∂! '
<
∂∂' (
string
∂∂( .
>
∂∂. /
(
∂∂/ 0
type
∂∂0 4
:
∂∂4 5
$str
∂∂6 D
,
∂∂D E
	maxLength
∂∂F O
:
∂∂O P
$num
∂∂Q S
,
∂∂S T
nullable
∂∂U ]
:
∂∂] ^
false
∂∂_ d
)
∂∂d e
,
∂∂e f
Surname
∑∑ 
=
∑∑ 
table
∑∑ #
.
∑∑# $
Column
∑∑$ *
<
∑∑* +
string
∑∑+ 1
>
∑∑1 2
(
∑∑2 3
type
∑∑3 7
:
∑∑7 8
$str
∑∑9 G
,
∑∑G H
	maxLength
∑∑I R
:
∑∑R S
$num
∑∑T V
,
∑∑V W
nullable
∑∑X `
:
∑∑` a
false
∑∑b g
)
∑∑g h
,
∑∑h i
Email
∏∏ 
=
∏∏ 
table
∏∏ !
.
∏∏! "
Column
∏∏" (
<
∏∏( )
string
∏∏) /
>
∏∏/ 0
(
∏∏0 1
type
∏∏1 5
:
∏∏5 6
$str
∏∏7 F
,
∏∏F G
nullable
∏∏H P
:
∏∏P Q
false
∏∏R W
)
∏∏W X
,
∏∏X Y
Login
ππ 
=
ππ 
table
ππ !
.
ππ! "
Column
ππ" (
<
ππ( )
string
ππ) /
>
ππ/ 0
(
ππ0 1
type
ππ1 5
:
ππ5 6
$str
ππ7 E
,
ππE F
	maxLength
ππG P
:
ππP Q
$num
ππR T
,
ππT U
nullable
ππV ^
:
ππ^ _
false
ππ` e
)
ππe f
,
ππf g
Password
∫∫ 
=
∫∫ 
table
∫∫ $
.
∫∫$ %
Column
∫∫% +
<
∫∫+ ,
string
∫∫, 2
>
∫∫2 3
(
∫∫3 4
type
∫∫4 8
:
∫∫8 9
$str
∫∫: H
,
∫∫H I
	maxLength
∫∫J S
:
∫∫S T
$num
∫∫U W
,
∫∫W X
nullable
∫∫Y a
:
∫∫a b
false
∫∫c h
)
∫∫h i
,
∫∫i j
Role
ªª 
=
ªª 
table
ªª  
.
ªª  !
Column
ªª! '
<
ªª' (
int
ªª( +
>
ªª+ ,
(
ªª, -
type
ªª- 1
:
ªª1 2
$str
ªª3 8
,
ªª8 9
nullable
ªª: B
:
ªªB C
false
ªªD I
)
ªªI J
}
ºº 
,
ºº 
constraints
ΩΩ 
:
ΩΩ 
table
ΩΩ "
=>
ΩΩ# %
{
ææ 
table
øø 
.
øø 

PrimaryKey
øø $
(
øø$ %
$str
øø% /
,
øø/ 0
x
øø1 2
=>
øø3 5
x
øø6 7
.
øø7 8
Id
øø8 :
)
øø: ;
;
øø; <
}
¿¿ 
)
¿¿ 
;
¿¿ 
migrationBuilder
¬¬ 
.
¬¬ 
CreateTable
¬¬ (
(
¬¬( )
name
√√ 
:
√√ 
$str
√√ #
,
√√# $
schema
ƒƒ 
:
ƒƒ 
$str
ƒƒ $
,
ƒƒ$ %
columns
≈≈ 
:
≈≈ 
table
≈≈ 
=>
≈≈ !
new
≈≈" %
{
∆∆ 
Id
«« 
=
«« 
table
«« 
.
«« 
Column
«« %
<
««% &
int
««& )
>
««) *
(
««* +
type
««+ /
:
««/ 0
$str
««1 6
,
««6 7
nullable
««8 @
:
««@ A
false
««B G
)
««G H
.
»» 

Annotation
»» #
(
»»# $
$str
»»$ 8
,
»»8 9
$str
»»: @
)
»»@ A
,
»»A B
Index
…… 
=
…… 
table
…… !
.
……! "
Column
……" (
<
……( )
int
……) ,
>
……, -
(
……- .
type
……. 2
:
……2 3
$str
……4 9
,
……9 :
nullable
……; C
:
……C D
false
……E J
)
……J K
,
……K L
Teaser
   
=
   
table
   "
.
  " #
Column
  # )
<
  ) *
string
  * 0
>
  0 1
(
  1 2
type
  2 6
:
  6 7
$str
  8 G
,
  G H
	maxLength
  I R
:
  R S
$num
  T W
,
  W X
nullable
  Y a
:
  a b
true
  c g
)
  g h
,
  h i

DateString
ÀÀ 
=
ÀÀ  
table
ÀÀ! &
.
ÀÀ& '
Column
ÀÀ' -
<
ÀÀ- .
string
ÀÀ. 4
>
ÀÀ4 5
(
ÀÀ5 6
type
ÀÀ6 :
:
ÀÀ: ;
$str
ÀÀ< J
,
ÀÀJ K
	maxLength
ÀÀL U
:
ÀÀU V
$num
ÀÀW Y
,
ÀÀY Z
nullable
ÀÀ[ c
:
ÀÀc d
false
ÀÀe j
)
ÀÀj k
,
ÀÀk l
Alias
ÃÃ 
=
ÃÃ 
table
ÃÃ !
.
ÃÃ! "
Column
ÃÃ" (
<
ÃÃ( )
string
ÃÃ) /
>
ÃÃ/ 0
(
ÃÃ0 1
type
ÃÃ1 5
:
ÃÃ5 6
$str
ÃÃ7 E
,
ÃÃE F
	maxLength
ÃÃG P
:
ÃÃP Q
$num
ÃÃR T
,
ÃÃT U
nullable
ÃÃV ^
:
ÃÃ^ _
true
ÃÃ` d
)
ÃÃd e
,
ÃÃe f
Status
ÕÕ 
=
ÕÕ 
table
ÕÕ "
.
ÕÕ" #
Column
ÕÕ# )
<
ÕÕ) *
int
ÕÕ* -
>
ÕÕ- .
(
ÕÕ. /
type
ÕÕ/ 3
:
ÕÕ3 4
$str
ÕÕ5 :
,
ÕÕ: ;
nullable
ÕÕ< D
:
ÕÕD E
false
ÕÕF K
)
ÕÕK L
,
ÕÕL M
Title
ŒŒ 
=
ŒŒ 
table
ŒŒ !
.
ŒŒ! "
Column
ŒŒ" (
<
ŒŒ( )
string
ŒŒ) /
>
ŒŒ/ 0
(
ŒŒ0 1
type
ŒŒ1 5
:
ŒŒ5 6
$str
ŒŒ7 F
,
ŒŒF G
	maxLength
ŒŒH Q
:
ŒŒQ R
$num
ŒŒS V
,
ŒŒV W
nullable
ŒŒX `
:
ŒŒ` a
false
ŒŒb g
)
ŒŒg h
,
ŒŒh i 
TransliterationUrl
œœ &
=
œœ' (
table
œœ) .
.
œœ. /
Column
œœ/ 5
<
œœ5 6
string
œœ6 <
>
œœ< =
(
œœ= >
type
œœ> B
:
œœB C
$str
œœD S
,
œœS T
	maxLength
œœU ^
:
œœ^ _
$num
œœ` c
,
œœc d
nullable
œœe m
:
œœm n
false
œœo t
)
œœt u
,
œœu v
	ViewCount
–– 
=
–– 
table
––  %
.
––% &
Column
––& ,
<
––, -
int
––- 0
>
––0 1
(
––1 2
type
––2 6
:
––6 7
$str
––8 =
,
––= >
nullable
––? G
:
––G H
false
––I N
,
––N O
defaultValue
––P \
:
––\ ]
$num
––^ _
)
––_ `
,
––` a
	CreatedAt
—— 
=
—— 
table
——  %
.
——% &
Column
——& ,
<
——, -
DateTime
——- 5
>
——5 6
(
——6 7
type
——7 ;
:
——; <
$str
——= H
,
——H I
nullable
——J R
:
——R S
false
——T Y
,
——Y Z
defaultValueSql
——[ j
:
——j k
$str
——l w
)
——w x
,
——x y
	UpdatedAt
““ 
=
““ 
table
““  %
.
““% &
Column
““& ,
<
““, -
DateTime
““- 5
>
““5 6
(
““6 7
type
““7 ;
:
““; <
$str
““= H
,
““H I
nullable
““J R
:
““R S
false
““T Y
,
““Y Z
defaultValueSql
““[ j
:
““j k
$str
““l w
)
““w x
,
““x y)
EventStartOrPersonBirthDate
”” /
=
””0 1
table
””2 7
.
””7 8
Column
””8 >
<
””> ?
DateTime
””? G
>
””G H
(
””H I
type
””I M
:
””M N
$str
””O Z
,
””Z [
nullable
””\ d
:
””d e
false
””f k
)
””k l
,
””l m'
EventEndOrPersonDeathDate
‘‘ -
=
‘‘. /
table
‘‘0 5
.
‘‘5 6
Column
‘‘6 <
<
‘‘< =
DateTime
‘‘= E
>
‘‘E F
(
‘‘F G
type
‘‘G K
:
‘‘K L
$str
‘‘M X
,
‘‘X Y
nullable
‘‘Z b
:
‘‘b c
true
‘‘d h
)
‘‘h i
,
‘‘i j
AudioId
’’ 
=
’’ 
table
’’ #
.
’’# $
Column
’’$ *
<
’’* +
int
’’+ .
>
’’. /
(
’’/ 0
type
’’0 4
:
’’4 5
$str
’’6 ;
,
’’; <
nullable
’’= E
:
’’E F
true
’’G K
)
’’K L
,
’’L M
StreetcodeType
÷÷ "
=
÷÷# $
table
÷÷% *
.
÷÷* +
Column
÷÷+ 1
<
÷÷1 2
string
÷÷2 8
>
÷÷8 9
(
÷÷9 :
type
÷÷: >
:
÷÷> ?
$str
÷÷@ O
,
÷÷O P
nullable
÷÷Q Y
:
÷÷Y Z
false
÷÷[ `
)
÷÷` a
,
÷÷a b
	FirstName
◊◊ 
=
◊◊ 
table
◊◊  %
.
◊◊% &
Column
◊◊& ,
<
◊◊, -
string
◊◊- 3
>
◊◊3 4
(
◊◊4 5
type
◊◊5 9
:
◊◊9 :
$str
◊◊; I
,
◊◊I J
	maxLength
◊◊K T
:
◊◊T U
$num
◊◊V X
,
◊◊X Y
nullable
◊◊Z b
:
◊◊b c
true
◊◊d h
)
◊◊h i
,
◊◊i j
Rank
ÿÿ 
=
ÿÿ 
table
ÿÿ  
.
ÿÿ  !
Column
ÿÿ! '
<
ÿÿ' (
string
ÿÿ( .
>
ÿÿ. /
(
ÿÿ/ 0
type
ÿÿ0 4
:
ÿÿ4 5
$str
ÿÿ6 D
,
ÿÿD E
	maxLength
ÿÿF O
:
ÿÿO P
$num
ÿÿQ S
,
ÿÿS T
nullable
ÿÿU ]
:
ÿÿ] ^
true
ÿÿ_ c
)
ÿÿc d
,
ÿÿd e
LastName
ŸŸ 
=
ŸŸ 
table
ŸŸ $
.
ŸŸ$ %
Column
ŸŸ% +
<
ŸŸ+ ,
string
ŸŸ, 2
>
ŸŸ2 3
(
ŸŸ3 4
type
ŸŸ4 8
:
ŸŸ8 9
$str
ŸŸ: H
,
ŸŸH I
	maxLength
ŸŸJ S
:
ŸŸS T
$num
ŸŸU W
,
ŸŸW X
nullable
ŸŸY a
:
ŸŸa b
true
ŸŸc g
)
ŸŸg h
}
⁄⁄ 
,
⁄⁄ 
constraints
€€ 
:
€€ 
table
€€ "
=>
€€# %
{
‹‹ 
table
›› 
.
›› 

PrimaryKey
›› $
(
››$ %
$str
››% 5
,
››5 6
x
››7 8
=>
››9 ;
x
››< =
.
››= >
Id
››> @
)
››@ A
;
››A B
table
ﬁﬁ 
.
ﬁﬁ 

ForeignKey
ﬁﬁ $
(
ﬁﬁ$ %
name
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ =
,
ﬂﬂ= >
column
‡‡ 
:
‡‡ 
x
‡‡  !
=>
‡‡" $
x
‡‡% &
.
‡‡& '
AudioId
‡‡' .
,
‡‡. /
principalSchema
·· '
:
··' (
$str
··) 0
,
··0 1
principalTable
‚‚ &
:
‚‚& '
$str
‚‚( 0
,
‚‚0 1
principalColumn
„„ '
:
„„' (
$str
„„) -
,
„„- .
onDelete
‰‰  
:
‰‰  !
ReferentialAction
‰‰" 3
.
‰‰3 4
Cascade
‰‰4 ;
)
‰‰; <
;
‰‰< =
}
ÂÂ 
)
ÂÂ 
;
ÂÂ 
migrationBuilder
ÁÁ 
.
ÁÁ 
CreateTable
ÁÁ (
(
ÁÁ( )
name
ËË 
:
ËË 
$str
ËË 
,
ËË 
schema
ÈÈ 
:
ÈÈ 
$str
ÈÈ 
,
ÈÈ  
columns
ÍÍ 
:
ÍÍ 
table
ÍÍ 
=>
ÍÍ !
new
ÍÍ" %
{
ÎÎ 
Id
ÏÏ 
=
ÏÏ 
table
ÏÏ 
.
ÏÏ 
Column
ÏÏ %
<
ÏÏ% &
int
ÏÏ& )
>
ÏÏ) *
(
ÏÏ* +
type
ÏÏ+ /
:
ÏÏ/ 0
$str
ÏÏ1 6
,
ÏÏ6 7
nullable
ÏÏ8 @
:
ÏÏ@ A
false
ÏÏB G
)
ÏÏG H
.
ÌÌ 

Annotation
ÌÌ #
(
ÌÌ# $
$str
ÌÌ$ 8
,
ÌÌ8 9
$str
ÌÌ: @
)
ÌÌ@ A
,
ÌÌA B
Description
ÓÓ 
=
ÓÓ  !
table
ÓÓ" '
.
ÓÓ' (
Column
ÓÓ( .
<
ÓÓ. /
string
ÓÓ/ 5
>
ÓÓ5 6
(
ÓÓ6 7
type
ÓÓ7 ;
:
ÓÓ; <
$str
ÓÓ= L
,
ÓÓL M
	maxLength
ÓÓN W
:
ÓÓW X
$num
ÓÓY \
,
ÓÓ\ ]
nullable
ÓÓ^ f
:
ÓÓf g
true
ÓÓh l
)
ÓÓl m
,
ÓÓm n
Title
ÔÔ 
=
ÔÔ 
table
ÔÔ !
.
ÔÔ! "
Column
ÔÔ" (
<
ÔÔ( )
string
ÔÔ) /
>
ÔÔ/ 0
(
ÔÔ0 1
type
ÔÔ1 5
:
ÔÔ5 6
$str
ÔÔ7 F
,
ÔÔF G
	maxLength
ÔÔH Q
:
ÔÔQ R
$num
ÔÔS V
,
ÔÔV W
nullable
ÔÔX `
:
ÔÔ` a
true
ÔÔb f
)
ÔÔf g
,
ÔÔg h
ImageId
 
=
 
table
 #
.
# $
Column
$ *
<
* +
int
+ .
>
. /
(
/ 0
type
0 4
:
4 5
$str
6 ;
,
; <
nullable
= E
:
E F
false
G L
)
L M
}
ÒÒ 
,
ÒÒ 
constraints
ÚÚ 
:
ÚÚ 
table
ÚÚ "
=>
ÚÚ# %
{
ÛÛ 
table
ÙÙ 
.
ÙÙ 

PrimaryKey
ÙÙ $
(
ÙÙ$ %
$str
ÙÙ% .
,
ÙÙ. /
x
ÙÙ0 1
=>
ÙÙ2 4
x
ÙÙ5 6
.
ÙÙ6 7
Id
ÙÙ7 9
)
ÙÙ9 :
;
ÙÙ: ;
table
ıı 
.
ıı 

ForeignKey
ıı $
(
ıı$ %
name
ˆˆ 
:
ˆˆ 
$str
ˆˆ 6
,
ˆˆ6 7
column
˜˜ 
:
˜˜ 
x
˜˜  !
=>
˜˜" $
x
˜˜% &
.
˜˜& '
ImageId
˜˜' .
,
˜˜. /
principalSchema
¯¯ '
:
¯¯' (
$str
¯¯) 0
,
¯¯0 1
principalTable
˘˘ &
:
˘˘& '
$str
˘˘( 0
,
˘˘0 1
principalColumn
˙˙ '
:
˙˙' (
$str
˙˙) -
,
˙˙- .
onDelete
˚˚  
:
˚˚  !
ReferentialAction
˚˚" 3
.
˚˚3 4
Cascade
˚˚4 ;
)
˚˚; <
;
˚˚< =
}
¸¸ 
)
¸¸ 
;
¸¸ 
migrationBuilder
˛˛ 
.
˛˛ 
CreateTable
˛˛ (
(
˛˛( )
name
ˇˇ 
:
ˇˇ 
$str
ˇˇ %
,
ˇˇ% &
schema
ÄÄ 
:
ÄÄ 
$str
ÄÄ 
,
ÄÄ  
columns
ÅÅ 
:
ÅÅ 
table
ÅÅ 
=>
ÅÅ !
new
ÅÅ" %
{
ÇÇ 
Id
ÉÉ 
=
ÉÉ 
table
ÉÉ 
.
ÉÉ 
Column
ÉÉ %
<
ÉÉ% &
int
ÉÉ& )
>
ÉÉ) *
(
ÉÉ* +
type
ÉÉ+ /
:
ÉÉ/ 0
$str
ÉÉ1 6
,
ÉÉ6 7
nullable
ÉÉ8 @
:
ÉÉ@ A
false
ÉÉB G
)
ÉÉG H
.
ÑÑ 

Annotation
ÑÑ #
(
ÑÑ# $
$str
ÑÑ$ 8
,
ÑÑ8 9
$str
ÑÑ: @
)
ÑÑ@ A
,
ÑÑA B
Title
ÖÖ 
=
ÖÖ 
table
ÖÖ !
.
ÖÖ! "
Column
ÖÖ" (
<
ÖÖ( )
string
ÖÖ) /
>
ÖÖ/ 0
(
ÖÖ0 1
type
ÖÖ1 5
:
ÖÖ5 6
$str
ÖÖ7 F
,
ÖÖF G
	maxLength
ÖÖH Q
:
ÖÖQ R
$num
ÖÖS V
,
ÖÖV W
nullable
ÖÖX `
:
ÖÖ` a
true
ÖÖb f
)
ÖÖf g
,
ÖÖg h
Alt
ÜÜ 
=
ÜÜ 
table
ÜÜ 
.
ÜÜ  
Column
ÜÜ  &
<
ÜÜ& '
string
ÜÜ' -
>
ÜÜ- .
(
ÜÜ. /
type
ÜÜ/ 3
:
ÜÜ3 4
$str
ÜÜ5 D
,
ÜÜD E
	maxLength
ÜÜF O
:
ÜÜO P
$num
ÜÜQ T
,
ÜÜT U
nullable
ÜÜV ^
:
ÜÜ^ _
true
ÜÜ` d
)
ÜÜd e
,
ÜÜe f
ImageId
áá 
=
áá 
table
áá #
.
áá# $
Column
áá$ *
<
áá* +
int
áá+ .
>
áá. /
(
áá/ 0
type
áá0 4
:
áá4 5
$str
áá6 ;
,
áá; <
nullable
áá= E
:
ááE F
false
ááG L
)
ááL M
}
àà 
,
àà 
constraints
ââ 
:
ââ 
table
ââ "
=>
ââ# %
{
ää 
table
ãã 
.
ãã 

PrimaryKey
ãã $
(
ãã$ %
$str
ãã% 7
,
ãã7 8
x
ãã9 :
=>
ãã; =
x
ãã> ?
.
ãã? @
Id
ãã@ B
)
ããB C
;
ããC D
table
åå 
.
åå 

ForeignKey
åå $
(
åå$ %
name
çç 
:
çç 
$str
çç ?
,
çç? @
column
éé 
:
éé 
x
éé  !
=>
éé" $
x
éé% &
.
éé& '
ImageId
éé' .
,
éé. /
principalSchema
èè '
:
èè' (
$str
èè) 0
,
èè0 1
principalTable
êê &
:
êê& '
$str
êê( 0
,
êê0 1
principalColumn
ëë '
:
ëë' (
$str
ëë) -
,
ëë- .
onDelete
íí  
:
íí  !
ReferentialAction
íí" 3
.
íí3 4
Cascade
íí4 ;
)
íí; <
;
íí< =
}
ìì 
)
ìì 
;
ìì 
migrationBuilder
ïï 
.
ïï 
CreateTable
ïï (
(
ïï( )
name
ññ 
:
ññ 
$str
ññ 
,
ññ 
schema
óó 
:
óó 
$str
óó 
,
óó 
columns
òò 
:
òò 
table
òò 
=>
òò !
new
òò" %
{
ôô 
Id
öö 
=
öö 
table
öö 
.
öö 
Column
öö %
<
öö% &
int
öö& )
>
öö) *
(
öö* +
type
öö+ /
:
öö/ 0
$str
öö1 6
,
öö6 7
nullable
öö8 @
:
öö@ A
false
ööB G
)
ööG H
.
õõ 

Annotation
õõ #
(
õõ# $
$str
õõ$ 8
,
õõ8 9
$str
õõ: @
)
õõ@ A
,
õõA B
Title
úú 
=
úú 
table
úú !
.
úú! "
Column
úú" (
<
úú( )
string
úú) /
>
úú/ 0
(
úú0 1
type
úú1 5
:
úú5 6
$str
úú7 F
,
úúF G
	maxLength
úúH Q
:
úúQ R
$num
úúS V
,
úúV W
nullable
úúX `
:
úú` a
false
úúb g
)
úúg h
,
úúh i
Text
ùù 
=
ùù 
table
ùù  
.
ùù  !
Column
ùù! '
<
ùù' (
string
ùù( .
>
ùù. /
(
ùù/ 0
type
ùù0 4
:
ùù4 5
$str
ùù6 E
,
ùùE F
nullable
ùùG O
:
ùùO P
false
ùùQ V
)
ùùV W
,
ùùW X
URL
ûû 
=
ûû 
table
ûû 
.
ûû  
Column
ûû  &
<
ûû& '
string
ûû' -
>
ûû- .
(
ûû. /
type
ûû/ 3
:
ûû3 4
$str
ûû5 D
,
ûûD E
	maxLength
ûûF O
:
ûûO P
$num
ûûQ T
,
ûûT U
nullable
ûûV ^
:
ûû^ _
false
ûû` e
)
ûûe f
,
ûûf g
ImageId
üü 
=
üü 
table
üü #
.
üü# $
Column
üü$ *
<
üü* +
int
üü+ .
>
üü. /
(
üü/ 0
type
üü0 4
:
üü4 5
$str
üü6 ;
,
üü; <
nullable
üü= E
:
üüE F
true
üüG K
)
üüK L
,
üüL M
CreationDate
††  
=
††! "
table
††# (
.
††( )
Column
††) /
<
††/ 0
DateTime
††0 8
>
††8 9
(
††9 :
type
††: >
:
††> ?
$str
††@ K
,
††K L
nullable
††M U
:
††U V
false
††W \
)
††\ ]
}
°° 
,
°° 
constraints
¢¢ 
:
¢¢ 
table
¢¢ "
=>
¢¢# %
{
££ 
table
§§ 
.
§§ 

PrimaryKey
§§ $
(
§§$ %
$str
§§% .
,
§§. /
x
§§0 1
=>
§§2 4
x
§§5 6
.
§§6 7
Id
§§7 9
)
§§9 :
;
§§: ;
table
•• 
.
•• 

ForeignKey
•• $
(
••$ %
name
¶¶ 
:
¶¶ 
$str
¶¶ 6
,
¶¶6 7
column
ßß 
:
ßß 
x
ßß  !
=>
ßß" $
x
ßß% &
.
ßß& '
ImageId
ßß' .
,
ßß. /
principalSchema
®® '
:
®®' (
$str
®®) 0
,
®®0 1
principalTable
©© &
:
©©& '
$str
©©( 0
,
©©0 1
principalColumn
™™ '
:
™™' (
$str
™™) -
)
™™- .
;
™™. /
}
´´ 
)
´´ 
;
´´ 
migrationBuilder
≠≠ 
.
≠≠ 
CreateTable
≠≠ (
(
≠≠( )
name
ÆÆ 
:
ÆÆ 
$str
ÆÆ  
,
ÆÆ  !
schema
ØØ 
:
ØØ 
$str
ØØ "
,
ØØ" #
columns
∞∞ 
:
∞∞ 
table
∞∞ 
=>
∞∞ !
new
∞∞" %
{
±± 
Id
≤≤ 
=
≤≤ 
table
≤≤ 
.
≤≤ 
Column
≤≤ %
<
≤≤% &
int
≤≤& )
>
≤≤) *
(
≤≤* +
type
≤≤+ /
:
≤≤/ 0
$str
≤≤1 6
,
≤≤6 7
nullable
≤≤8 @
:
≤≤@ A
false
≤≤B G
)
≤≤G H
.
≥≥ 

Annotation
≥≥ #
(
≥≥# $
$str
≥≥$ 8
,
≥≥8 9
$str
≥≥: @
)
≥≥@ A
,
≥≥A B
Title
¥¥ 
=
¥¥ 
table
¥¥ !
.
¥¥! "
Column
¥¥" (
<
¥¥( )
string
¥¥) /
>
¥¥/ 0
(
¥¥0 1
type
¥¥1 5
:
¥¥5 6
$str
¥¥7 F
,
¥¥F G
	maxLength
¥¥H Q
:
¥¥Q R
$num
¥¥S V
,
¥¥V W
nullable
¥¥X `
:
¥¥` a
false
¥¥b g
)
¥¥g h
,
¥¥h i
LogoId
µµ 
=
µµ 
table
µµ "
.
µµ" #
Column
µµ# )
<
µµ) *
int
µµ* -
>
µµ- .
(
µµ. /
type
µµ/ 3
:
µµ3 4
$str
µµ5 :
,
µµ: ;
nullable
µµ< D
:
µµD E
false
µµF K
)
µµK L
,
µµL M
IsKeyPartner
∂∂  
=
∂∂! "
table
∂∂# (
.
∂∂( )
Column
∂∂) /
<
∂∂/ 0
bool
∂∂0 4
>
∂∂4 5
(
∂∂5 6
type
∂∂6 :
:
∂∂: ;
$str
∂∂< A
,
∂∂A B
nullable
∂∂C K
:
∂∂K L
false
∂∂M R
,
∂∂R S
defaultValue
∂∂T `
:
∂∂` a
false
∂∂b g
)
∂∂g h
,
∂∂h i!
IsVisibleEverywhere
∑∑ '
=
∑∑( )
table
∑∑* /
.
∑∑/ 0
Column
∑∑0 6
<
∑∑6 7
bool
∑∑7 ;
>
∑∑; <
(
∑∑< =
type
∑∑= A
:
∑∑A B
$str
∑∑C H
,
∑∑H I
nullable
∑∑J R
:
∑∑R S
false
∑∑T Y
)
∑∑Y Z
,
∑∑Z [
	TargetUrl
∏∏ 
=
∏∏ 
table
∏∏  %
.
∏∏% &
Column
∏∏& ,
<
∏∏, -
string
∏∏- 3
>
∏∏3 4
(
∏∏4 5
type
∏∏5 9
:
∏∏9 :
$str
∏∏; J
,
∏∏J K
	maxLength
∏∏L U
:
∏∏U V
$num
∏∏W Z
,
∏∏Z [
nullable
∏∏\ d
:
∏∏d e
false
∏∏f k
)
∏∏k l
,
∏∏l m
UrlTitle
ππ 
=
ππ 
table
ππ $
.
ππ$ %
Column
ππ% +
<
ππ+ ,
string
ππ, 2
>
ππ2 3
(
ππ3 4
type
ππ4 8
:
ππ8 9
$str
ππ: I
,
ππI J
	maxLength
ππK T
:
ππT U
$num
ππV Y
,
ππY Z
nullable
ππ[ c
:
ππc d
true
ππe i
)
ππi j
,
ππj k
Description
∫∫ 
=
∫∫  !
table
∫∫" '
.
∫∫' (
Column
∫∫( .
<
∫∫. /
string
∫∫/ 5
>
∫∫5 6
(
∫∫6 7
type
∫∫7 ;
:
∫∫; <
$str
∫∫= L
,
∫∫L M
	maxLength
∫∫N W
:
∫∫W X
$num
∫∫Y \
,
∫∫\ ]
nullable
∫∫^ f
:
∫∫f g
true
∫∫h l
)
∫∫l m
}
ªª 
,
ªª 
constraints
ºº 
:
ºº 
table
ºº "
=>
ºº# %
{
ΩΩ 
table
ææ 
.
ææ 

PrimaryKey
ææ $
(
ææ$ %
$str
ææ% 2
,
ææ2 3
x
ææ4 5
=>
ææ6 8
x
ææ9 :
.
ææ: ;
Id
ææ; =
)
ææ= >
;
ææ> ?
table
øø 
.
øø 

ForeignKey
øø $
(
øø$ %
name
¿¿ 
:
¿¿ 
$str
¿¿ 9
,
¿¿9 :
column
¡¡ 
:
¡¡ 
x
¡¡  !
=>
¡¡" $
x
¡¡% &
.
¡¡& '
LogoId
¡¡' -
,
¡¡- .
principalSchema
¬¬ '
:
¬¬' (
$str
¬¬) 0
,
¬¬0 1
principalTable
√√ &
:
√√& '
$str
√√( 0
,
√√0 1
principalColumn
ƒƒ '
:
ƒƒ' (
$str
ƒƒ) -
,
ƒƒ- .
onDelete
≈≈  
:
≈≈  !
ReferentialAction
≈≈" 3
.
≈≈3 4
Cascade
≈≈4 ;
)
≈≈; <
;
≈≈< =
}
∆∆ 
)
∆∆ 
;
∆∆ 
migrationBuilder
»» 
.
»» 
CreateTable
»» (
(
»»( )
name
…… 
:
…… 
$str
…… .
,
……. /
schema
   
:
   
$str
   !
,
  ! "
columns
ÀÀ 
:
ÀÀ 
table
ÀÀ 
=>
ÀÀ !
new
ÀÀ" %
{
ÃÃ 
Id
ÕÕ 
=
ÕÕ 
table
ÕÕ 
.
ÕÕ 
Column
ÕÕ %
<
ÕÕ% &
int
ÕÕ& )
>
ÕÕ) *
(
ÕÕ* +
type
ÕÕ+ /
:
ÕÕ/ 0
$str
ÕÕ1 6
,
ÕÕ6 7
nullable
ÕÕ8 @
:
ÕÕ@ A
false
ÕÕB G
)
ÕÕG H
.
ŒŒ 

Annotation
ŒŒ #
(
ŒŒ# $
$str
ŒŒ$ 8
,
ŒŒ8 9
$str
ŒŒ: @
)
ŒŒ@ A
,
ŒŒA B
Title
œœ 
=
œœ 
table
œœ !
.
œœ! "
Column
œœ" (
<
œœ( )
string
œœ) /
>
œœ/ 0
(
œœ0 1
type
œœ1 5
:
œœ5 6
$str
œœ7 F
,
œœF G
	maxLength
œœH Q
:
œœQ R
$num
œœS V
,
œœV W
nullable
œœX `
:
œœ` a
false
œœb g
)
œœg h
,
œœh i
ImageId
–– 
=
–– 
table
–– #
.
––# $
Column
––$ *
<
––* +
int
––+ .
>
––. /
(
––/ 0
type
––0 4
:
––4 5
$str
––6 ;
,
––; <
nullable
––= E
:
––E F
false
––G L
)
––L M
}
—— 
,
—— 
constraints
““ 
:
““ 
table
““ "
=>
““# %
{
”” 
table
‘‘ 
.
‘‘ 

PrimaryKey
‘‘ $
(
‘‘$ %
$str
‘‘% @
,
‘‘@ A
x
‘‘B C
=>
‘‘D F
x
‘‘G H
.
‘‘H I
Id
‘‘I K
)
‘‘K L
;
‘‘L M
table
’’ 
.
’’ 

ForeignKey
’’ $
(
’’$ %
name
÷÷ 
:
÷÷ 
$str
÷÷ H
,
÷÷H I
column
◊◊ 
:
◊◊ 
x
◊◊  !
=>
◊◊" $
x
◊◊% &
.
◊◊& '
ImageId
◊◊' .
,
◊◊. /
principalSchema
ÿÿ '
:
ÿÿ' (
$str
ÿÿ) 0
,
ÿÿ0 1
principalTable
ŸŸ &
:
ŸŸ& '
$str
ŸŸ( 0
,
ŸŸ0 1
principalColumn
⁄⁄ '
:
⁄⁄' (
$str
⁄⁄) -
,
⁄⁄- .
onDelete
€€  
:
€€  !
ReferentialAction
€€" 3
.
€€3 4
Cascade
€€4 ;
)
€€; <
;
€€< =
}
‹‹ 
)
‹‹ 
;
‹‹ 
migrationBuilder
ﬁﬁ 
.
ﬁﬁ 
CreateTable
ﬁﬁ (
(
ﬁﬁ( )
name
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ $
,
ﬂﬂ$ %
schema
‡‡ 
:
‡‡ 
$str
‡‡ 
,
‡‡ 
columns
·· 
:
·· 
table
·· 
=>
·· !
new
··" %
{
‚‚ 
Id
„„ 
=
„„ 
table
„„ 
.
„„ 
Column
„„ %
<
„„% &
int
„„& )
>
„„) *
(
„„* +
type
„„+ /
:
„„/ 0
$str
„„1 6
,
„„6 7
nullable
„„8 @
:
„„@ A
false
„„B G
)
„„G H
.
‰‰ 

Annotation
‰‰ #
(
‰‰# $
$str
‰‰$ 8
,
‰‰8 9
$str
‰‰: @
)
‰‰@ A
,
‰‰A B
	FirstName
ÂÂ 
=
ÂÂ 
table
ÂÂ  %
.
ÂÂ% &
Column
ÂÂ& ,
<
ÂÂ, -
string
ÂÂ- 3
>
ÂÂ3 4
(
ÂÂ4 5
type
ÂÂ5 9
:
ÂÂ9 :
$str
ÂÂ; I
,
ÂÂI J
	maxLength
ÂÂK T
:
ÂÂT U
$num
ÂÂV X
,
ÂÂX Y
nullable
ÂÂZ b
:
ÂÂb c
false
ÂÂd i
)
ÂÂi j
,
ÂÂj k
LastName
ÊÊ 
=
ÊÊ 
table
ÊÊ $
.
ÊÊ$ %
Column
ÊÊ% +
<
ÊÊ+ ,
string
ÊÊ, 2
>
ÊÊ2 3
(
ÊÊ3 4
type
ÊÊ4 8
:
ÊÊ8 9
$str
ÊÊ: H
,
ÊÊH I
	maxLength
ÊÊJ S
:
ÊÊS T
$num
ÊÊU W
,
ÊÊW X
nullable
ÊÊY a
:
ÊÊa b
true
ÊÊc g
)
ÊÊg h
,
ÊÊh i
Description
ÁÁ 
=
ÁÁ  !
table
ÁÁ" '
.
ÁÁ' (
Column
ÁÁ( .
<
ÁÁ. /
string
ÁÁ/ 5
>
ÁÁ5 6
(
ÁÁ6 7
type
ÁÁ7 ;
:
ÁÁ; <
$str
ÁÁ= L
,
ÁÁL M
	maxLength
ÁÁN W
:
ÁÁW X
$num
ÁÁY \
,
ÁÁ\ ]
nullable
ÁÁ^ f
:
ÁÁf g
false
ÁÁh m
)
ÁÁm n
,
ÁÁn o
IsMain
ËË 
=
ËË 
table
ËË "
.
ËË" #
Column
ËË# )
<
ËË) *
bool
ËË* .
>
ËË. /
(
ËË/ 0
type
ËË0 4
:
ËË4 5
$str
ËË6 ;
,
ËË; <
nullable
ËË= E
:
ËËE F
false
ËËG L
)
ËËL M
,
ËËM N
ImageId
ÈÈ 
=
ÈÈ 
table
ÈÈ #
.
ÈÈ# $
Column
ÈÈ$ *
<
ÈÈ* +
int
ÈÈ+ .
>
ÈÈ. /
(
ÈÈ/ 0
type
ÈÈ0 4
:
ÈÈ4 5
$str
ÈÈ6 ;
,
ÈÈ; <
nullable
ÈÈ= E
:
ÈÈE F
false
ÈÈG L
)
ÈÈL M
}
ÍÍ 
,
ÍÍ 
constraints
ÎÎ 
:
ÎÎ 
table
ÎÎ "
=>
ÎÎ# %
{
ÏÏ 
table
ÌÌ 
.
ÌÌ 

PrimaryKey
ÌÌ $
(
ÌÌ$ %
$str
ÌÌ% 6
,
ÌÌ6 7
x
ÌÌ8 9
=>
ÌÌ: <
x
ÌÌ= >
.
ÌÌ> ?
Id
ÌÌ? A
)
ÌÌA B
;
ÌÌB C
table
ÓÓ 
.
ÓÓ 

ForeignKey
ÓÓ $
(
ÓÓ$ %
name
ÔÔ 
:
ÔÔ 
$str
ÔÔ >
,
ÔÔ> ?
column
 
:
 
x
  !
=>
" $
x
% &
.
& '
ImageId
' .
,
. /
principalSchema
ÒÒ '
:
ÒÒ' (
$str
ÒÒ) 0
,
ÒÒ0 1
principalTable
ÚÚ &
:
ÚÚ& '
$str
ÚÚ( 0
,
ÚÚ0 1
principalColumn
ÛÛ '
:
ÛÛ' (
$str
ÛÛ) -
,
ÛÛ- .
onDelete
ÙÙ  
:
ÙÙ  !
ReferentialAction
ÙÙ" 3
.
ÙÙ3 4
Cascade
ÙÙ4 ;
)
ÙÙ; <
;
ÙÙ< =
}
ıı 
)
ıı 
;
ıı 
migrationBuilder
˜˜ 
.
˜˜ 
CreateTable
˜˜ (
(
˜˜( )
name
¯¯ 
:
¯¯ 
$str
¯¯ %
,
¯¯% &
schema
˘˘ 
:
˘˘ 
$str
˘˘ $
,
˘˘$ %
columns
˙˙ 
:
˙˙ 
table
˙˙ 
=>
˙˙ !
new
˙˙" %
{
˚˚ 
Id
¸¸ 
=
¸¸ 
table
¸¸ 
.
¸¸ 
Column
¸¸ %
<
¸¸% &
int
¸¸& )
>
¸¸) *
(
¸¸* +
type
¸¸+ /
:
¸¸/ 0
$str
¸¸1 6
,
¸¸6 7
nullable
¸¸8 @
:
¸¸@ A
false
¸¸B G
)
¸¸G H
.
˝˝ 

Annotation
˝˝ #
(
˝˝# $
$str
˝˝$ 8
,
˝˝8 9
$str
˝˝: @
)
˝˝@ A
,
˝˝A B
Word
˛˛ 
=
˛˛ 
table
˛˛  
.
˛˛  !
Column
˛˛! '
<
˛˛' (
string
˛˛( .
>
˛˛. /
(
˛˛/ 0
type
˛˛0 4
:
˛˛4 5
$str
˛˛6 D
,
˛˛D E
	maxLength
˛˛F O
:
˛˛O P
$num
˛˛Q S
,
˛˛S T
nullable
˛˛U ]
:
˛˛] ^
false
˛˛_ d
)
˛˛d e
,
˛˛e f
TermId
ˇˇ 
=
ˇˇ 
table
ˇˇ "
.
ˇˇ" #
Column
ˇˇ# )
<
ˇˇ) *
int
ˇˇ* -
>
ˇˇ- .
(
ˇˇ. /
type
ˇˇ/ 3
:
ˇˇ3 4
$str
ˇˇ5 :
,
ˇˇ: ;
nullable
ˇˇ< D
:
ˇˇD E
false
ˇˇF K
)
ˇˇK L
}
ÄÄ 
,
ÄÄ 
constraints
ÅÅ 
:
ÅÅ 
table
ÅÅ "
=>
ÅÅ# %
{
ÇÇ 
table
ÉÉ 
.
ÉÉ 

PrimaryKey
ÉÉ $
(
ÉÉ$ %
$str
ÉÉ% 7
,
ÉÉ7 8
x
ÉÉ9 :
=>
ÉÉ; =
x
ÉÉ> ?
.
ÉÉ? @
Id
ÉÉ@ B
)
ÉÉB C
;
ÉÉC D
table
ÑÑ 
.
ÑÑ 

ForeignKey
ÑÑ $
(
ÑÑ$ %
name
ÖÖ 
:
ÖÖ 
$str
ÖÖ =
,
ÖÖ= >
column
ÜÜ 
:
ÜÜ 
x
ÜÜ  !
=>
ÜÜ" $
x
ÜÜ% &
.
ÜÜ& '
TermId
ÜÜ' -
,
ÜÜ- .
principalSchema
áá '
:
áá' (
$str
áá) 5
,
áá5 6
principalTable
àà &
:
àà& '
$str
àà( /
,
àà/ 0
principalColumn
ââ '
:
ââ' (
$str
ââ) -
,
ââ- .
onDelete
ää  
:
ää  !
ReferentialAction
ää" 3
.
ää3 4
Cascade
ää4 ;
)
ää; <
;
ää< =
}
ãã 
)
ãã 
;
ãã 
migrationBuilder
çç 
.
çç 
CreateTable
çç (
(
çç( )
name
éé 
:
éé 
$str
éé #
,
éé# $
schema
èè 
:
èè 
$str
èè %
,
èè% &
columns
êê 
:
êê 
table
êê 
=>
êê !
new
êê" %
{
ëë 
Id
íí 
=
íí 
table
íí 
.
íí 
Column
íí %
<
íí% &
int
íí& )
>
íí) *
(
íí* +
type
íí+ /
:
íí/ 0
$str
íí1 6
,
íí6 7
nullable
íí8 @
:
íí@ A
false
ííB G
)
ííG H
.
ìì 

Annotation
ìì #
(
ìì# $
$str
ìì$ 8
,
ìì8 9
$str
ìì: @
)
ìì@ A
,
ììA B
Latitude
îî 
=
îî 
table
îî $
.
îî$ %
Column
îî% +
<
îî+ ,
decimal
îî, 3
>
îî3 4
(
îî4 5
type
îî5 9
:
îî9 :
$str
îî; J
,
îîJ K
nullable
îîL T
:
îîT U
false
îîV [
)
îî[ \
,
îî\ ]

Longtitude
ïï 
=
ïï  
table
ïï! &
.
ïï& '
Column
ïï' -
<
ïï- .
decimal
ïï. 5
>
ïï5 6
(
ïï6 7
type
ïï7 ;
:
ïï; <
$str
ïï= L
,
ïïL M
nullable
ïïN V
:
ïïV W
false
ïïX ]
)
ïï] ^
,
ïï^ _
CoordinateType
ññ "
=
ññ# $
table
ññ% *
.
ññ* +
Column
ññ+ 1
<
ññ1 2
string
ññ2 8
>
ññ8 9
(
ññ9 :
type
ññ: >
:
ññ> ?
$str
ññ@ O
,
ññO P
nullable
ññQ Y
:
ññY Z
false
ññ[ `
)
ññ` a
,
ñña b
StreetcodeId
óó  
=
óó! "
table
óó# (
.
óó( )
Column
óó) /
<
óó/ 0
int
óó0 3
>
óó3 4
(
óó4 5
type
óó5 9
:
óó9 :
$str
óó; @
,
óó@ A
nullable
óóB J
:
óóJ K
true
óóL P
)
óóP Q
,
óóQ R
	ToponymId
òò 
=
òò 
table
òò  %
.
òò% &
Column
òò& ,
<
òò, -
int
òò- 0
>
òò0 1
(
òò1 2
type
òò2 6
:
òò6 7
$str
òò8 =
,
òò= >
nullable
òò? G
:
òòG H
true
òòI M
)
òòM N
}
ôô 
,
ôô 
constraints
öö 
:
öö 
table
öö "
=>
öö# %
{
õõ 
table
úú 
.
úú 

PrimaryKey
úú $
(
úú$ %
$str
úú% 5
,
úú5 6
x
úú7 8
=>
úú9 ;
x
úú< =
.
úú= >
Id
úú> @
)
úú@ A
;
úúA B
table
ùù 
.
ùù 

ForeignKey
ùù $
(
ùù$ %
name
ûû 
:
ûû 
$str
ûû G
,
ûûG H
column
üü 
:
üü 
x
üü  !
=>
üü" $
x
üü% &
.
üü& '
StreetcodeId
üü' 3
,
üü3 4
principalSchema
†† '
:
††' (
$str
††) 5
,
††5 6
principalTable
°° &
:
°°& '
$str
°°( 5
,
°°5 6
principalColumn
¢¢ '
:
¢¢' (
$str
¢¢) -
,
¢¢- .
onDelete
££  
:
££  !
ReferentialAction
££" 3
.
££3 4
Cascade
££4 ;
)
££; <
;
££< =
table
§§ 
.
§§ 

ForeignKey
§§ $
(
§§$ %
name
•• 
:
•• 
$str
•• A
,
••A B
column
¶¶ 
:
¶¶ 
x
¶¶  !
=>
¶¶" $
x
¶¶% &
.
¶¶& '
	ToponymId
¶¶' 0
,
¶¶0 1
principalSchema
ßß '
:
ßß' (
$str
ßß) 3
,
ßß3 4
principalTable
®® &
:
®®& '
$str
®®( 2
,
®®2 3
principalColumn
©© '
:
©©' (
$str
©©) -
,
©©- .
onDelete
™™  
:
™™  !
ReferentialAction
™™" 3
.
™™3 4
Cascade
™™4 ;
)
™™; <
;
™™< =
}
´´ 
)
´´ 
;
´´ 
migrationBuilder
≠≠ 
.
≠≠ 
CreateTable
≠≠ (
(
≠≠( )
name
ÆÆ 
:
ÆÆ 
$str
ÆÆ 
,
ÆÆ 
schema
ØØ 
:
ØØ 
$str
ØØ $
,
ØØ$ %
columns
∞∞ 
:
∞∞ 
table
∞∞ 
=>
∞∞ !
new
∞∞" %
{
±± 
Id
≤≤ 
=
≤≤ 
table
≤≤ 
.
≤≤ 
Column
≤≤ %
<
≤≤% &
int
≤≤& )
>
≤≤) *
(
≤≤* +
type
≤≤+ /
:
≤≤/ 0
$str
≤≤1 6
,
≤≤6 7
nullable
≤≤8 @
:
≤≤@ A
false
≤≤B G
)
≤≤G H
.
≥≥ 

Annotation
≥≥ #
(
≥≥# $
$str
≥≥$ 8
,
≥≥8 9
$str
≥≥: @
)
≥≥@ A
,
≥≥A B
Title
¥¥ 
=
¥¥ 
table
¥¥ !
.
¥¥! "
Column
¥¥" (
<
¥¥( )
string
¥¥) /
>
¥¥/ 0
(
¥¥0 1
type
¥¥1 5
:
¥¥5 6
$str
¥¥7 F
,
¥¥F G
	maxLength
¥¥H Q
:
¥¥Q R
$num
¥¥S V
,
¥¥V W
nullable
¥¥X `
:
¥¥` a
false
¥¥b g
)
¥¥g h
,
¥¥h i
FactContent
µµ 
=
µµ  !
table
µµ" '
.
µµ' (
Column
µµ( .
<
µµ. /
string
µµ/ 5
>
µµ5 6
(
µµ6 7
type
µµ7 ;
:
µµ; <
$str
µµ= L
,
µµL M
	maxLength
µµN W
:
µµW X
$num
µµY \
,
µµ\ ]
nullable
µµ^ f
:
µµf g
false
µµh m
)
µµm n
,
µµn o
ImageId
∂∂ 
=
∂∂ 
table
∂∂ #
.
∂∂# $
Column
∂∂$ *
<
∂∂* +
int
∂∂+ .
>
∂∂. /
(
∂∂/ 0
type
∂∂0 4
:
∂∂4 5
$str
∂∂6 ;
,
∂∂; <
nullable
∂∂= E
:
∂∂E F
true
∂∂G K
)
∂∂K L
,
∂∂L M
StreetcodeId
∑∑  
=
∑∑! "
table
∑∑# (
.
∑∑( )
Column
∑∑) /
<
∑∑/ 0
int
∑∑0 3
>
∑∑3 4
(
∑∑4 5
type
∑∑5 9
:
∑∑9 :
$str
∑∑; @
,
∑∑@ A
nullable
∑∑B J
:
∑∑J K
false
∑∑L Q
)
∑∑Q R
}
∏∏ 
,
∏∏ 
constraints
ππ 
:
ππ 
table
ππ "
=>
ππ# %
{
∫∫ 
table
ªª 
.
ªª 

PrimaryKey
ªª $
(
ªª$ %
$str
ªª% /
,
ªª/ 0
x
ªª1 2
=>
ªª3 5
x
ªª6 7
.
ªª7 8
Id
ªª8 :
)
ªª: ;
;
ªª; <
table
ºº 
.
ºº 

ForeignKey
ºº $
(
ºº$ %
name
ΩΩ 
:
ΩΩ 
$str
ΩΩ 7
,
ΩΩ7 8
column
ææ 
:
ææ 
x
ææ  !
=>
ææ" $
x
ææ% &
.
ææ& '
ImageId
ææ' .
,
ææ. /
principalSchema
øø '
:
øø' (
$str
øø) 0
,
øø0 1
principalTable
¿¿ &
:
¿¿& '
$str
¿¿( 0
,
¿¿0 1
principalColumn
¡¡ '
:
¡¡' (
$str
¡¡) -
,
¡¡- .
onDelete
¬¬  
:
¬¬  !
ReferentialAction
¬¬" 3
.
¬¬3 4
Cascade
¬¬4 ;
)
¬¬; <
;
¬¬< =
table
√√ 
.
√√ 

ForeignKey
√√ $
(
√√$ %
name
ƒƒ 
:
ƒƒ 
$str
ƒƒ A
,
ƒƒA B
column
≈≈ 
:
≈≈ 
x
≈≈  !
=>
≈≈" $
x
≈≈% &
.
≈≈& '
StreetcodeId
≈≈' 3
,
≈≈3 4
principalSchema
∆∆ '
:
∆∆' (
$str
∆∆) 5
,
∆∆5 6
principalTable
«« &
:
««& '
$str
««( 5
,
««5 6
principalColumn
»» '
:
»»' (
$str
»») -
,
»»- .
onDelete
……  
:
……  !
ReferentialAction
……" 3
.
……3 4
Cascade
……4 ;
)
……; <
;
……< =
}
   
)
   
;
   
migrationBuilder
ÃÃ 
.
ÃÃ 
CreateTable
ÃÃ (
(
ÃÃ( )
name
ÕÕ 
:
ÕÕ 
$str
ÕÕ '
,
ÕÕ' (
schema
ŒŒ 
:
ŒŒ 
$str
ŒŒ $
,
ŒŒ$ %
columns
œœ 
:
œœ 
table
œœ 
=>
œœ !
new
œœ" %
{
–– 

ObserverId
—— 
=
——  
table
——! &
.
——& '
Column
——' -
<
——- .
int
——. 1
>
——1 2
(
——2 3
type
——3 7
:
——7 8
$str
——9 >
,
——> ?
nullable
——@ H
:
——H I
false
——J O
)
——O P
,
——P Q
TargetId
““ 
=
““ 
table
““ $
.
““$ %
Column
““% +
<
““+ ,
int
““, /
>
““/ 0
(
““0 1
type
““1 5
:
““5 6
$str
““7 <
,
““< =
nullable
““> F
:
““F G
false
““H M
)
““M N
}
”” 
,
”” 
constraints
‘‘ 
:
‘‘ 
table
‘‘ "
=>
‘‘# %
{
’’ 
table
÷÷ 
.
÷÷ 

PrimaryKey
÷÷ $
(
÷÷$ %
$str
÷÷% 9
,
÷÷9 :
x
÷÷; <
=>
÷÷= ?
new
÷÷@ C
{
÷÷D E
x
÷÷F G
.
÷÷G H

ObserverId
÷÷H R
,
÷÷R S
x
÷÷T U
.
÷÷U V
TargetId
÷÷V ^
}
÷÷_ `
)
÷÷` a
;
÷÷a b
table
◊◊ 
.
◊◊ 

ForeignKey
◊◊ $
(
◊◊$ %
name
ÿÿ 
:
ÿÿ 
$str
ÿÿ I
,
ÿÿI J
column
ŸŸ 
:
ŸŸ 
x
ŸŸ  !
=>
ŸŸ" $
x
ŸŸ% &
.
ŸŸ& '

ObserverId
ŸŸ' 1
,
ŸŸ1 2
principalSchema
⁄⁄ '
:
⁄⁄' (
$str
⁄⁄) 5
,
⁄⁄5 6
principalTable
€€ &
:
€€& '
$str
€€( 5
,
€€5 6
principalColumn
‹‹ '
:
‹‹' (
$str
‹‹) -
,
‹‹- .
onDelete
››  
:
››  !
ReferentialAction
››" 3
.
››3 4
Restrict
››4 <
)
››< =
;
››= >
table
ﬁﬁ 
.
ﬁﬁ 

ForeignKey
ﬁﬁ $
(
ﬁﬁ$ %
name
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ G
,
ﬂﬂG H
column
‡‡ 
:
‡‡ 
x
‡‡  !
=>
‡‡" $
x
‡‡% &
.
‡‡& '
TargetId
‡‡' /
,
‡‡/ 0
principalSchema
·· '
:
··' (
$str
··) 5
,
··5 6
principalTable
‚‚ &
:
‚‚& '
$str
‚‚( 5
,
‚‚5 6
principalColumn
„„ '
:
„„' (
$str
„„) -
,
„„- .
onDelete
‰‰  
:
‰‰  !
ReferentialAction
‰‰" 3
.
‰‰3 4
Cascade
‰‰4 ;
)
‰‰; <
;
‰‰< =
}
ÂÂ 
)
ÂÂ 
;
ÂÂ 
migrationBuilder
ÁÁ 
.
ÁÁ 
CreateTable
ÁÁ (
(
ÁÁ( )
name
ËË 
:
ËË 
$str
ËË (
,
ËË( )
schema
ÈÈ 
:
ÈÈ 
$str
ÈÈ $
,
ÈÈ$ %
columns
ÍÍ 
:
ÍÍ 
table
ÍÍ 
=>
ÍÍ !
new
ÍÍ" %
{
ÎÎ 
StreetcodeId
ÏÏ  
=
ÏÏ! "
table
ÏÏ# (
.
ÏÏ( )
Column
ÏÏ) /
<
ÏÏ/ 0
int
ÏÏ0 3
>
ÏÏ3 4
(
ÏÏ4 5
type
ÏÏ5 9
:
ÏÏ9 :
$str
ÏÏ; @
,
ÏÏ@ A
nullable
ÏÏB J
:
ÏÏJ K
false
ÏÏL Q
)
ÏÏQ R
,
ÏÏR S
ImageId
ÌÌ 
=
ÌÌ 
table
ÌÌ #
.
ÌÌ# $
Column
ÌÌ$ *
<
ÌÌ* +
int
ÌÌ+ .
>
ÌÌ. /
(
ÌÌ/ 0
type
ÌÌ0 4
:
ÌÌ4 5
$str
ÌÌ6 ;
,
ÌÌ; <
nullable
ÌÌ= E
:
ÌÌE F
false
ÌÌG L
)
ÌÌL M
}
ÓÓ 
,
ÓÓ 
constraints
ÔÔ 
:
ÔÔ 
table
ÔÔ "
=>
ÔÔ# %
{
 
table
ÒÒ 
.
ÒÒ 

PrimaryKey
ÒÒ $
(
ÒÒ$ %
$str
ÒÒ% :
,
ÒÒ: ;
x
ÒÒ< =
=>
ÒÒ> @
new
ÒÒA D
{
ÒÒE F
x
ÒÒG H
.
ÒÒH I
ImageId
ÒÒI P
,
ÒÒP Q
x
ÒÒR S
.
ÒÒS T
StreetcodeId
ÒÒT `
}
ÒÒa b
)
ÒÒb c
;
ÒÒc d
table
ÚÚ 
.
ÚÚ 

ForeignKey
ÚÚ $
(
ÚÚ$ %
name
ÛÛ 
:
ÛÛ 
$str
ÛÛ B
,
ÛÛB C
column
ÙÙ 
:
ÙÙ 
x
ÙÙ  !
=>
ÙÙ" $
x
ÙÙ% &
.
ÙÙ& '
ImageId
ÙÙ' .
,
ÙÙ. /
principalSchema
ıı '
:
ıı' (
$str
ıı) 0
,
ıı0 1
principalTable
ˆˆ &
:
ˆˆ& '
$str
ˆˆ( 0
,
ˆˆ0 1
principalColumn
˜˜ '
:
˜˜' (
$str
˜˜) -
,
˜˜- .
onDelete
¯¯  
:
¯¯  !
ReferentialAction
¯¯" 3
.
¯¯3 4
Cascade
¯¯4 ;
)
¯¯; <
;
¯¯< =
table
˘˘ 
.
˘˘ 

ForeignKey
˘˘ $
(
˘˘$ %
name
˙˙ 
:
˙˙ 
$str
˙˙ L
,
˙˙L M
column
˚˚ 
:
˚˚ 
x
˚˚  !
=>
˚˚" $
x
˚˚% &
.
˚˚& '
StreetcodeId
˚˚' 3
,
˚˚3 4
principalSchema
¸¸ '
:
¸¸' (
$str
¸¸) 5
,
¸¸5 6
principalTable
˝˝ &
:
˝˝& '
$str
˝˝( 5
,
˝˝5 6
principalColumn
˛˛ '
:
˛˛' (
$str
˛˛) -
,
˛˛- .
onDelete
ˇˇ  
:
ˇˇ  !
ReferentialAction
ˇˇ" 3
.
ˇˇ3 4
Cascade
ˇˇ4 ;
)
ˇˇ; <
;
ˇˇ< =
}
ÄÄ 
)
ÄÄ 
;
ÄÄ 
migrationBuilder
ÇÇ 
.
ÇÇ 
CreateTable
ÇÇ (
(
ÇÇ( )
name
ÉÉ 
:
ÉÉ 
$str
ÉÉ ,
,
ÉÉ, -
schema
ÑÑ 
:
ÑÑ 
$str
ÑÑ %
,
ÑÑ% &
columns
ÖÖ 
:
ÖÖ 
table
ÖÖ 
=>
ÖÖ !
new
ÖÖ" %
{
ÜÜ 
StreetcodeId
áá  
=
áá! "
table
áá# (
.
áá( )
Column
áá) /
<
áá/ 0
int
áá0 3
>
áá3 4
(
áá4 5
type
áá5 9
:
áá9 :
$str
áá; @
,
áá@ A
nullable
ááB J
:
ááJ K
false
ááL Q
)
ááQ R
,
ááR S
TagId
àà 
=
àà 
table
àà !
.
àà! "
Column
àà" (
<
àà( )
int
àà) ,
>
àà, -
(
àà- .
type
àà. 2
:
àà2 3
$str
àà4 9
,
àà9 :
nullable
àà; C
:
ààC D
false
ààE J
)
ààJ K
,
ààK L
	IsVisible
ââ 
=
ââ 
table
ââ  %
.
ââ% &
Column
ââ& ,
<
ââ, -
bool
ââ- 1
>
ââ1 2
(
ââ2 3
type
ââ3 7
:
ââ7 8
$str
ââ9 >
,
ââ> ?
nullable
ââ@ H
:
ââH I
false
ââJ O
)
ââO P
,
ââP Q
Index
ää 
=
ää 
table
ää !
.
ää! "
Column
ää" (
<
ää( )
int
ää) ,
>
ää, -
(
ää- .
type
ää. 2
:
ää2 3
$str
ää4 9
,
ää9 :
nullable
ää; C
:
ääC D
false
ääE J
)
ääJ K
}
ãã 
,
ãã 
constraints
åå 
:
åå 
table
åå "
=>
åå# %
{
çç 
table
éé 
.
éé 

PrimaryKey
éé $
(
éé$ %
$str
éé% >
,
éé> ?
x
éé@ A
=>
ééB D
new
ééE H
{
ééI J
x
ééK L
.
ééL M
StreetcodeId
ééM Y
,
ééY Z
x
éé[ \
.
éé\ ]
TagId
éé] b
}
ééc d
)
ééd e
;
éée f
table
èè 
.
èè 

ForeignKey
èè $
(
èè$ %
name
êê 
:
êê 
$str
êê P
,
êêP Q
column
ëë 
:
ëë 
x
ëë  !
=>
ëë" $
x
ëë% &
.
ëë& '
StreetcodeId
ëë' 3
,
ëë3 4
principalSchema
íí '
:
íí' (
$str
íí) 5
,
íí5 6
principalTable
ìì &
:
ìì& '
$str
ìì( 5
,
ìì5 6
principalColumn
îî '
:
îî' (
$str
îî) -
,
îî- .
onDelete
ïï  
:
ïï  !
ReferentialAction
ïï" 3
.
ïï3 4
Cascade
ïï4 ;
)
ïï; <
;
ïï< =
table
ññ 
.
ññ 

ForeignKey
ññ $
(
ññ$ %
name
óó 
:
óó 
$str
óó B
,
óóB C
column
òò 
:
òò 
x
òò  !
=>
òò" $
x
òò% &
.
òò& '
TagId
òò' ,
,
òò, -
principalSchema
ôô '
:
ôô' (
$str
ôô) 6
,
ôô6 7
principalTable
öö &
:
öö& '
$str
öö( .
,
öö. /
principalColumn
õõ '
:
õõ' (
$str
õõ) -
,
õõ- .
onDelete
úú  
:
úú  !
ReferentialAction
úú" 3
.
úú3 4
Cascade
úú4 ;
)
úú; <
;
úú< =
}
ùù 
)
ùù 
;
ùù 
migrationBuilder
üü 
.
üü 
CreateTable
üü (
(
üü( )
name
†† 
:
†† 
$str
†† *
,
††* +
schema
°° 
:
°° 
$str
°° $
,
°°$ %
columns
¢¢ 
:
¢¢ 
table
¢¢ 
=>
¢¢ !
new
¢¢" %
{
££ 
StreetcodeId
§§  
=
§§! "
table
§§# (
.
§§( )
Column
§§) /
<
§§/ 0
int
§§0 3
>
§§3 4
(
§§4 5
type
§§5 9
:
§§9 :
$str
§§; @
,
§§@ A
nullable
§§B J
:
§§J K
false
§§L Q
)
§§Q R
,
§§R S
	ToponymId
•• 
=
•• 
table
••  %
.
••% &
Column
••& ,
<
••, -
int
••- 0
>
••0 1
(
••1 2
type
••2 6
:
••6 7
$str
••8 =
,
••= >
nullable
••? G
:
••G H
false
••I N
)
••N O
}
¶¶ 
,
¶¶ 
constraints
ßß 
:
ßß 
table
ßß "
=>
ßß# %
{
®® 
table
©© 
.
©© 

PrimaryKey
©© $
(
©©$ %
$str
©©% <
,
©©< =
x
©©> ?
=>
©©@ B
new
©©C F
{
©©G H
x
©©I J
.
©©J K
StreetcodeId
©©K W
,
©©W X
x
©©Y Z
.
©©Z [
	ToponymId
©©[ d
}
©©e f
)
©©f g
;
©©g h
table
™™ 
.
™™ 

ForeignKey
™™ $
(
™™$ %
name
´´ 
:
´´ 
$str
´´ N
,
´´N O
column
¨¨ 
:
¨¨ 
x
¨¨  !
=>
¨¨" $
x
¨¨% &
.
¨¨& '
StreetcodeId
¨¨' 3
,
¨¨3 4
principalSchema
≠≠ '
:
≠≠' (
$str
≠≠) 5
,
≠≠5 6
principalTable
ÆÆ &
:
ÆÆ& '
$str
ÆÆ( 5
,
ÆÆ5 6
principalColumn
ØØ '
:
ØØ' (
$str
ØØ) -
,
ØØ- .
onDelete
∞∞  
:
∞∞  !
ReferentialAction
∞∞" 3
.
∞∞3 4
Cascade
∞∞4 ;
)
∞∞; <
;
∞∞< =
table
±± 
.
±± 

ForeignKey
±± $
(
±±$ %
name
≤≤ 
:
≤≤ 
$str
≤≤ H
,
≤≤H I
column
≥≥ 
:
≥≥ 
x
≥≥  !
=>
≥≥" $
x
≥≥% &
.
≥≥& '
	ToponymId
≥≥' 0
,
≥≥0 1
principalSchema
¥¥ '
:
¥¥' (
$str
¥¥) 3
,
¥¥3 4
principalTable
µµ &
:
µµ& '
$str
µµ( 2
,
µµ2 3
principalColumn
∂∂ '
:
∂∂' (
$str
∂∂) -
,
∂∂- .
onDelete
∑∑  
:
∑∑  !
ReferentialAction
∑∑" 3
.
∑∑3 4
Cascade
∑∑4 ;
)
∑∑; <
;
∑∑< =
}
∏∏ 
)
∏∏ 
;
∏∏ 
migrationBuilder
∫∫ 
.
∫∫ 
CreateTable
∫∫ (
(
∫∫( )
name
ªª 
:
ªª 
$str
ªª !
,
ªª! "
schema
ºº 
:
ºº 
$str
ºº %
,
ºº% &
columns
ΩΩ 
:
ΩΩ 
table
ΩΩ 
=>
ΩΩ !
new
ΩΩ" %
{
ææ 
Id
øø 
=
øø 
table
øø 
.
øø 
Column
øø %
<
øø% &
int
øø& )
>
øø) *
(
øø* +
type
øø+ /
:
øø/ 0
$str
øø1 6
,
øø6 7
nullable
øø8 @
:
øø@ A
false
øøB G
)
øøG H
.
¿¿ 

Annotation
¿¿ #
(
¿¿# $
$str
¿¿$ 8
,
¿¿8 9
$str
¿¿: @
)
¿¿@ A
,
¿¿A B
SubtitleText
¡¡  
=
¡¡! "
table
¡¡# (
.
¡¡( )
Column
¡¡) /
<
¡¡/ 0
string
¡¡0 6
>
¡¡6 7
(
¡¡7 8
type
¡¡8 <
:
¡¡< =
$str
¡¡> M
,
¡¡M N
	maxLength
¡¡O X
:
¡¡X Y
$num
¡¡Z ]
,
¡¡] ^
nullable
¡¡_ g
:
¡¡g h
true
¡¡i m
)
¡¡m n
,
¡¡n o
StreetcodeId
¬¬  
=
¬¬! "
table
¬¬# (
.
¬¬( )
Column
¬¬) /
<
¬¬/ 0
int
¬¬0 3
>
¬¬3 4
(
¬¬4 5
type
¬¬5 9
:
¬¬9 :
$str
¬¬; @
,
¬¬@ A
nullable
¬¬B J
:
¬¬J K
false
¬¬L Q
)
¬¬Q R
}
√√ 
,
√√ 
constraints
ƒƒ 
:
ƒƒ 
table
ƒƒ "
=>
ƒƒ# %
{
≈≈ 
table
∆∆ 
.
∆∆ 

PrimaryKey
∆∆ $
(
∆∆$ %
$str
∆∆% 3
,
∆∆3 4
x
∆∆5 6
=>
∆∆7 9
x
∆∆: ;
.
∆∆; <
Id
∆∆< >
)
∆∆> ?
;
∆∆? @
table
«« 
.
«« 

ForeignKey
«« $
(
««$ %
name
»» 
:
»» 
$str
»» E
,
»»E F
column
…… 
:
…… 
x
……  !
=>
……" $
x
……% &
.
……& '
StreetcodeId
……' 3
,
……3 4
principalSchema
   '
:
  ' (
$str
  ) 5
,
  5 6
principalTable
ÀÀ &
:
ÀÀ& '
$str
ÀÀ( 5
,
ÀÀ5 6
principalColumn
ÃÃ '
:
ÃÃ' (
$str
ÃÃ) -
,
ÃÃ- .
onDelete
ÕÕ  
:
ÕÕ  !
ReferentialAction
ÕÕ" 3
.
ÕÕ3 4
Cascade
ÕÕ4 ;
)
ÕÕ; <
;
ÕÕ< =
}
ŒŒ 
)
ŒŒ 
;
ŒŒ 
migrationBuilder
–– 
.
–– 
CreateTable
–– (
(
––( )
name
—— 
:
—— 
$str
—— 
,
—— 
schema
““ 
:
““ 
$str
““ $
,
““$ %
columns
”” 
:
”” 
table
”” 
=>
”” !
new
””" %
{
‘‘ 
Id
’’ 
=
’’ 
table
’’ 
.
’’ 
Column
’’ %
<
’’% &
int
’’& )
>
’’) *
(
’’* +
type
’’+ /
:
’’/ 0
$str
’’1 6
,
’’6 7
nullable
’’8 @
:
’’@ A
false
’’B G
)
’’G H
.
÷÷ 

Annotation
÷÷ #
(
÷÷# $
$str
÷÷$ 8
,
÷÷8 9
$str
÷÷: @
)
÷÷@ A
,
÷÷A B
Title
◊◊ 
=
◊◊ 
table
◊◊ !
.
◊◊! "
Column
◊◊" (
<
◊◊( )
string
◊◊) /
>
◊◊/ 0
(
◊◊0 1
type
◊◊1 5
:
◊◊5 6
$str
◊◊7 F
,
◊◊F G
	maxLength
◊◊H Q
:
◊◊Q R
$num
◊◊S V
,
◊◊V W
nullable
◊◊X `
:
◊◊` a
false
◊◊b g
)
◊◊g h
,
◊◊h i
TextContent
ÿÿ 
=
ÿÿ  !
table
ÿÿ" '
.
ÿÿ' (
Column
ÿÿ( .
<
ÿÿ. /
string
ÿÿ/ 5
>
ÿÿ5 6
(
ÿÿ6 7
type
ÿÿ7 ;
:
ÿÿ; <
$str
ÿÿ= L
,
ÿÿL M
	maxLength
ÿÿN W
:
ÿÿW X
$num
ÿÿY ^
,
ÿÿ^ _
nullable
ÿÿ` h
:
ÿÿh i
false
ÿÿj o
)
ÿÿo p
,
ÿÿp q
AdditionalText
ŸŸ "
=
ŸŸ# $
table
ŸŸ% *
.
ŸŸ* +
Column
ŸŸ+ 1
<
ŸŸ1 2
string
ŸŸ2 8
>
ŸŸ8 9
(
ŸŸ9 :
type
ŸŸ: >
:
ŸŸ> ?
$str
ŸŸ@ O
,
ŸŸO P
	maxLength
ŸŸQ Z
:
ŸŸZ [
$num
ŸŸ\ _
,
ŸŸ_ `
nullable
ŸŸa i
:
ŸŸi j
true
ŸŸk o
)
ŸŸo p
,
ŸŸp q
StreetcodeId
⁄⁄  
=
⁄⁄! "
table
⁄⁄# (
.
⁄⁄( )
Column
⁄⁄) /
<
⁄⁄/ 0
int
⁄⁄0 3
>
⁄⁄3 4
(
⁄⁄4 5
type
⁄⁄5 9
:
⁄⁄9 :
$str
⁄⁄; @
,
⁄⁄@ A
nullable
⁄⁄B J
:
⁄⁄J K
false
⁄⁄L Q
)
⁄⁄Q R
}
€€ 
,
€€ 
constraints
‹‹ 
:
‹‹ 
table
‹‹ "
=>
‹‹# %
{
›› 
table
ﬁﬁ 
.
ﬁﬁ 

PrimaryKey
ﬁﬁ $
(
ﬁﬁ$ %
$str
ﬁﬁ% /
,
ﬁﬁ/ 0
x
ﬁﬁ1 2
=>
ﬁﬁ3 5
x
ﬁﬁ6 7
.
ﬁﬁ7 8
Id
ﬁﬁ8 :
)
ﬁﬁ: ;
;
ﬁﬁ; <
table
ﬂﬂ 
.
ﬂﬂ 

ForeignKey
ﬂﬂ $
(
ﬂﬂ$ %
name
‡‡ 
:
‡‡ 
$str
‡‡ A
,
‡‡A B
column
·· 
:
·· 
x
··  !
=>
··" $
x
··% &
.
··& '
StreetcodeId
··' 3
,
··3 4
principalSchema
‚‚ '
:
‚‚' (
$str
‚‚) 5
,
‚‚5 6
principalTable
„„ &
:
„„& '
$str
„„( 5
,
„„5 6
principalColumn
‰‰ '
:
‰‰' (
$str
‰‰) -
,
‰‰- .
onDelete
ÂÂ  
:
ÂÂ  !
ReferentialAction
ÂÂ" 3
.
ÂÂ3 4
Cascade
ÂÂ4 ;
)
ÂÂ; <
;
ÂÂ< =
}
ÊÊ 
)
ÊÊ 
;
ÊÊ 
migrationBuilder
ËË 
.
ËË 
CreateTable
ËË (
(
ËË( )
name
ÈÈ 
:
ÈÈ 
$str
ÈÈ &
,
ÈÈ& '
schema
ÍÍ 
:
ÍÍ 
$str
ÍÍ "
,
ÍÍ" #
columns
ÎÎ 
:
ÎÎ 
table
ÎÎ 
=>
ÎÎ !
new
ÎÎ" %
{
ÏÏ 
Id
ÌÌ 
=
ÌÌ 
table
ÌÌ 
.
ÌÌ 
Column
ÌÌ %
<
ÌÌ% &
int
ÌÌ& )
>
ÌÌ) *
(
ÌÌ* +
type
ÌÌ+ /
:
ÌÌ/ 0
$str
ÌÌ1 6
,
ÌÌ6 7
nullable
ÌÌ8 @
:
ÌÌ@ A
false
ÌÌB G
)
ÌÌG H
.
ÓÓ 

Annotation
ÓÓ #
(
ÓÓ# $
$str
ÓÓ$ 8
,
ÓÓ8 9
$str
ÓÓ: @
)
ÓÓ@ A
,
ÓÓA B
Date
ÔÔ 
=
ÔÔ 
table
ÔÔ  
.
ÔÔ  !
Column
ÔÔ! '
<
ÔÔ' (
DateTime
ÔÔ( 0
>
ÔÔ0 1
(
ÔÔ1 2
type
ÔÔ2 6
:
ÔÔ6 7
$str
ÔÔ8 C
,
ÔÔC D
nullable
ÔÔE M
:
ÔÔM N
false
ÔÔO T
)
ÔÔT U
,
ÔÔU V
DateViewPattern
 #
=
$ %
table
& +
.
+ ,
Column
, 2
<
2 3
int
3 6
>
6 7
(
7 8
type
8 <
:
< =
$str
> C
,
C D
nullable
E M
:
M N
false
O T
)
T U
,
U V
Title
ÒÒ 
=
ÒÒ 
table
ÒÒ !
.
ÒÒ! "
Column
ÒÒ" (
<
ÒÒ( )
string
ÒÒ) /
>
ÒÒ/ 0
(
ÒÒ0 1
type
ÒÒ1 5
:
ÒÒ5 6
$str
ÒÒ7 F
,
ÒÒF G
	maxLength
ÒÒH Q
:
ÒÒQ R
$num
ÒÒS V
,
ÒÒV W
nullable
ÒÒX `
:
ÒÒ` a
false
ÒÒb g
)
ÒÒg h
,
ÒÒh i
Description
ÚÚ 
=
ÚÚ  !
table
ÚÚ" '
.
ÚÚ' (
Column
ÚÚ( .
<
ÚÚ. /
string
ÚÚ/ 5
>
ÚÚ5 6
(
ÚÚ6 7
type
ÚÚ7 ;
:
ÚÚ; <
$str
ÚÚ= L
,
ÚÚL M
	maxLength
ÚÚN W
:
ÚÚW X
$num
ÚÚY \
,
ÚÚ\ ]
nullable
ÚÚ^ f
:
ÚÚf g
true
ÚÚh l
)
ÚÚl m
,
ÚÚm n
StreetcodeId
ÛÛ  
=
ÛÛ! "
table
ÛÛ# (
.
ÛÛ( )
Column
ÛÛ) /
<
ÛÛ/ 0
int
ÛÛ0 3
>
ÛÛ3 4
(
ÛÛ4 5
type
ÛÛ5 9
:
ÛÛ9 :
$str
ÛÛ; @
,
ÛÛ@ A
nullable
ÛÛB J
:
ÛÛJ K
false
ÛÛL Q
)
ÛÛQ R
}
ÙÙ 
,
ÙÙ 
constraints
ıı 
:
ıı 
table
ıı "
=>
ıı# %
{
ˆˆ 
table
˜˜ 
.
˜˜ 

PrimaryKey
˜˜ $
(
˜˜$ %
$str
˜˜% 8
,
˜˜8 9
x
˜˜: ;
=>
˜˜< >
x
˜˜? @
.
˜˜@ A
Id
˜˜A C
)
˜˜C D
;
˜˜D E
table
¯¯ 
.
¯¯ 

ForeignKey
¯¯ $
(
¯¯$ %
name
˘˘ 
:
˘˘ 
$str
˘˘ J
,
˘˘J K
column
˙˙ 
:
˙˙ 
x
˙˙  !
=>
˙˙" $
x
˙˙% &
.
˙˙& '
StreetcodeId
˙˙' 3
,
˙˙3 4
principalSchema
˚˚ '
:
˚˚' (
$str
˚˚) 5
,
˚˚5 6
principalTable
¸¸ &
:
¸¸& '
$str
¸¸( 5
,
¸¸5 6
principalColumn
˝˝ '
:
˝˝' (
$str
˝˝) -
,
˝˝- .
onDelete
˛˛  
:
˛˛  !
ReferentialAction
˛˛" 3
.
˛˛3 4
Cascade
˛˛4 ;
)
˛˛; <
;
˛˛< =
}
ˇˇ 
)
ˇˇ 
;
ˇˇ 
migrationBuilder
ÅÅ 
.
ÅÅ 
CreateTable
ÅÅ (
(
ÅÅ( )
name
ÇÇ 
:
ÇÇ 
$str
ÇÇ )
,
ÇÇ) *
schema
ÉÉ 
:
ÉÉ 
$str
ÉÉ &
,
ÉÉ& '
columns
ÑÑ 
:
ÑÑ 
table
ÑÑ 
=>
ÑÑ !
new
ÑÑ" %
{
ÖÖ 
Id
ÜÜ 
=
ÜÜ 
table
ÜÜ 
.
ÜÜ 
Column
ÜÜ %
<
ÜÜ% &
int
ÜÜ& )
>
ÜÜ) *
(
ÜÜ* +
type
ÜÜ+ /
:
ÜÜ/ 0
$str
ÜÜ1 6
,
ÜÜ6 7
nullable
ÜÜ8 @
:
ÜÜ@ A
false
ÜÜB G
)
ÜÜG H
.
áá 

Annotation
áá #
(
áá# $
$str
áá$ 8
,
áá8 9
$str
áá: @
)
áá@ A
,
ááA B
UrlTitle
àà 
=
àà 
table
àà $
.
àà$ %
Column
àà% +
<
àà+ ,
string
àà, 2
>
àà2 3
(
àà3 4
type
àà4 8
:
àà8 9
$str
àà: I
,
ààI J
	maxLength
ààK T
:
ààT U
$num
ààV Y
,
ààY Z
nullable
àà[ c
:
ààc d
true
ààe i
)
àài j
,
ààj k
Url
ââ 
=
ââ 
table
ââ 
.
ââ  
Column
ââ  &
<
ââ& '
string
ââ' -
>
ââ- .
(
ââ. /
type
ââ/ 3
:
ââ3 4
$str
ââ5 D
,
ââD E
	maxLength
ââF O
:
ââO P
$num
ââQ T
,
ââT U
nullable
ââV ^
:
ââ^ _
false
ââ` e
)
ââe f
,
ââf g
StreetcodeId
ää  
=
ää! "
table
ää# (
.
ää( )
Column
ää) /
<
ää/ 0
int
ää0 3
>
ää3 4
(
ää4 5
type
ää5 9
:
ää9 :
$str
ää; @
,
ää@ A
nullable
ääB J
:
ääJ K
false
ääL Q
)
ääQ R
}
ãã 
,
ãã 
constraints
åå 
:
åå 
table
åå "
=>
åå# %
{
çç 
table
éé 
.
éé 

PrimaryKey
éé $
(
éé$ %
$str
éé% ;
,
éé; <
x
éé= >
=>
éé? A
x
ééB C
.
ééC D
Id
ééD F
)
ééF G
;
ééG H
table
èè 
.
èè 

ForeignKey
èè $
(
èè$ %
name
êê 
:
êê 
$str
êê M
,
êêM N
column
ëë 
:
ëë 
x
ëë  !
=>
ëë" $
x
ëë% &
.
ëë& '
StreetcodeId
ëë' 3
,
ëë3 4
principalSchema
íí '
:
íí' (
$str
íí) 5
,
íí5 6
principalTable
ìì &
:
ìì& '
$str
ìì( 5
,
ìì5 6
principalColumn
îî '
:
îî' (
$str
îî) -
,
îî- .
onDelete
ïï  
:
ïï  !
ReferentialAction
ïï" 3
.
ïï3 4
Cascade
ïï4 ;
)
ïï; <
;
ïï< =
}
ññ 
)
ññ 
;
ññ 
migrationBuilder
òò 
.
òò 
CreateTable
òò (
(
òò( )
name
ôô 
:
ôô 
$str
ôô 
,
ôô 
schema
öö 
:
öö 
$str
öö 
,
öö  
columns
õõ 
:
õõ 
table
õõ 
=>
õõ !
new
õõ" %
{
úú 
Id
ùù 
=
ùù 
table
ùù 
.
ùù 
Column
ùù %
<
ùù% &
int
ùù& )
>
ùù) *
(
ùù* +
type
ùù+ /
:
ùù/ 0
$str
ùù1 6
,
ùù6 7
nullable
ùù8 @
:
ùù@ A
false
ùùB G
)
ùùG H
.
ûû 

Annotation
ûû #
(
ûû# $
$str
ûû$ 8
,
ûû8 9
$str
ûû: @
)
ûû@ A
,
ûûA B
Title
üü 
=
üü 
table
üü !
.
üü! "
Column
üü" (
<
üü( )
string
üü) /
>
üü/ 0
(
üü0 1
type
üü1 5
:
üü5 6
$str
üü7 F
,
üüF G
	maxLength
üüH Q
:
üüQ R
$num
üüS V
,
üüV W
nullable
üüX `
:
üü` a
true
üüb f
)
üüf g
,
üüg h
Description
†† 
=
††  !
table
††" '
.
††' (
Column
††( .
<
††. /
string
††/ 5
>
††5 6
(
††6 7
type
††7 ;
:
††; <
$str
††= L
,
††L M
nullable
††N V
:
††V W
true
††X \
)
††\ ]
,
††] ^
Url
°° 
=
°° 
table
°° 
.
°°  
Column
°°  &
<
°°& '
string
°°' -
>
°°- .
(
°°. /
type
°°/ 3
:
°°3 4
$str
°°5 D
,
°°D E
nullable
°°F N
:
°°N O
false
°°P U
)
°°U V
,
°°V W
StreetcodeId
¢¢  
=
¢¢! "
table
¢¢# (
.
¢¢( )
Column
¢¢) /
<
¢¢/ 0
int
¢¢0 3
>
¢¢3 4
(
¢¢4 5
type
¢¢5 9
:
¢¢9 :
$str
¢¢; @
,
¢¢@ A
nullable
¢¢B J
:
¢¢J K
false
¢¢L Q
)
¢¢Q R
}
££ 
,
££ 
constraints
§§ 
:
§§ 
table
§§ "
=>
§§# %
{
•• 
table
¶¶ 
.
¶¶ 

PrimaryKey
¶¶ $
(
¶¶$ %
$str
¶¶% 0
,
¶¶0 1
x
¶¶2 3
=>
¶¶4 6
x
¶¶7 8
.
¶¶8 9
Id
¶¶9 ;
)
¶¶; <
;
¶¶< =
table
ßß 
.
ßß 

ForeignKey
ßß $
(
ßß$ %
name
®® 
:
®® 
$str
®® B
,
®®B C
column
©© 
:
©© 
x
©©  !
=>
©©" $
x
©©% &
.
©©& '
StreetcodeId
©©' 3
,
©©3 4
principalSchema
™™ '
:
™™' (
$str
™™) 5
,
™™5 6
principalTable
´´ &
:
´´& '
$str
´´( 5
,
´´5 6
principalColumn
¨¨ '
:
¨¨' (
$str
¨¨) -
,
¨¨- .
onDelete
≠≠  
:
≠≠  !
ReferentialAction
≠≠" 3
.
≠≠3 4
Cascade
≠≠4 ;
)
≠≠; <
;
≠≠< =
}
ÆÆ 
)
ÆÆ 
;
ÆÆ 
migrationBuilder
∞∞ 
.
∞∞ 
CreateTable
∞∞ (
(
∞∞( )
name
±± 
:
±± 
$str
±± &
,
±±& '
schema
≤≤ 
:
≤≤ 
$str
≤≤ $
,
≤≤$ %
columns
≥≥ 
:
≥≥ 
table
≥≥ 
=>
≥≥ !
new
≥≥" %
{
¥¥ 
StreetcodeId
µµ  
=
µµ! "
table
µµ# (
.
µµ( )
Column
µµ) /
<
µµ/ 0
int
µµ0 3
>
µµ3 4
(
µµ4 5
type
µµ5 9
:
µµ9 :
$str
µµ; @
,
µµ@ A
nullable
µµB J
:
µµJ K
false
µµL Q
)
µµQ R
,
µµR S
ArtId
∂∂ 
=
∂∂ 
table
∂∂ !
.
∂∂! "
Column
∂∂" (
<
∂∂( )
int
∂∂) ,
>
∂∂, -
(
∂∂- .
type
∂∂. 2
:
∂∂2 3
$str
∂∂4 9
,
∂∂9 :
nullable
∂∂; C
:
∂∂C D
false
∂∂E J
)
∂∂J K
,
∂∂K L
Index
∑∑ 
=
∑∑ 
table
∑∑ !
.
∑∑! "
Column
∑∑" (
<
∑∑( )
int
∑∑) ,
>
∑∑, -
(
∑∑- .
type
∑∑. 2
:
∑∑2 3
$str
∑∑4 9
,
∑∑9 :
nullable
∑∑; C
:
∑∑C D
false
∑∑E J
,
∑∑J K
defaultValue
∑∑L X
:
∑∑X Y
$num
∑∑Z [
)
∑∑[ \
}
∏∏ 
,
∏∏ 
constraints
ππ 
:
ππ 
table
ππ "
=>
ππ# %
{
∫∫ 
table
ªª 
.
ªª 

PrimaryKey
ªª $
(
ªª$ %
$str
ªª% 8
,
ªª8 9
x
ªª: ;
=>
ªª< >
new
ªª? B
{
ªªC D
x
ªªE F
.
ªªF G
ArtId
ªªG L
,
ªªL M
x
ªªN O
.
ªªO P
StreetcodeId
ªªP \
}
ªª] ^
)
ªª^ _
;
ªª_ `
table
ºº 
.
ºº 

ForeignKey
ºº $
(
ºº$ %
name
ΩΩ 
:
ΩΩ 
$str
ΩΩ <
,
ΩΩ< =
column
ææ 
:
ææ 
x
ææ  !
=>
ææ" $
x
ææ% &
.
ææ& '
ArtId
ææ' ,
,
ææ, -
principalSchema
øø '
:
øø' (
$str
øø) 0
,
øø0 1
principalTable
¿¿ &
:
¿¿& '
$str
¿¿( .
,
¿¿. /
principalColumn
¡¡ '
:
¡¡' (
$str
¡¡) -
,
¡¡- .
onDelete
¬¬  
:
¬¬  !
ReferentialAction
¬¬" 3
.
¬¬3 4
Cascade
¬¬4 ;
)
¬¬; <
;
¬¬< =
table
√√ 
.
√√ 

ForeignKey
√√ $
(
√√$ %
name
ƒƒ 
:
ƒƒ 
$str
ƒƒ J
,
ƒƒJ K
column
≈≈ 
:
≈≈ 
x
≈≈  !
=>
≈≈" $
x
≈≈% &
.
≈≈& '
StreetcodeId
≈≈' 3
,
≈≈3 4
principalSchema
∆∆ '
:
∆∆' (
$str
∆∆) 5
,
∆∆5 6
principalTable
«« &
:
««& '
$str
««( 5
,
««5 6
principalColumn
»» '
:
»»' (
$str
»») -
,
»»- .
onDelete
……  
:
……  !
ReferentialAction
……" 3
.
……3 4
Cascade
……4 ;
)
……; <
;
……< =
}
   
)
   
;
   
migrationBuilder
ÃÃ 
.
ÃÃ 
CreateTable
ÃÃ (
(
ÃÃ( )
name
ÕÕ 
:
ÕÕ 
$str
ÕÕ ,
,
ÕÕ, -
schema
ŒŒ 
:
ŒŒ 
$str
ŒŒ "
,
ŒŒ" #
columns
œœ 
:
œœ 
table
œœ 
=>
œœ !
new
œœ" %
{
–– 
Id
—— 
=
—— 
table
—— 
.
—— 
Column
—— %
<
——% &
int
——& )
>
——) *
(
——* +
type
——+ /
:
——/ 0
$str
——1 6
,
——6 7
nullable
——8 @
:
——@ A
false
——B G
)
——G H
.
““ 

Annotation
““ #
(
““# $
$str
““$ 8
,
““8 9
$str
““: @
)
““@ A
,
““A B
Title
”” 
=
”” 
table
”” !
.
””! "
Column
””" (
<
””( )
string
””) /
>
””/ 0
(
””0 1
type
””1 5
:
””5 6
$str
””7 E
,
””E F
	maxLength
””G P
:
””P Q
$num
””R T
,
””T U
nullable
””V ^
:
””^ _
false
””` e
)
””e f
,
””f g
LogoType
‘‘ 
=
‘‘ 
table
‘‘ $
.
‘‘$ %
Column
‘‘% +
<
‘‘+ ,
byte
‘‘, 0
>
‘‘0 1
(
‘‘1 2
type
‘‘2 6
:
‘‘6 7
$str
‘‘8 A
,
‘‘A B
nullable
‘‘C K
:
‘‘K L
false
‘‘M R
)
‘‘R S
,
‘‘S T
	TargetUrl
’’ 
=
’’ 
table
’’  %
.
’’% &
Column
’’& ,
<
’’, -
string
’’- 3
>
’’3 4
(
’’4 5
type
’’5 9
:
’’9 :
$str
’’; J
,
’’J K
	maxLength
’’L U
:
’’U V
$num
’’W Z
,
’’Z [
nullable
’’\ d
:
’’d e
false
’’f k
)
’’k l
,
’’l m
	PartnerId
÷÷ 
=
÷÷ 
table
÷÷  %
.
÷÷% &
Column
÷÷& ,
<
÷÷, -
int
÷÷- 0
>
÷÷0 1
(
÷÷1 2
type
÷÷2 6
:
÷÷6 7
$str
÷÷8 =
,
÷÷= >
nullable
÷÷? G
:
÷÷G H
false
÷÷I N
)
÷÷N O
}
◊◊ 
,
◊◊ 
constraints
ÿÿ 
:
ÿÿ 
table
ÿÿ "
=>
ÿÿ# %
{
ŸŸ 
table
⁄⁄ 
.
⁄⁄ 

PrimaryKey
⁄⁄ $
(
⁄⁄$ %
$str
⁄⁄% >
,
⁄⁄> ?
x
⁄⁄@ A
=>
⁄⁄B D
x
⁄⁄E F
.
⁄⁄F G
Id
⁄⁄G I
)
⁄⁄I J
;
⁄⁄J K
table
€€ 
.
€€ 

ForeignKey
€€ $
(
€€$ %
name
‹‹ 
:
‹‹ 
$str
‹‹ J
,
‹‹J K
column
›› 
:
›› 
x
››  !
=>
››" $
x
››% &
.
››& '
	PartnerId
››' 0
,
››0 1
principalSchema
ﬁﬁ '
:
ﬁﬁ' (
$str
ﬁﬁ) 3
,
ﬁﬁ3 4
principalTable
ﬂﬂ &
:
ﬂﬂ& '
$str
ﬂﬂ( 2
,
ﬂﬂ2 3
principalColumn
‡‡ '
:
‡‡' (
$str
‡‡) -
,
‡‡- .
onDelete
··  
:
··  !
ReferentialAction
··" 3
.
··3 4
Cascade
··4 ;
)
··; <
;
··< =
}
‚‚ 
)
‚‚ 
;
‚‚ 
migrationBuilder
‰‰ 
.
‰‰ 
CreateTable
‰‰ (
(
‰‰( )
name
ÂÂ 
:
ÂÂ 
$str
ÂÂ +
,
ÂÂ+ ,
schema
ÊÊ 
:
ÊÊ 
$str
ÊÊ $
,
ÊÊ$ %
columns
ÁÁ 
:
ÁÁ 
table
ÁÁ 
=>
ÁÁ !
new
ÁÁ" %
{
ËË 
StreetcodeId
ÈÈ  
=
ÈÈ! "
table
ÈÈ# (
.
ÈÈ( )
Column
ÈÈ) /
<
ÈÈ/ 0
int
ÈÈ0 3
>
ÈÈ3 4
(
ÈÈ4 5
type
ÈÈ5 9
:
ÈÈ9 :
$str
ÈÈ; @
,
ÈÈ@ A
nullable
ÈÈB J
:
ÈÈJ K
false
ÈÈL Q
)
ÈÈQ R
,
ÈÈR S
	PartnerId
ÍÍ 
=
ÍÍ 
table
ÍÍ  %
.
ÍÍ% &
Column
ÍÍ& ,
<
ÍÍ, -
int
ÍÍ- 0
>
ÍÍ0 1
(
ÍÍ1 2
type
ÍÍ2 6
:
ÍÍ6 7
$str
ÍÍ8 =
,
ÍÍ= >
nullable
ÍÍ? G
:
ÍÍG H
false
ÍÍI N
)
ÍÍN O
}
ÎÎ 
,
ÎÎ 
constraints
ÏÏ 
:
ÏÏ 
table
ÏÏ "
=>
ÏÏ# %
{
ÌÌ 
table
ÓÓ 
.
ÓÓ 

PrimaryKey
ÓÓ $
(
ÓÓ$ %
$str
ÓÓ% =
,
ÓÓ= >
x
ÓÓ? @
=>
ÓÓA C
new
ÓÓD G
{
ÓÓH I
x
ÓÓJ K
.
ÓÓK L
	PartnerId
ÓÓL U
,
ÓÓU V
x
ÓÓW X
.
ÓÓX Y
StreetcodeId
ÓÓY e
}
ÓÓf g
)
ÓÓg h
;
ÓÓh i
table
ÔÔ 
.
ÔÔ 

ForeignKey
ÔÔ $
(
ÔÔ$ %
name
 
:
 
$str
 I
,
I J
column
ÒÒ 
:
ÒÒ 
x
ÒÒ  !
=>
ÒÒ" $
x
ÒÒ% &
.
ÒÒ& '
	PartnerId
ÒÒ' 0
,
ÒÒ0 1
principalSchema
ÚÚ '
:
ÚÚ' (
$str
ÚÚ) 3
,
ÚÚ3 4
principalTable
ÛÛ &
:
ÛÛ& '
$str
ÛÛ( 2
,
ÛÛ2 3
principalColumn
ÙÙ '
:
ÙÙ' (
$str
ÙÙ) -
,
ÙÙ- .
onDelete
ıı  
:
ıı  !
ReferentialAction
ıı" 3
.
ıı3 4
Cascade
ıı4 ;
)
ıı; <
;
ıı< =
table
ˆˆ 
.
ˆˆ 

ForeignKey
ˆˆ $
(
ˆˆ$ %
name
˜˜ 
:
˜˜ 
$str
˜˜ O
,
˜˜O P
column
¯¯ 
:
¯¯ 
x
¯¯  !
=>
¯¯" $
x
¯¯% &
.
¯¯& '
StreetcodeId
¯¯' 3
,
¯¯3 4
principalSchema
˘˘ '
:
˘˘' (
$str
˘˘) 5
,
˘˘5 6
principalTable
˙˙ &
:
˙˙& '
$str
˙˙( 5
,
˙˙5 6
principalColumn
˚˚ '
:
˚˚' (
$str
˚˚) -
,
˚˚- .
onDelete
¸¸  
:
¸¸  !
ReferentialAction
¸¸" 3
.
¸¸3 4
Cascade
¸¸4 ;
)
¸¸; <
;
¸¸< =
}
˝˝ 
)
˝˝ 
;
˝˝ 
migrationBuilder
ˇˇ 
.
ˇˇ 
CreateTable
ˇˇ (
(
ˇˇ( )
name
ÄÄ 
:
ÄÄ 
$str
ÄÄ 9
,
ÄÄ9 :
schema
ÅÅ 
:
ÅÅ 
$str
ÅÅ !
,
ÅÅ! "
columns
ÇÇ 
:
ÇÇ 
table
ÇÇ 
=>
ÇÇ !
new
ÇÇ" %
{
ÉÉ "
SourceLinkCategoryId
ÑÑ (
=
ÑÑ) *
table
ÑÑ+ 0
.
ÑÑ0 1
Column
ÑÑ1 7
<
ÑÑ7 8
int
ÑÑ8 ;
>
ÑÑ; <
(
ÑÑ< =
type
ÑÑ= A
:
ÑÑA B
$str
ÑÑC H
,
ÑÑH I
nullable
ÑÑJ R
:
ÑÑR S
false
ÑÑT Y
)
ÑÑY Z
,
ÑÑZ [
StreetcodeId
ÖÖ  
=
ÖÖ! "
table
ÖÖ# (
.
ÖÖ( )
Column
ÖÖ) /
<
ÖÖ/ 0
int
ÖÖ0 3
>
ÖÖ3 4
(
ÖÖ4 5
type
ÖÖ5 9
:
ÖÖ9 :
$str
ÖÖ; @
,
ÖÖ@ A
nullable
ÖÖB J
:
ÖÖJ K
false
ÖÖL Q
)
ÖÖQ R
,
ÖÖR S
Text
ÜÜ 
=
ÜÜ 
table
ÜÜ  
.
ÜÜ  !
Column
ÜÜ! '
<
ÜÜ' (
string
ÜÜ( .
>
ÜÜ. /
(
ÜÜ/ 0
type
ÜÜ0 4
:
ÜÜ4 5
$str
ÜÜ6 F
,
ÜÜF G
	maxLength
ÜÜH Q
:
ÜÜQ R
$num
ÜÜS W
,
ÜÜW X
nullable
ÜÜY a
:
ÜÜa b
false
ÜÜc h
)
ÜÜh i
}
áá 
,
áá 
constraints
àà 
:
àà 
table
àà "
=>
àà# %
{
ââ 
table
ää 
.
ää 

PrimaryKey
ää $
(
ää$ %
$str
ää% K
,
ääK L
x
ääM N
=>
ääO Q
new
ääR U
{
ääV W
x
ääX Y
.
ääY Z"
SourceLinkCategoryId
ääZ n
,
ään o
x
ääp q
.
ääq r
StreetcodeId
äär ~
}ää Ä
)ääÄ Å
;ääÅ Ç
table
ãã 
.
ãã 

ForeignKey
ãã $
(
ãã$ %
name
åå 
:
åå 
$str
åå p
,
ååp q
column
çç 
:
çç 
x
çç  !
=>
çç" $
x
çç% &
.
çç& '"
SourceLinkCategoryId
çç' ;
,
çç; <
principalSchema
éé '
:
éé' (
$str
éé) 2
,
éé2 3
principalTable
èè &
:
èè& '
$str
èè( @
,
èè@ A
principalColumn
êê '
:
êê' (
$str
êê) -
,
êê- .
onDelete
ëë  
:
ëë  !
ReferentialAction
ëë" 3
.
ëë3 4
Cascade
ëë4 ;
)
ëë; <
;
ëë< =
table
íí 
.
íí 

ForeignKey
íí $
(
íí$ %
name
ìì 
:
ìì 
$str
ìì ]
,
ìì] ^
column
îî 
:
îî 
x
îî  !
=>
îî" $
x
îî% &
.
îî& '
StreetcodeId
îî' 3
,
îî3 4
principalSchema
ïï '
:
ïï' (
$str
ïï) 5
,
ïï5 6
principalTable
ññ &
:
ññ& '
$str
ññ( 5
,
ññ5 6
principalColumn
óó '
:
óó' (
$str
óó) -
,
óó- .
onDelete
òò  
:
òò  !
ReferentialAction
òò" 3
.
òò3 4
Cascade
òò4 ;
)
òò; <
;
òò< =
}
ôô 
)
ôô 
;
ôô 
migrationBuilder
õõ 
.
õõ 
CreateTable
õõ (
(
õõ( )
name
úú 
:
úú 
$str
úú )
,
úú) *
schema
ùù 
:
ùù 
$str
ùù 
,
ùù 
columns
ûû 
:
ûû 
table
ûû 
=>
ûû !
new
ûû" %
{
üü 
Id
†† 
=
†† 
table
†† 
.
†† 
Column
†† %
<
††% &
int
††& )
>
††) *
(
††* +
type
††+ /
:
††/ 0
$str
††1 6
,
††6 7
nullable
††8 @
:
††@ A
false
††B G
)
††G H
.
°° 

Annotation
°° #
(
°°# $
$str
°°$ 8
,
°°8 9
$str
°°: @
)
°°@ A
,
°°A B
LogoType
¢¢ 
=
¢¢ 
table
¢¢ $
.
¢¢$ %
Column
¢¢% +
<
¢¢+ ,
byte
¢¢, 0
>
¢¢0 1
(
¢¢1 2
type
¢¢2 6
:
¢¢6 7
$str
¢¢8 A
,
¢¢A B
nullable
¢¢C K
:
¢¢K L
false
¢¢M R
)
¢¢R S
,
¢¢S T
	TargetUrl
££ 
=
££ 
table
££  %
.
££% &
Column
££& ,
<
££, -
string
££- 3
>
££3 4
(
££4 5
type
££5 9
:
££9 :
$str
££; J
,
££J K
	maxLength
££L U
:
££U V
$num
££W Z
,
££Z [
nullable
££\ d
:
££d e
false
££f k
)
££k l
,
££l m
TeamMemberId
§§  
=
§§! "
table
§§# (
.
§§( )
Column
§§) /
<
§§/ 0
int
§§0 3
>
§§3 4
(
§§4 5
type
§§5 9
:
§§9 :
$str
§§; @
,
§§@ A
nullable
§§B J
:
§§J K
false
§§L Q
)
§§Q R
}
•• 
,
•• 
constraints
¶¶ 
:
¶¶ 
table
¶¶ "
=>
¶¶# %
{
ßß 
table
®® 
.
®® 

PrimaryKey
®® $
(
®®$ %
$str
®®% ;
,
®®; <
x
®®= >
=>
®®? A
x
®®B C
.
®®C D
Id
®®D F
)
®®F G
;
®®G H
table
©© 
.
©© 

ForeignKey
©© $
(
©©$ %
name
™™ 
:
™™ 
$str
™™ N
,
™™N O
column
´´ 
:
´´ 
x
´´  !
=>
´´" $
x
´´% &
.
´´& '
TeamMemberId
´´' 3
,
´´3 4
principalSchema
¨¨ '
:
¨¨' (
$str
¨¨) /
,
¨¨/ 0
principalTable
≠≠ &
:
≠≠& '
$str
≠≠( 6
,
≠≠6 7
principalColumn
ÆÆ '
:
ÆÆ' (
$str
ÆÆ) -
,
ÆÆ- .
onDelete
ØØ  
:
ØØ  !
ReferentialAction
ØØ" 3
.
ØØ3 4
Cascade
ØØ4 ;
)
ØØ; <
;
ØØ< =
}
∞∞ 
)
∞∞ 
;
∞∞ 
migrationBuilder
≤≤ 
.
≤≤ 
CreateTable
≤≤ (
(
≤≤( )
name
≥≥ 
:
≥≥ 
$str
≥≥ -
,
≥≥- .
schema
¥¥ 
:
¥¥ 
$str
¥¥ 
,
¥¥ 
columns
µµ 
:
µµ 
table
µµ 
=>
µµ !
new
µµ" %
{
∂∂ 
TeamMemberId
∑∑  
=
∑∑! "
table
∑∑# (
.
∑∑( )
Column
∑∑) /
<
∑∑/ 0
int
∑∑0 3
>
∑∑3 4
(
∑∑4 5
type
∑∑5 9
:
∑∑9 :
$str
∑∑; @
,
∑∑@ A
nullable
∑∑B J
:
∑∑J K
false
∑∑L Q
)
∑∑Q R
,
∑∑R S
PositionsId
∏∏ 
=
∏∏  !
table
∏∏" '
.
∏∏' (
Column
∏∏( .
<
∏∏. /
int
∏∏/ 2
>
∏∏2 3
(
∏∏3 4
type
∏∏4 8
:
∏∏8 9
$str
∏∏: ?
,
∏∏? @
nullable
∏∏A I
:
∏∏I J
false
∏∏K P
)
∏∏P Q
}
ππ 
,
ππ 
constraints
∫∫ 
:
∫∫ 
table
∫∫ "
=>
∫∫# %
{
ªª 
table
ºº 
.
ºº 

PrimaryKey
ºº $
(
ºº$ %
$str
ºº% ?
,
ºº? @
x
ººA B
=>
ººC E
new
ººF I
{
ººJ K
x
ººL M
.
ººM N
TeamMemberId
ººN Z
,
ººZ [
x
ºº\ ]
.
ºº] ^
PositionsId
ºº^ i
}
ººj k
)
ººk l
;
ººl m
table
ΩΩ 
.
ΩΩ 

ForeignKey
ΩΩ $
(
ΩΩ$ %
name
ææ 
:
ææ 
$str
ææ N
,
ææN O
column
øø 
:
øø 
x
øø  !
=>
øø" $
x
øø% &
.
øø& '
PositionsId
øø' 2
,
øø2 3
principalSchema
¿¿ '
:
¿¿' (
$str
¿¿) /
,
¿¿/ 0
principalTable
¡¡ &
:
¡¡& '
$str
¡¡( 3
,
¡¡3 4
principalColumn
¬¬ '
:
¬¬' (
$str
¬¬) -
,
¬¬- .
onDelete
√√  
:
√√  !
ReferentialAction
√√" 3
.
√√3 4
Cascade
√√4 ;
)
√√; <
;
√√< =
table
ƒƒ 
.
ƒƒ 

ForeignKey
ƒƒ $
(
ƒƒ$ %
name
≈≈ 
:
≈≈ 
$str
≈≈ R
,
≈≈R S
column
∆∆ 
:
∆∆ 
x
∆∆  !
=>
∆∆" $
x
∆∆% &
.
∆∆& '
TeamMemberId
∆∆' 3
,
∆∆3 4
principalSchema
«« '
:
««' (
$str
««) /
,
««/ 0
principalTable
»» &
:
»»& '
$str
»»( 6
,
»»6 7
principalColumn
…… '
:
……' (
$str
……) -
,
……- .
onDelete
    
:
    !
ReferentialAction
  " 3
.
  3 4
Cascade
  4 ;
)
  ; <
;
  < =
}
ÀÀ 
)
ÀÀ 
;
ÀÀ 
migrationBuilder
ÕÕ 
.
ÕÕ 
CreateTable
ÕÕ (
(
ÕÕ( )
name
ŒŒ 
:
ŒŒ 
$str
ŒŒ &
,
ŒŒ& '
schema
œœ 
:
œœ 
$str
œœ %
,
œœ% &
columns
–– 
:
–– 
table
–– 
=>
–– !
new
––" %
{
—— 
Id
““ 
=
““ 
table
““ 
.
““ 
Column
““ %
<
““% &
int
““& )
>
““) *
(
““* +
type
““+ /
:
““/ 0
$str
““1 6
,
““6 7
nullable
““8 @
:
““@ A
false
““B G
)
““G H
.
”” 

Annotation
”” #
(
””# $
$str
””$ 8
,
””8 9
$str
””: @
)
””@ A
,
””A B
QrId
‘‘ 
=
‘‘ 
table
‘‘  
.
‘‘  !
Column
‘‘! '
<
‘‘' (
int
‘‘( +
>
‘‘+ ,
(
‘‘, -
type
‘‘- 1
:
‘‘1 2
$str
‘‘3 8
,
‘‘8 9
nullable
‘‘: B
:
‘‘B C
false
‘‘D I
)
‘‘I J
,
‘‘J K
Count
’’ 
=
’’ 
table
’’ !
.
’’! "
Column
’’" (
<
’’( )
int
’’) ,
>
’’, -
(
’’- .
type
’’. 2
:
’’2 3
$str
’’4 9
,
’’9 :
nullable
’’; C
:
’’C D
false
’’E J
)
’’J K
,
’’K L
Address
÷÷ 
=
÷÷ 
table
÷÷ #
.
÷÷# $
Column
÷÷$ *
<
÷÷* +
string
÷÷+ 1
>
÷÷1 2
(
÷÷2 3
type
÷÷3 7
:
÷÷7 8
$str
÷÷9 H
,
÷÷H I
	maxLength
÷÷J S
:
÷÷S T
$num
÷÷U X
,
÷÷X Y
nullable
÷÷Z b
:
÷÷b c
false
÷÷d i
)
÷÷i j
,
÷÷j k
StreetcodeId
◊◊  
=
◊◊! "
table
◊◊# (
.
◊◊( )
Column
◊◊) /
<
◊◊/ 0
int
◊◊0 3
>
◊◊3 4
(
◊◊4 5
type
◊◊5 9
:
◊◊9 :
$str
◊◊; @
,
◊◊@ A
nullable
◊◊B J
:
◊◊J K
false
◊◊L Q
)
◊◊Q R
,
◊◊R S$
StreetcodeCoordinateId
ÿÿ *
=
ÿÿ+ ,
table
ÿÿ- 2
.
ÿÿ2 3
Column
ÿÿ3 9
<
ÿÿ9 :
int
ÿÿ: =
>
ÿÿ= >
(
ÿÿ> ?
type
ÿÿ? C
:
ÿÿC D
$str
ÿÿE J
,
ÿÿJ K
nullable
ÿÿL T
:
ÿÿT U
false
ÿÿV [
)
ÿÿ[ \
}
ŸŸ 
,
ŸŸ 
constraints
⁄⁄ 
:
⁄⁄ 
table
⁄⁄ "
=>
⁄⁄# %
{
€€ 
table
‹‹ 
.
‹‹ 

PrimaryKey
‹‹ $
(
‹‹$ %
$str
‹‹% 8
,
‹‹8 9
x
‹‹: ;
=>
‹‹< >
x
‹‹? @
.
‹‹@ A
Id
‹‹A C
)
‹‹C D
;
‹‹D E
table
›› 
.
›› 

ForeignKey
›› $
(
››$ %
name
ﬁﬁ 
:
ﬁﬁ 
$str
ﬁﬁ T
,
ﬁﬁT U
column
ﬂﬂ 
:
ﬂﬂ 
x
ﬂﬂ  !
=>
ﬂﬂ" $
x
ﬂﬂ% &
.
ﬂﬂ& '$
StreetcodeCoordinateId
ﬂﬂ' =
,
ﬂﬂ= >
principalSchema
‡‡ '
:
‡‡' (
$str
‡‡) 6
,
‡‡6 7
principalTable
·· &
:
··& '
$str
··( 5
,
··5 6
principalColumn
‚‚ '
:
‚‚' (
$str
‚‚) -
,
‚‚- .
onDelete
„„  
:
„„  !
ReferentialAction
„„" 3
.
„„3 4
Cascade
„„4 ;
)
„„; <
;
„„< =
table
‰‰ 
.
‰‰ 

ForeignKey
‰‰ $
(
‰‰$ %
name
ÂÂ 
:
ÂÂ 
$str
ÂÂ J
,
ÂÂJ K
column
ÊÊ 
:
ÊÊ 
x
ÊÊ  !
=>
ÊÊ" $
x
ÊÊ% &
.
ÊÊ& '
StreetcodeId
ÊÊ' 3
,
ÊÊ3 4
principalSchema
ÁÁ '
:
ÁÁ' (
$str
ÁÁ) 5
,
ÁÁ5 6
principalTable
ËË &
:
ËË& '
$str
ËË( 5
,
ËË5 6
principalColumn
ÈÈ '
:
ÈÈ' (
$str
ÈÈ) -
)
ÈÈ- .
;
ÈÈ. /
}
ÍÍ 
)
ÍÍ 
;
ÍÍ 
migrationBuilder
ÏÏ 
.
ÏÏ 
CreateTable
ÏÏ (
(
ÏÏ( )
name
ÌÌ 
:
ÌÌ 
$str
ÌÌ 3
,
ÌÌ3 4
columns
ÓÓ 
:
ÓÓ 
table
ÓÓ 
=>
ÓÓ !
new
ÓÓ" %
{
ÔÔ !
HistoricalContextId
 '
=
( )
table
* /
.
/ 0
Column
0 6
<
6 7
int
7 :
>
: ;
(
; <
type
< @
:
@ A
$str
B G
,
G H
nullable
I Q
:
Q R
false
S X
)
X Y
,
Y Z

TimelineId
ÒÒ 
=
ÒÒ  
table
ÒÒ! &
.
ÒÒ& '
Column
ÒÒ' -
<
ÒÒ- .
int
ÒÒ. 1
>
ÒÒ1 2
(
ÒÒ2 3
type
ÒÒ3 7
:
ÒÒ7 8
$str
ÒÒ9 >
,
ÒÒ> ?
nullable
ÒÒ@ H
:
ÒÒH I
false
ÒÒJ O
)
ÒÒO P
}
ÚÚ 
,
ÚÚ 
constraints
ÛÛ 
:
ÛÛ 
table
ÛÛ "
=>
ÛÛ# %
{
ÙÙ 
table
ıı 
.
ıı 

PrimaryKey
ıı $
(
ıı$ %
$str
ıı% E
,
ııE F
x
ııG H
=>
ııI K
new
ııL O
{
ııP Q
x
ııR S
.
ııS T

TimelineId
ııT ^
,
ıı^ _
x
ıı` a
.
ııa b!
HistoricalContextId
ııb u
}
ııv w
)
ııw x
;
ııx y
table
ˆˆ 
.
ˆˆ 

ForeignKey
ˆˆ $
(
ˆˆ$ %
name
˜˜ 
:
˜˜ 
$str
˜˜ f
,
˜˜f g
column
¯¯ 
:
¯¯ 
x
¯¯  !
=>
¯¯" $
x
¯¯% &
.
¯¯& '!
HistoricalContextId
¯¯' :
,
¯¯: ;
principalSchema
˘˘ '
:
˘˘' (
$str
˘˘) 3
,
˘˘3 4
principalTable
˙˙ &
:
˙˙& '
$str
˙˙( =
,
˙˙= >
principalColumn
˚˚ '
:
˚˚' (
$str
˚˚) -
,
˚˚- .
onDelete
¸¸  
:
¸¸  !
ReferentialAction
¸¸" 3
.
¸¸3 4
Cascade
¸¸4 ;
)
¸¸; <
;
¸¸< =
table
˝˝ 
.
˝˝ 

ForeignKey
˝˝ $
(
˝˝$ %
name
˛˛ 
:
˛˛ 
$str
˛˛ X
,
˛˛X Y
column
ˇˇ 
:
ˇˇ 
x
ˇˇ  !
=>
ˇˇ" $
x
ˇˇ% &
.
ˇˇ& '

TimelineId
ˇˇ' 1
,
ˇˇ1 2
principalSchema
ÄÄ '
:
ÄÄ' (
$str
ÄÄ) 3
,
ÄÄ3 4
principalTable
ÅÅ &
:
ÅÅ& '
$str
ÅÅ( 8
,
ÅÅ8 9
principalColumn
ÇÇ '
:
ÇÇ' (
$str
ÇÇ) -
,
ÇÇ- .
onDelete
ÉÉ  
:
ÉÉ  !
ReferentialAction
ÉÉ" 3
.
ÉÉ3 4
Cascade
ÉÉ4 ;
)
ÉÉ; <
;
ÉÉ< =
}
ÑÑ 
)
ÑÑ 
;
ÑÑ 
migrationBuilder
ÜÜ 
.
ÜÜ 
CreateIndex
ÜÜ (
(
ÜÜ( )
name
áá 
:
áá 
$str
áá '
,
áá' (
schema
àà 
:
àà 
$str
àà 
,
àà  
table
ââ 
:
ââ 
$str
ââ 
,
ââ 
column
ää 
:
ää 
$str
ää !
,
ää! "
unique
ãã 
:
ãã 
true
ãã 
)
ãã 
;
ãã 
migrationBuilder
çç 
.
çç 
CreateIndex
çç (
(
çç( )
name
éé 
:
éé 
$str
éé 3
,
éé3 4
schema
èè 
:
èè 
$str
èè %
,
èè% &
table
êê 
:
êê 
$str
êê $
,
êê$ %
column
ëë 
:
ëë 
$str
ëë &
)
ëë& '
;
ëë' (
migrationBuilder
ìì 
.
ìì 
CreateIndex
ìì (
(
ìì( )
name
îî 
:
îî 
$str
îî 0
,
îî0 1
schema
ïï 
:
ïï 
$str
ïï %
,
ïï% &
table
ññ 
:
ññ 
$str
ññ $
,
ññ$ %
column
óó 
:
óó 
$str
óó #
,
óó# $
unique
òò 
:
òò 
true
òò 
,
òò 
filter
ôô 
:
ôô 
$str
ôô 1
)
ôô1 2
;
ôô2 3
migrationBuilder
õõ 
.
õõ 
CreateIndex
õõ (
(
õõ( )
name
úú 
:
úú 
$str
úú (
,
úú( )
schema
ùù 
:
ùù 
$str
ùù $
,
ùù$ %
table
ûû 
:
ûû 
$str
ûû 
,
ûû 
column
üü 
:
üü 
$str
üü !
)
üü! "
;
üü" #
migrationBuilder
°° 
.
°° 
CreateIndex
°° (
(
°°( )
name
¢¢ 
:
¢¢ 
$str
¢¢ -
,
¢¢- .
schema
££ 
:
££ 
$str
££ $
,
££$ %
table
§§ 
:
§§ 
$str
§§ 
,
§§ 
column
•• 
:
•• 
$str
•• &
)
••& '
;
••' (
migrationBuilder
ßß 
.
ßß 
CreateIndex
ßß (
(
ßß( )
name
®® 
:
®® 
$str
®® J
,
®®J K
table
©© 
:
©© 
$str
©© 4
,
©©4 5
column
™™ 
:
™™ 
$str
™™ -
)
™™- .
;
™™. /
migrationBuilder
¨¨ 
.
¨¨ 
CreateIndex
¨¨ (
(
¨¨( )
name
≠≠ 
:
≠≠ 
$str
≠≠ 0
,
≠≠0 1
schema
ÆÆ 
:
ÆÆ 
$str
ÆÆ 
,
ÆÆ  
table
ØØ 
:
ØØ 
$str
ØØ &
,
ØØ& '
column
∞∞ 
:
∞∞ 
$str
∞∞ !
,
∞∞! "
unique
±± 
:
±± 
true
±± 
)
±± 
;
±± 
migrationBuilder
≥≥ 
.
≥≥ 
CreateIndex
≥≥ (
(
≥≥( )
name
¥¥ 
:
¥¥ 
$str
¥¥ '
,
¥¥' (
schema
µµ 
:
µµ 
$str
µµ 
,
µµ 
table
∂∂ 
:
∂∂ 
$str
∂∂ 
,
∂∂ 
column
∑∑ 
:
∑∑ 
$str
∑∑ !
,
∑∑! "
unique
∏∏ 
:
∏∏ 
true
∏∏ 
,
∏∏ 
filter
ππ 
:
ππ 
$str
ππ /
)
ππ/ 0
;
ππ0 1
migrationBuilder
ªª 
.
ªª 
CreateIndex
ªª (
(
ªª( )
name
ºº 
:
ºº 
$str
ºº #
,
ºº# $
schema
ΩΩ 
:
ΩΩ 
$str
ΩΩ 
,
ΩΩ 
table
ææ 
:
ææ 
$str
ææ 
,
ææ 
column
øø 
:
øø 
$str
øø 
,
øø 
unique
¿¿ 
:
¿¿ 
true
¿¿ 
)
¿¿ 
;
¿¿ 
migrationBuilder
¬¬ 
.
¬¬ 
CreateIndex
¬¬ (
(
¬¬( )
name
√√ 
:
√√ 
$str
√√ 9
,
√√9 :
schema
ƒƒ 
:
ƒƒ 
$str
ƒƒ "
,
ƒƒ" #
table
≈≈ 
:
≈≈ 
$str
≈≈ -
,
≈≈- .
column
∆∆ 
:
∆∆ 
$str
∆∆ #
)
∆∆# $
;
∆∆$ %
migrationBuilder
»» 
.
»» 
CreateIndex
»» (
(
»»( )
name
…… 
:
…… 
$str
…… *
,
……* +
schema
   
:
   
$str
   "
,
  " #
table
ÀÀ 
:
ÀÀ 
$str
ÀÀ !
,
ÀÀ! "
column
ÃÃ 
:
ÃÃ 
$str
ÃÃ  
,
ÃÃ  !
unique
ÕÕ 
:
ÕÕ 
true
ÕÕ 
)
ÕÕ 
;
ÕÕ 
migrationBuilder
œœ 
.
œœ 
CreateIndex
œœ (
(
œœ( )
name
–– 
:
–– 
$str
–– @
,
––@ A
schema
—— 
:
—— 
$str
—— %
,
——% &
table
““ 
:
““ 
$str
““ '
,
““' (
column
”” 
:
”” 
$str
”” 0
,
””0 1
unique
‘‘ 
:
‘‘ 
true
‘‘ 
)
‘‘ 
;
‘‘ 
migrationBuilder
÷÷ 
.
÷÷ 
CreateIndex
÷÷ (
(
÷÷( )
name
◊◊ 
:
◊◊ 
$str
◊◊ 6
,
◊◊6 7
schema
ÿÿ 
:
ÿÿ 
$str
ÿÿ %
,
ÿÿ% &
table
ŸŸ 
:
ŸŸ 
$str
ŸŸ '
,
ŸŸ' (
column
⁄⁄ 
:
⁄⁄ 
$str
⁄⁄ &
)
⁄⁄& '
;
⁄⁄' (
migrationBuilder
‹‹ 
.
‹‹ 
CreateIndex
‹‹ (
(
‹‹( )
name
›› 
:
›› 
$str
›› 3
,
››3 4
schema
ﬁﬁ 
:
ﬁﬁ 
$str
ﬁﬁ $
,
ﬁﬁ$ %
table
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ (
,
ﬂﬂ( )
column
‡‡ 
:
‡‡ 
$str
‡‡ "
)
‡‡" #
;
‡‡# $
migrationBuilder
‚‚ 
.
‚‚ 
CreateIndex
‚‚ (
(
‚‚( )
name
„„ 
:
„„ 
$str
„„ /
,
„„/ 0
schema
‰‰ 
:
‰‰ 
$str
‰‰ $
,
‰‰$ %
table
ÂÂ 
:
ÂÂ 
$str
ÂÂ &
,
ÂÂ& '
column
ÊÊ 
:
ÊÊ 
$str
ÊÊ  
)
ÊÊ  !
;
ÊÊ! "
migrationBuilder
ËË 
.
ËË 
CreateIndex
ËË (
(
ËË( )
name
ÈÈ 
:
ÈÈ 
$str
ÈÈ 9
,
ÈÈ9 :
schema
ÍÍ 
:
ÍÍ 
$str
ÍÍ !
,
ÍÍ! "
table
ÎÎ 
:
ÎÎ 
$str
ÎÎ /
,
ÎÎ/ 0
column
ÏÏ 
:
ÏÏ 
$str
ÏÏ !
)
ÏÏ! "
;
ÏÏ" #
migrationBuilder
ÓÓ 
.
ÓÓ 
CreateIndex
ÓÓ (
(
ÓÓ( )
name
ÔÔ 
:
ÔÔ 
$str
ÔÔ <
,
ÔÔ< =
schema
 
:
 
$str
 $
,
$ %
table
ÒÒ 
:
ÒÒ 
$str
ÒÒ '
,
ÒÒ' (
columns
ÚÚ 
:
ÚÚ 
new
ÚÚ 
[
ÚÚ 
]
ÚÚ 
{
ÚÚ  
$str
ÚÚ! (
,
ÚÚ( )
$str
ÚÚ* 8
}
ÚÚ9 :
)
ÚÚ: ;
;
ÚÚ; <
migrationBuilder
ÙÙ 
.
ÙÙ 
CreateIndex
ÙÙ (
(
ÙÙ( )
name
ıı 
:
ıı 
$str
ıı 6
,
ıı6 7
schema
ˆˆ 
:
ˆˆ 
$str
ˆˆ $
,
ˆˆ$ %
table
˜˜ 
:
˜˜ 
$str
˜˜ '
,
˜˜' (
column
¯¯ 
:
¯¯ 
$str
¯¯ &
)
¯¯& '
;
¯¯' (
migrationBuilder
˙˙ 
.
˙˙ 
CreateIndex
˙˙ (
(
˙˙( )
name
˚˚ 
:
˚˚ 
$str
˚˚ 8
,
˚˚8 9
schema
¸¸ 
:
¸¸ 
$str
¸¸ $
,
¸¸$ %
table
˝˝ 
:
˝˝ 
$str
˝˝ )
,
˝˝) *
column
˛˛ 
:
˛˛ 
$str
˛˛ &
)
˛˛& '
;
˛˛' (
migrationBuilder
ÄÄ 
.
ÄÄ 
CreateIndex
ÄÄ (
(
ÄÄ( )
name
ÅÅ 
:
ÅÅ 
$str
ÅÅ ;
,
ÅÅ; <
schema
ÇÇ 
:
ÇÇ 
$str
ÇÇ $
,
ÇÇ$ %
table
ÉÉ 
:
ÉÉ 
$str
ÉÉ ,
,
ÉÉ, -
column
ÑÑ 
:
ÑÑ 
$str
ÑÑ &
)
ÑÑ& '
;
ÑÑ' (
migrationBuilder
ÜÜ 
.
ÜÜ 
CreateIndex
ÜÜ (
(
ÜÜ( )
name
áá 
:
áá 
$str
áá I
,
ááI J
schema
àà 
:
àà 
$str
àà !
,
àà! "
table
ââ 
:
ââ 
$str
ââ :
,
ââ: ;
column
ää 
:
ää 
$str
ää &
)
ää& '
;
ää' (
migrationBuilder
åå 
.
åå 
CreateIndex
åå (
(
åå( )
name
çç 
:
çç 
$str
çç 5
,
çç5 6
schema
éé 
:
éé 
$str
éé %
,
éé% &
table
èè 
:
èè 
$str
èè -
,
èè- .
column
êê 
:
êê 
$str
êê 
)
êê  
;
êê  !
migrationBuilder
íí 
.
íí 
CreateIndex
íí (
(
íí( )
name
ìì 
:
ìì 
$str
ìì 7
,
ìì7 8
schema
îî 
:
îî 
$str
îî $
,
îî$ %
table
ïï 
:
ïï 
$str
ïï +
,
ïï+ ,
column
ññ 
:
ññ 
$str
ññ #
)
ññ# $
;
ññ$ %
migrationBuilder
òò 
.
òò 
CreateIndex
òò (
(
òò( )
name
ôô 
:
ôô 
$str
ôô .
,
ôô. /
schema
öö 
:
öö 
$str
öö $
,
öö$ %
table
õõ 
:
õõ 
$str
õõ $
,
õõ$ %
column
úú 
:
úú 
$str
úú !
,
úú! "
unique
ùù 
:
ùù 
true
ùù 
,
ùù 
filter
ûû 
:
ûû 
$str
ûû /
)
ûû/ 0
;
ûû0 1
migrationBuilder
†† 
.
†† 
CreateIndex
†† (
(
††( )
name
°° 
:
°° 
$str
°° ,
,
°°, -
schema
¢¢ 
:
¢¢ 
$str
¢¢ $
,
¢¢$ %
table
££ 
:
££ 
$str
££ $
,
££$ %
column
§§ 
:
§§ 
$str
§§ 
,
§§  
unique
•• 
:
•• 
true
•• 
)
•• 
;
•• 
migrationBuilder
ßß 
.
ßß 
CreateIndex
ßß (
(
ßß( )
name
®® 
:
®® 
$str
®® 9
,
®®9 :
schema
©© 
:
©© 
$str
©© $
,
©©$ %
table
™™ 
:
™™ 
$str
™™ $
,
™™$ %
column
´´ 
:
´´ 
$str
´´ ,
,
´´, -
unique
¨¨ 
:
¨¨ 
true
¨¨ 
)
¨¨ 
;
¨¨ 
migrationBuilder
ÆÆ 
.
ÆÆ 
CreateIndex
ÆÆ (
(
ÆÆ( )
name
ØØ 
:
ØØ 
$str
ØØ 1
,
ØØ1 2
schema
∞∞ 
:
∞∞ 
$str
∞∞ %
,
∞∞% &
table
±± 
:
±± 
$str
±± "
,
±±" #
column
≤≤ 
:
≤≤ 
$str
≤≤ &
)
≤≤& '
;
≤≤' (
migrationBuilder
¥¥ 
.
¥¥ 
CreateIndex
¥¥ (
(
¥¥( )
name
µµ 
:
µµ 
$str
µµ 9
,
µµ9 :
schema
∂∂ 
:
∂∂ 
$str
∂∂ 
,
∂∂ 
table
∑∑ 
:
∑∑ 
$str
∑∑ *
,
∑∑* +
column
∏∏ 
:
∏∏ 
$str
∏∏ &
)
∏∏& '
;
∏∏' (
migrationBuilder
∫∫ 
.
∫∫ 
CreateIndex
∫∫ (
(
∫∫( )
name
ªª 
:
ªª 
$str
ªª <
,
ªª< =
schema
ºº 
:
ºº 
$str
ºº 
,
ºº 
table
ΩΩ 
:
ΩΩ 
$str
ΩΩ .
,
ΩΩ. /
column
ææ 
:
ææ 
$str
ææ %
)
ææ% &
;
ææ& '
migrationBuilder
¿¿ 
.
¿¿ 
CreateIndex
¿¿ (
(
¿¿( )
name
¡¡ 
:
¡¡ 
$str
¡¡ /
,
¡¡/ 0
schema
¬¬ 
:
¬¬ 
$str
¬¬ 
,
¬¬ 
table
√√ 
:
√√ 
$str
√√ %
,
√√% &
column
ƒƒ 
:
ƒƒ 
$str
ƒƒ !
,
ƒƒ! "
unique
≈≈ 
:
≈≈ 
true
≈≈ 
)
≈≈ 
;
≈≈ 
migrationBuilder
«« 
.
«« 
CreateIndex
«« (
(
««( )
name
»» 
:
»» 
$str
»» -
,
»»- .
schema
…… 
:
…… 
$str
…… $
,
……$ %
table
   
:
   
$str
   
,
   
column
ÀÀ 
:
ÀÀ 
$str
ÀÀ &
,
ÀÀ& '
unique
ÃÃ 
:
ÃÃ 
true
ÃÃ 
)
ÃÃ 
;
ÃÃ 
migrationBuilder
ŒŒ 
.
ŒŒ 
CreateIndex
ŒŒ (
(
ŒŒ( )
name
œœ 
:
œœ 
$str
œœ 6
,
œœ6 7
schema
–– 
:
–– 
$str
–– "
,
––" #
table
—— 
:
—— 
$str
—— '
,
——' (
column
““ 
:
““ 
$str
““ &
)
““& '
;
““' (
migrationBuilder
‘‘ 
.
‘‘ 
CreateIndex
‘‘ (
(
‘‘( )
name
’’ 
:
’’ 
$str
’’ 9
,
’’9 :
schema
÷÷ 
:
÷÷ 
$str
÷÷ &
,
÷÷& '
table
◊◊ 
:
◊◊ 
$str
◊◊ *
,
◊◊* +
column
ÿÿ 
:
ÿÿ 
$str
ÿÿ &
,
ÿÿ& '
unique
ŸŸ 
:
ŸŸ 
true
ŸŸ 
)
ŸŸ 
;
ŸŸ 
migrationBuilder
€€ 
.
€€ 
CreateIndex
€€ (
(
€€( )
name
‹‹ 
:
‹‹ 
$str
‹‹ .
,
‹‹. /
schema
›› 
:
›› 
$str
›› 
,
››  
table
ﬁﬁ 
:
ﬁﬁ 
$str
ﬁﬁ 
,
ﬁﬁ  
column
ﬂﬂ 
:
ﬂﬂ 
$str
ﬂﬂ &
)
ﬂﬂ& '
;
ﬂﬂ' (
}
‡‡ 	
	protected
‚‚ 
override
‚‚ 
void
‚‚ 
Down
‚‚  $
(
‚‚$ %
MigrationBuilder
‚‚% 5
migrationBuilder
‚‚6 F
)
‚‚F G
{
„„ 	
migrationBuilder
‰‰ 
.
‰‰ 
	DropTable
‰‰ &
(
‰‰& '
name
ÂÂ 
:
ÂÂ 
$str
ÂÂ 
,
ÂÂ 
schema
ÊÊ 
:
ÊÊ 
$str
ÊÊ $
)
ÊÊ$ %
;
ÊÊ% &
migrationBuilder
ËË 
.
ËË 
	DropTable
ËË &
(
ËË& '
name
ÈÈ 
:
ÈÈ 
$str
ÈÈ 3
)
ÈÈ3 4
;
ÈÈ4 5
migrationBuilder
ÎÎ 
.
ÎÎ 
	DropTable
ÎÎ &
(
ÎÎ& '
name
ÏÏ 
:
ÏÏ 
$str
ÏÏ %
,
ÏÏ% &
schema
ÌÌ 
:
ÌÌ 
$str
ÌÌ 
)
ÌÌ  
;
ÌÌ  !
migrationBuilder
ÔÔ 
.
ÔÔ 
	DropTable
ÔÔ &
(
ÔÔ& '
name
 
:
 
$str
 
,
 
schema
ÒÒ 
:
ÒÒ 
$str
ÒÒ 
)
ÒÒ 
;
ÒÒ  
migrationBuilder
ÛÛ 
.
ÛÛ 
	DropTable
ÛÛ &
(
ÛÛ& '
name
ÙÙ 
:
ÙÙ 
$str
ÙÙ ,
,
ÙÙ, -
schema
ıı 
:
ıı 
$str
ıı "
)
ıı" #
;
ıı# $
migrationBuilder
˜˜ 
.
˜˜ 
	DropTable
˜˜ &
(
˜˜& '
name
¯¯ 
:
¯¯ 
$str
¯¯ &
,
¯¯& '
schema
˘˘ 
:
˘˘ 
$str
˘˘ %
)
˘˘% &
;
˘˘& '
migrationBuilder
˚˚ 
.
˚˚ 
	DropTable
˚˚ &
(
˚˚& '
name
¸¸ 
:
¸¸ 
$str
¸¸ '
,
¸¸' (
schema
˝˝ 
:
˝˝ 
$str
˝˝ $
)
˝˝$ %
;
˝˝% &
migrationBuilder
ˇˇ 
.
ˇˇ 
	DropTable
ˇˇ &
(
ˇˇ& '
name
Ä	Ä	 
:
Ä	Ä	 
$str
Ä	Ä	 %
,
Ä	Ä	% &
schema
Å	Å	 
:
Å	Å	 
$str
Å	Å	 $
)
Å	Å	$ %
;
Å	Å	% &
migrationBuilder
É	É	 
.
É	É	 
	DropTable
É	É	 &
(
É	É	& '
name
Ñ	Ñ	 
:
Ñ	Ñ	 
$str
Ñ	Ñ	 !
,
Ñ	Ñ	! "
schema
Ö	Ö	 
:
Ö	Ö	 
$str
Ö	Ö	 "
)
Ö	Ö	" #
;
Ö	Ö	# $
migrationBuilder
á	á	 
.
á	á	 
	DropTable
á	á	 &
(
á	á	& '
name
à	à	 
:
à	à	 
$str
à	à	 &
,
à	à	& '
schema
â	â	 
:
â	â	 
$str
â	â	 $
)
â	â	$ %
;
â	â	% &
migrationBuilder
ã	ã	 
.
ã	ã	 
	DropTable
ã	ã	 &
(
ã	ã	& '
name
å	å	 
:
å	å	 
$str
å	å	 (
,
å	å	( )
schema
ç	ç	 
:
ç	ç	 
$str
ç	ç	 $
)
ç	ç	$ %
;
ç	ç	% &
migrationBuilder
è	è	 
.
è	è	 
	DropTable
è	è	 &
(
è	è	& '
name
ê	ê	 
:
ê	ê	 
$str
ê	ê	 +
,
ê	ê	+ ,
schema
ë	ë	 
:
ë	ë	 
$str
ë	ë	 $
)
ë	ë	$ %
;
ë	ë	% &
migrationBuilder
ì	ì	 
.
ì	ì	 
	DropTable
ì	ì	 &
(
ì	ì	& '
name
î	î	 
:
î	î	 
$str
î	î	 9
,
î	î	9 :
schema
ï	ï	 
:
ï	ï	 
$str
ï	ï	 !
)
ï	ï	! "
;
ï	ï	" #
migrationBuilder
ó	ó	 
.
ó	ó	 
	DropTable
ó	ó	 &
(
ó	ó	& '
name
ò	ò	 
:
ò	ò	 
$str
ò	ò	 ,
,
ò	ò	, -
schema
ô	ô	 
:
ô	ô	 
$str
ô	ô	 %
)
ô	ô	% &
;
ô	ô	& '
migrationBuilder
õ	õ	 
.
õ	õ	 
	DropTable
õ	õ	 &
(
õ	õ	& '
name
ú	ú	 
:
ú	ú	 
$str
ú	ú	 *
,
ú	ú	* +
schema
ù	ù	 
:
ù	ù	 
$str
ù	ù	 $
)
ù	ù	$ %
;
ù	ù	% &
migrationBuilder
ü	ü	 
.
ü	ü	 
	DropTable
ü	ü	 &
(
ü	ü	& '
name
†	†	 
:
†	†	 
$str
†	†	 !
,
†	†	! "
schema
°	°	 
:
°	°	 
$str
°	°	 %
)
°	°	% &
;
°	°	& '
migrationBuilder
£	£	 
.
£	£	 
	DropTable
£	£	 &
(
£	£	& '
name
§	§	 
:
§	§	 
$str
§	§	 )
,
§	§	) *
schema
•	•	 
:
•	•	 
$str
•	•	 
)
•	•	 
;
•	•	  
migrationBuilder
ß	ß	 
.
ß	ß	 
	DropTable
ß	ß	 &
(
ß	ß	& '
name
®	®	 
:
®	®	 
$str
®	®	 -
,
®	®	- .
schema
©	©	 
:
©	©	 
$str
©	©	 
)
©	©	 
;
©	©	  
migrationBuilder
´	´	 
.
´	´	 
	DropTable
´	´	 &
(
´	´	& '
name
¨	¨	 
:
¨	¨	 
$str
¨	¨	 
,
¨	¨	 
schema
≠	≠	 
:
≠	≠	 
$str
≠	≠	 $
)
≠	≠	$ %
;
≠	≠	% &
migrationBuilder
Ø	Ø	 
.
Ø	Ø	 
	DropTable
Ø	Ø	 &
(
Ø	Ø	& '
name
∞	∞	 
:
∞	∞	 
$str
∞	∞	 )
,
∞	∞	) *
schema
±	±	 
:
±	±	 
$str
±	±	 &
)
±	±	& '
;
±	±	' (
migrationBuilder
≥	≥	 
.
≥	≥	 
	DropTable
≥	≥	 &
(
≥	≥	& '
name
¥	¥	 
:
¥	¥	 
$str
¥	¥	 
,
¥	¥	 
schema
µ	µ	 
:
µ	µ	 
$str
µ	µ	 
)
µ	µ	  
;
µ	µ	  !
migrationBuilder
∑	∑	 
.
∑	∑	 
	DropTable
∑	∑	 &
(
∑	∑	& '
name
∏	∏	 
:
∏	∏	 
$str
∏	∏	 
,
∏	∏	 
schema
π	π	 
:
π	π	 
$str
π	π	 
)
π	π	  
;
π	π	  !
migrationBuilder
ª	ª	 
.
ª	ª	 
	DropTable
ª	ª	 &
(
ª	ª	& '
name
º	º	 
:
º	º	 
$str
º	º	 +
,
º	º	+ ,
schema
Ω	Ω	 
:
Ω	Ω	 
$str
Ω	Ω	 "
)
Ω	Ω	" #
;
Ω	Ω	# $
migrationBuilder
ø	ø	 
.
ø	ø	 
	DropTable
ø	ø	 &
(
ø	ø	& '
name
¿	¿	 
:
¿	¿	 
$str
¿	¿	 &
,
¿	¿	& '
schema
¡	¡	 
:
¡	¡	 
$str
¡	¡	 "
)
¡	¡	" #
;
¡	¡	# $
migrationBuilder
√	√	 
.
√	√	 
	DropTable
√	√	 &
(
√	√	& '
name
ƒ	ƒ	 
:
ƒ	ƒ	 
$str
ƒ	ƒ	 #
,
ƒ	ƒ	# $
schema
≈	≈	 
:
≈	≈	 
$str
≈	≈	 %
)
≈	≈	% &
;
≈	≈	& '
migrationBuilder
«	«	 
.
«	«	 
	DropTable
«	«	 &
(
«	«	& '
name
»	»	 
:
»	»	 
$str
»	»	 
,
»	»	 
schema
…	…	 
:
…	…	 
$str
…	…	 $
)
…	…	$ %
;
…	…	% &
migrationBuilder
À	À	 
.
À	À	 
	DropTable
À	À	 &
(
À	À	& '
name
Ã	Ã	 
:
Ã	Ã	 
$str
Ã	Ã	 
,
Ã	Ã	 
schema
Õ	Õ	 
:
Õ	Õ	 
$str
Õ	Õ	 
)
Õ	Õ	  
;
Õ	Õ	  !
migrationBuilder
œ	œ	 
.
œ	œ	 
	DropTable
œ	œ	 &
(
œ	œ	& '
name
–	–	 
:
–	–	 
$str
–	–	  
,
–	–	  !
schema
—	—	 
:
—	—	 
$str
—	—	 "
)
—	—	" #
;
—	—	# $
migrationBuilder
”	”	 
.
”	”	 
	DropTable
”	”	 &
(
”	”	& '
name
‘	‘	 
:
‘	‘	 
$str
‘	‘	 .
,
‘	‘	. /
schema
’	’	 
:
’	’	 
$str
’	’	 !
)
’	’	! "
;
’	’	" #
migrationBuilder
◊	◊	 
.
◊	◊	 
	DropTable
◊	◊	 &
(
◊	◊	& '
name
ÿ	ÿ	 
:
ÿ	ÿ	 
$str
ÿ	ÿ	 
,
ÿ	ÿ	 
schema
Ÿ	Ÿ	 
:
Ÿ	Ÿ	 
$str
Ÿ	Ÿ	 %
)
Ÿ	Ÿ	% &
;
Ÿ	Ÿ	& '
migrationBuilder
€	€	 
.
€	€	 
	DropTable
€	€	 &
(
€	€	& '
name
‹	‹	 
:
‹	‹	 
$str
‹	‹	 !
,
‹	‹	! "
schema
›	›	 
:
›	›	 
$str
›	›	 
)
›	›	 
;
›	›	  
migrationBuilder
ﬂ	ﬂ	 
.
ﬂ	ﬂ	 
	DropTable
ﬂ	ﬂ	 &
(
ﬂ	ﬂ	& '
name
‡	‡	 
:
‡	‡	 
$str
‡	‡	 $
,
‡	‡	$ %
schema
·	·	 
:
·	·	 
$str
·	·	 
)
·	·	 
;
·	·	  
migrationBuilder
„	„	 
.
„	„	 
	DropTable
„	„	 &
(
„	„	& '
name
‰	‰	 
:
‰	‰	 
$str
‰	‰	 #
,
‰	‰	# $
schema
Â	Â	 
:
Â	Â	 
$str
Â	Â	 $
)
Â	Â	$ %
;
Â	Â	% &
migrationBuilder
Á	Á	 
.
Á	Á	 
	DropTable
Á	Á	 &
(
Á	Á	& '
name
Ë	Ë	 
:
Ë	Ë	 
$str
Ë	Ë	  
,
Ë	Ë	  !
schema
È	È	 
:
È	È	 
$str
È	È	 "
)
È	È	" #
;
È	È	# $
migrationBuilder
Î	Î	 
.
Î	Î	 
	DropTable
Î	Î	 &
(
Î	Î	& '
name
Ï	Ï	 
:
Ï	Ï	 
$str
Ï	Ï	 
,
Ï	Ï	 
schema
Ì	Ì	 
:
Ì	Ì	 
$str
Ì	Ì	 
)
Ì	Ì	  
;
Ì	Ì	  !
migrationBuilder
Ô	Ô	 
.
Ô	Ô	 
	DropTable
Ô	Ô	 &
(
Ô	Ô	& '
name
		 
:
		 
$str
		 
,
		 
schema
Ò	Ò	 
:
Ò	Ò	 
$str
Ò	Ò	 
)
Ò	Ò	  
;
Ò	Ò	  !
}
Ú	Ú	 	
}
Û	Û	 
}Ù	Ù	 Ë
~D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Enums\StreetcodeTypeDiscriminators.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Enums 
{ 
public 

static 
class (
StreetcodeTypeDiscriminators 4
{ 
public 
static 
string 
StreetcodeBaseType /
{0 1
get2 5
=>6 8
$str9 J
;J K
}L M
public 
static 
string  
StreetcodePersonType 1
{2 3
get4 7
=>8 :
$str; N
;N O
}P Q
public 
static 
string 
StreetcodeEventType 0
{1 2
get3 6
=>7 9
$str: L
;L M
}N O
public 
static 
string 
DiscriminatorName .
{/ 0
get1 4
=>5 7
$str8 H
;H I
}J K
public

 
static

 
string

 
GetStreetcodeType

 .
(

. /
StreetcodeType

/ =
streetcodeType

> L
)

L M
{ 	
switch 
( 
streetcodeType "
)" #
{ 
case 
StreetcodeType #
.# $
Event$ )
:) *
return+ 1
StreetcodeEventType2 E
;E F
case 
StreetcodeType #
.# $
Person$ *
:* +
return, 2 
StreetcodePersonType3 G
;G H
default 
: 
return 
StreetcodeBaseType  2
;2 3
} 
} 	
} 
} †
pD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Enums\StreetcodeType.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Enums 
{ 
public 
enum	 
StreetcodeType 
{ 
Event 	
,	 

Person 

,
 
} 
} ´
rD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Enums\StreetcodeStatus.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Enums 
; 
public 
enum 
StreetcodeStatus 
{ 
Draft 	
,	 

	Published 
, 
Deleted 
} „
jD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Enums\LogoType.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Enums 
; 
public 
enum 
LogoType 
: 
byte 
{ 
Twitter 
, 
	Instagram 
, 
Facebook 
, 
YouTube 
}		 ’
pD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Enums\ImageAssigment.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Enums 
{ 
public		 

enum		 
ImageAssigment		 
{

 
	Animation 
, 
Blackandwhite 
, 
Relatedfigure 
, 
} 
} Ê
qD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Enums\DateViewPattern.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Enums 
{ 
public		 

enum		 
DateViewPattern		 
{

 
DateMonthYear 
, 
	MonthYear 
, 

SeasonYear 
, 
Year 
} 
} °
oD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Users\User.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Users" '
{ 
[ 
Table 

(
 
$str 
, 
Schema 
= 
$str $
)$ %
]% &
public 

class 
User 
{		 
[

 	
Key

	 
]

 
[ 	
DatabaseGenerated	 
( #
DatabaseGeneratedOption 2
.2 3
Identity3 ;
); <
]< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Surname 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
] 
[ 	
EmailAddress	 
] 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Login 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Required	 
] 
public 
UserRole 
Role 
{ 
get "
;" #
set$ '
;' (
}) *
} 
} Ï
ÅD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Transactions\TransactionLink.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Transactions" .
;. /
[ 
Table 
( 
$str 
, 
Schema "
=# $
$str% 3
)3 4
]4 5
public 
class 
TransactionLink 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
UrlTitle 
{ 
get !
;! "
set# &
;& '
}( )
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Url 
{ 
get 
; 
set !
;! "
}# $
[ 
Required 
] 
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ì
uD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Toponyms\Toponym.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Toponyms" *
;* +
[ 
Table 
( 
$str 
, 
Schema 
= 
$str &
)& '
]' (
public		 
class		 
Toponym		 
{

 
[ 
Key 
] 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
Oblast 
{ 
get 
; 
set  #
;# $
}% &
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
AdminRegionOld !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
AdminRegionNew !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Gromada 
{ 
get  
;  !
set" %
;% &
}' (
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
	Community 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
Required 
] 
[   
	MaxLength   
(   
$num   
)   
]   
public!! 

string!! 

StreetName!! 
{!! 
get!! "
;!!" #
set!!$ '
;!!' (
}!!) *
[## 
	MaxLength## 
(## 
$num## 
)## 
]## 
public$$ 

string$$ 
?$$ 

StreetType$$ 
{$$ 
get$$  #
;$$# $
set$$% (
;$$( )
}$$* +
public&& 

List&& 
<&& 
StreetcodeContent&& !
>&&! "
Streetcodes&&# .
{&&/ 0
get&&1 4
;&&4 5
set&&6 9
;&&9 :
}&&; <
=&&= >
new&&? B
(&&C D
)&&D E
;&&E F
public(( 

ToponymCoordinate(( 

Coordinate(( '
{((( )
get((* -
;((- .
set((/ 2
;((2 3
}((4 5
})) ò	
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Toponyms\StreetcodeToponym.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Toponyms" *
{ 
public 

class 
StreetcodeToponym "
{ 
[ 	
Required	 
] 
public		 
int		 
StreetcodeId		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
[ 	
Required	 
] 
public 
int 
	ToponymId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
StreetcodeContent  
?  !

Streetcode" ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
Toponym 
? 
Toponym 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} £
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Timeline\TimelineItem.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Timeline" *
;* +
[ 
Table 
( 
$str 
, 
Schema 
=  !
$str" ,
), -
]- .
public		 
class		 
TimelineItem		 
{

 
[ 
Key 
] 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
DataType 
( 
DataType 
. 
Date 
) 
] 
public 

DateTime 
Date 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
] 
public 

DateViewPattern 
DateViewPattern *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public!! 

List!! 
<!! %
HistoricalContextTimeline!! )
>!!) *&
HistoricalContextTimelines!!+ E
{!!F G
get!!H K
;!!K L
set!!M P
;!!P Q
}!!R S
=!!T U
new!!V Y
(!!Z [
)!![ \
;!!\ ]
}"" æ	
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Timeline\HistoricalContextTimeline.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Timeline" *
{ 
public 

class %
HistoricalContextTimeline *
{ 
[ 	
Required	 
] 
public 
int 
HistoricalContextId &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
[

 	
Required

	 
]

 
public 
int 

TimelineId 
{ 
get  #
;# $
set% (
;( )
}* +
public 
HistoricalContext  
?  !
HistoricalContext" 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
public 
TimelineItem 
? 
Timeline %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} é
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Timeline\HistoricalContext.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Timeline" *
;* +
[ 
Table 
( 
$str 
, 
Schema $
=% &
$str' 1
)1 2
]2 3
public 
class 
HistoricalContext 
{ 
[		 
Key		 
]		 	
[

 
DatabaseGenerated

 
(

 #
DatabaseGeneratedOption

 .
.

. /
Identity

/ 7
)

7 8
]

8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
public 

List 
< %
HistoricalContextTimeline )
>) *&
HistoricalContextTimelines+ E
{F G
getH K
;K L
setM P
;P Q
}R S
=T U
newV Y
(Y Z
)Z [
;[ \
} ≤	
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Team\TeamMemberPositions.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Team" &
{		 
[

 
Table

 

(


 
$str

 "
,

" #
Schema

$ *
=

+ ,
$str

- 3
)

3 4
]

4 5
public 

class 
TeamMemberPositions $
{ 
public 
int 
TeamMemberId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
	Positions 
	Positions "
{# $
get% (
;( )
set* -
;- .
}/ 0
public 

TeamMember 

TeamMember $
{% &
get' *
;* +
set, /
;/ 0
}1 2
public 
int 
PositionsId 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} ≠
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Team\TeamMemberLink.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Team" &
{ 
[ 
Table 

(
 
$str 
, 
Schema  &
=' (
$str) /
)/ 0
]0 1
public 

class 
TeamMemberLink 
{		 
[

 	
Key

	 
]

 
[ 	
DatabaseGenerated	 
( #
DatabaseGeneratedOption 2
.2 3
Identity3 ;
); <
]< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[ 	
Required	 
] 
public 
LogoType 
LogoType  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
	TargetUrl  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
Required	 
] 
public 
int 
TeamMemberId 
{  !
get" %
;% &
set' *
;* +
}, -
public 

TeamMember 
? 

TeamMember %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} ∞
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Team\TeamMember.cs
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
DAL

 
.

 
Entities

 !
.

! "
Team

" &
{ 
[ 
Table 

(
 
$str 
, 
Schema !
=" #
$str$ *
)* +
]+ ,
public 

class 

TeamMember 
{ 
[ 	
Key	 
] 
[ 	
DatabaseGenerated	 
( #
DatabaseGeneratedOption 2
.2 3
Identity3 ;
); <
]< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
	FirstName  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
LastName 
{  !
get" %
;% &
set' *
;* +
}, -
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Description "
{# $
get% (
;( )
set* -
;- .
}/ 0
[ 	
Required	 
] 
public 
bool 
IsMain 
{ 
get  
;  !
set" %
;% &
}' (
public!! 
List!! 
<!! 
TeamMemberLink!! "
>!!" #
?!!# $
TeamMemberLinks!!% 4
{!!5 6
get!!7 :
;!!: ;
set!!< ?
;!!? @
}!!A B
public## 
List## 
<## 
	Positions## 
>## 
?## 
	Positions##  )
{##* +
get##, /
;##/ 0
set##1 4
;##4 5
}##6 7
[%% 	
Required%%	 
]%% 
public&& 
int&& 
ImageId&& 
{&& 
get&&  
;&&  !
set&&" %
;&&% &
}&&' (
public(( 
Image(( 
?(( 
Image(( 
{(( 
get(( !
;((! "
set((# &
;((& '
}((( )
})) 
}** ∞
sD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Team\Positions.cs
	namespace		 	

Streetcode		
 
.		 
DAL		 
.		 
Entities		 !
.		! "
Team		" &
{

 
[ 
Table 

(
 
$str 
, 
Schema 
=  
$str! '
)' (
]( )
public 

class 
	Positions 
{ 
[ 	
Key	 
] 
[ 	
DatabaseGenerated	 
( #
DatabaseGeneratedOption 2
.2 3
Identity3 ;
); <
]< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Position 
{  !
get" %
;% &
set' *
;* +
}, -
public 
List 
< 

TeamMember 
> 
?  
TeamMembers! ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
} 
} Ì	
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\Types\PersonStreetCode.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
., -
Types- 2
;2 3
public 
class 
PersonStreetcode 
: 
StreetcodeContent  1
{ 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
	FirstName 
{ 
get !
;! "
set# &
;& '
}( )
[

 
	MaxLength

 
(

 
$num

 
)

 
]

 
public 

string 
? 
Rank 
{ 
get 
; 
set "
;" #
}$ %
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
LastName 
{ 
get  
;  !
set" %
;% &
}' (
} ‹
ÖD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\Types\EventStreetCode.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
., -
Types- 2
;2 3
public 
class 
EventStreetcode 
: 
StreetcodeContent 0
{ 
} ’
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\TextContent\Text.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
., -
TextContent- 8
;8 9
[ 
Table 
( 
$str 
, 
Schema 
= 
$str %
)% &
]& '
public 
class 
Text 
{ 
[		 
Key		 
]		 	
[

 
DatabaseGenerated

 
(

 #
DatabaseGeneratedOption

 .
.

. /
Identity

/ 7
)

7 8
]

8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
TextContent 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
AdditionalText !
{" #
get$ '
;' (
set) ,
;, -
}. /
[ 
Required 
] 
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ÿ
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\TextContent\Term.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
., -
TextContent- 8
;8 9
[ 
Table 
( 
$str 
, 
Schema 
= 
$str %
)% &
]& '
public 
class 
Term 
{ 
[		 
Key		 
]		 	
[

 
DatabaseGenerated

 
(

 #
DatabaseGeneratedOption

 .
.

. /
Identity

/ 7
)

7 8
]

8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

List 
< 
RelatedTerm 
> 
RelatedTerms )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
=8 9
new: =
(= >
)> ?
;? @
} ˇ
áD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\TextContent\RelatedTerm.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
., -
TextContent- 8
{ 
[ 
Table 

(
 
$str 
, 
Schema "
=# $
$str% 1
)1 2
]2 3
public 

class 
RelatedTerm 
{ 
[		 	
Key			 
]		 
[

 	
DatabaseGenerated

	 
(

 #
DatabaseGeneratedOption

 2
.

2 3
Identity

3 ;
)

; <
]

< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Word 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
public 
int 
TermId 
{ 
get 
;  
set! $
;$ %
}& '
public 
Term 
? 
Term 
{ 
get 
;  
set! $
;$ %
}& '
} 
} Ÿ
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\TextContent\Fact.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
., -
TextContent- 8
;8 9
[ 
Table 
( 
$str 
, 
Schema 
= 
$str %
)% &
]& '
public 
class 
Fact 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
FactContent 
{  
get! $
;$ %
set& )
;) *
}+ ,
public 

int 
? 
ImageId 
{ 
get 
; 
set "
;" #
}$ %
public 

Image 
? 
Image 
{ 
get 
; 
set "
;" #
}$ %
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ¯I
ÅD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\StreetcodeContent.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
;, -
[ 
Table 
( 
$str 
, 
Schema 
= 
$str +
)+ ,
], -
[ 
Index 
( 
nameof 
( 
TransliterationUrl  
)  !
,! "
IsUnique# +
=, -
true. 2
)2 3
]3 4
[ 
Index 
( 
nameof 
( 
Index 
) 
, 
IsUnique 
=  
true! %
)% &
]& '
public 
class 
StreetcodeContent 
{ 
[ 
Key 
] 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
public 

int 
Index 
{ 
get 
; 
set 
;  
}! "
[ 
	MaxLength 
( 
$num 
) 
] 
public   

string   
?   
Teaser   
{   
get   
;    
set  ! $
;  $ %
}  & '
["" 
Required"" 
]"" 
[## 
	MaxLength## 
(## 
$num## 
)## 
]## 
public$$ 

string$$ 
?$$ 

DateString$$ 
{$$ 
get$$  #
;$$# $
set$$% (
;$$( )
}$$* +
[&& 
	MaxLength&& 
(&& 
$num&& 
)&& 
]&& 
public'' 

string'' 
?'' 
Alias'' 
{'' 
get'' 
;'' 
set''  #
;''# $
}''% &
public)) 

StreetcodeStatus)) 
Status)) "
{))# $
get))% (
;))( )
set))* -
;))- .
}))/ 0
[++ 
Required++ 
]++ 
[,, 
	MaxLength,, 
(,, 
$num,, 
),, 
],, 
public-- 

string-- 
?-- 
Title-- 
{-- 
get-- 
;-- 
set--  #
;--# $
}--% &
[.. 
Required.. 
].. 
[// 
	MaxLength// 
(// 
$num// 
)// 
]// 
public00 

string00 
?00 
TransliterationUrl00 %
{00& '
get00( +
;00+ ,
set00- 0
;000 1
}002 3
public22 

int22 
	ViewCount22 
{22 
get22 
;22 
set22  #
;22# $
}22% &
public44 

DateTime44 
	CreatedAt44 
{44 
get44  #
;44# $
set44% (
;44( )
}44* +
public66 

DateTime66 
	UpdatedAt66 
{66 
get66  #
;66# $
set66% (
;66( )
}66* +
[88 
Required88 
]88 
public99 

DateTime99 '
EventStartOrPersonBirthDate99 /
{990 1
get992 5
;995 6
set997 :
;99: ;
}99< =
public;; 

DateTime;; 
?;; %
EventEndOrPersonDeathDate;; .
{;;/ 0
get;;1 4
;;;4 5
set;;6 9
;;;9 :
};;; <
public== 

int== 
?== 
AudioId== 
{== 
get== 
;== 
set== "
;==" #
}==$ %
public?? 

Text?? 
??? 
Text?? 
{?? 
get?? 
;?? 
set??  
;??  !
}??" #
publicAA 

AudioAA 
?AA 
AudioAA 
{AA 
getAA 
;AA 
setAA "
;AA" #
}AA$ %
publicCC 

ListCC 
<CC 
StatisticRecordCC 
>CC  
StatisticRecordsCC! 1
{CC2 3
getCC4 7
;CC7 8
setCC9 <
;CC< =
}CC> ?
=CC@ A
newCCB E
(CCE F
)CCF G
;CCG H
publicEE 

ListEE 
<EE  
StreetcodeCoordinateEE $
>EE$ %
CoordinatesEE& 1
{EE2 3
getEE4 7
;EE7 8
setEE9 <
;EE< =
}EE> ?
=EE@ A
newEEB E
(EEE F
)EEF G
;EEG H
publicGG 

TransactionLinkGG 
?GG 
TransactionLinkGG +
{GG, -
getGG. 1
;GG1 2
setGG3 6
;GG6 7
}GG8 9
publicII 

ListII 
<II 
ToponymII 
>II 
ToponymsII !
{II" #
getII$ '
;II' (
setII) ,
;II, -
}II. /
=II0 1
newII2 5
(II6 7
)II7 8
;II8 9
publicKK 

ListKK 
<KK 
ImageKK 
>KK 
ImagesKK 
{KK 
getKK  #
;KK# $
setKK% (
;KK( )
}KK* +
=KK, -
newKK. 1
(KK2 3
)KK3 4
;KK4 5
publicMM 

ListMM 
<MM 
StreetcodeTagIndexMM "
>MM" # 
StreetcodeTagIndicesMM$ 8
{MM9 :
getMM; >
;MM> ?
setMM@ C
;MMC D
}MME F
=MMG H
newMMI L
(MMM N
)MMN O
;MMO P
publicOO 

ListOO 
<OO 
TagOO 
>OO 
TagsOO 
{OO 
getOO 
;OO  
setOO! $
;OO$ %
}OO& '
=OO( )
newOO* -
(OO- .
)OO. /
;OO/ 0
publicQQ 

ListQQ 
<QQ 
SubtitleQQ 
>QQ 
	SubtitlesQQ #
{QQ$ %
getQQ& )
;QQ) *
setQQ+ .
;QQ. /
}QQ0 1
=QQ2 3
newQQ4 7
(QQ8 9
)QQ9 :
;QQ: ;
publicSS 

ListSS 
<SS 
FactSS 
>SS 
FactsSS 
{SS 
getSS !
;SS! "
setSS# &
;SS& '
}SS( )
=SS* +
newSS, /
(SS0 1
)SS1 2
;SS2 3
publicUU 

ListUU 
<UU 
VideoUU 
>UU 
VideosUU 
{UU 
getUU  #
;UU# $
setUU% (
;UU( )
}UU* +
=UU, -
newUU. 1
(UU2 3
)UU3 4
;UU4 5
publicWW 

ListWW 
<WW 
SourceLinkCategoryWW "
>WW" # 
SourceLinkCategoriesWW$ 8
{WW9 :
getWW; >
;WW> ?
setWW@ C
;WWC D
}WWE F
=WWG H
newWWI L
(WWM N
)WWN O
;WWO P
publicYY 

ListYY 
<YY 
TimelineItemYY 
>YY 
TimelineItemsYY +
{YY, -
getYY. 1
;YY1 2
setYY3 6
;YY6 7
}YY8 9
=YY: ;
newYY< ?
(YY@ A
)YYA B
;YYB C
public[[ 

List[[ 
<[[ 
RelatedFigure[[ 
>[[ 
	Observers[[ (
{[[) *
get[[+ .
;[[. /
set[[0 3
;[[3 4
}[[5 6
=[[7 8
new[[9 <
([[= >
)[[> ?
;[[? @
public]] 

List]] 
<]] 
RelatedFigure]] 
>]] 
Targets]] &
{]]' (
get]]) ,
;]], -
set]]. 1
;]]1 2
}]]3 4
=]]5 6
new]]7 :
(]]; <
)]]< =
;]]= >
public__ 

List__ 
<__ 
Partner__ 
>__ 
Partners__ !
{__" #
get__$ '
;__' (
set__) ,
;__, -
}__. /
=__0 1
new__2 5
(__6 7
)__7 8
;__8 9
publicaa 

Listaa 
<aa 
StreetcodeArtaa 
>aa 
StreetcodeArtsaa -
{aa. /
getaa0 3
;aa3 4
setaa5 8
;aa8 9
}aa: ;
=aa< =
newaa> A
(aaB C
)aaC D
;aaD E
publiccc 

Listcc 
<cc %
StreetcodeCategoryContentcc )
>cc) *&
StreetcodeCategoryContentscc+ E
{ccF G
getccH K
;ccK L
setccM P
;ccP Q
}ccR S
=ccT U
newccV Y
(ccY Z
)ccZ [
;cc[ \
}dd ∞
}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\StreetcodeArt.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
;, -
[ 
Table 
( 
$str 
, 
Schema 
=  !
$str" .
). /
]/ 0
public 
class 
StreetcodeArt 
{		 
public

 

int

 
Index

 
{

 
get

 
;

 
set

 
;

  
}

! "
[ 
Required 
] 
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
[ 
Required 
] 
public 

int 
ArtId 
{ 
get 
; 
set 
;  
}! "
public 

Art 
? 
Art 
{ 
get 
; 
set 
; 
}  !
} å

}D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Streetcode\RelatedFigure.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "

Streetcode" ,
;, -
[ 
Table 
( 
$str 
, 
Schema  
=! "
$str# /
)/ 0
]0 1
public 
class 
RelatedFigure 
{ 
[		 
Required		 
]		 
public

 

int

 

ObserverId

 
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
public 

StreetcodeContent 
Observer %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
[ 
Required 
] 
public 

int 
TargetId 
{ 
get 
; 
set "
;" #
}$ %
public 

StreetcodeContent 
Target #
{$ %
get& )
;) *
set+ .
;. /
}0 1
} ö
ÜD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Sources\StreetcodeCategoryContent.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Sources" )
;) *
[ 
Table 
( 
$str #
,# $
Schema% +
=, -
$str. 7
)7 8
]8 9
public 
class %
StreetcodeCategoryContent &
{		 
[

 
Required

 
]

 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Text 
{ 
get 
; 
set "
;" #
}$ %
[ 
Required 
] 
public 

int  
SourceLinkCategoryId #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 
Required 
] 
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 

SourceLinkCategory 
? 
SourceLinkCategory 1
{2 3
get4 7
;7 8
set9 <
;< =
}> ?
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ®
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Sources\SourceLinkCategory.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Sources" )
;) *
[		 
Table		 
(		 
$str		 
,		  
Schema		! '
=		( )
$str		* 3
)		3 4
]		4 5
public

 
class

 
SourceLinkCategory

 
{ 
[ 
Key 
] 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
] 
public 

int 
ImageId 
{ 
get 
; 
set !
;! "
}# $
public 

Image 
? 
Image 
{ 
get 
; 
set "
;" #
}$ %
public 

List 
< 
StreetcodeContent !
>! "
Streetcodes# .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
== >
new? B
(C D
)D E
;E F
public 

List 
< %
StreetcodeCategoryContent )
>) *&
StreetcodeCategoryContents+ E
{F G
getH K
;K L
setM P
;P Q
}R S
=T U
newV Y
(Z [
)[ \
;\ ]
} ê
yD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Payment\SaveCardData.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Payment" )
{ 
public 

class 
SaveCardData 
{ 
[ 	
JsonProperty	 
( 
$str  
)  !
]! "
public 
bool 
SaveCard 
{ 
get "
;" #
set$ '
;' (
}) *
[

 	
JsonProperty

	 
(

 
$str

  
)

  !
]

! "
public 
string 
WalletId 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} †
ÄD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Payment\MerchantPaymentInfo.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Payment" )
{ 
public 

class 
MerchantPaymentInfo $
{ 
[

 	
JsonProperty

	 
(

 
$str

 #
)

# $
]

$ %
public 
string 
Destination !
{" #
get$ '
;' (
set) ,
;, -
}. /
} 
} Î	
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Payment\InvoiceInfo.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Payment" )
{ 
public 

class 
InvoiceInfo 
{ 
[ 	
JsonConstructor	 
] 
public 
InvoiceInfo 
( 
string !
	invoiceId" +
,+ ,
string- 3
pageUrl4 ;
); <
{		 	
	InvoiceId

 
=

 
	invoiceId

 !
;

! "
PageUrl 
= 
pageUrl 
; 
} 	
[ 	
JsonProperty	 
( 
$str !
)! "
]" #
public 
string 
	InvoiceId 
{  !
get" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
)  
]  !
public 
string 
PageUrl 
{ 
get  #
;# $
}% &
} 
} º
tD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Payment\Invoice.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Payment" )
{ 
public 

class 
Invoice 
{ 
public 
Invoice 
( 
long 
amount "
," #
int$ '
?' (
ccy) ,
,, -
MerchantPaymentInfo. A
merchantPaymentInfoB U
,U V
stringW ]
redirectUrl^ i
)i j
{	 

Amount		 
=		 
amount		 
;		 
Ccy

 
=

 
ccy

 
;

 
MerchantPaymentInfo 
=  !
merchantPaymentInfo" 5
;5 6
RedirectUrl 
= 
redirectUrl %
;% &
} 	
[ 	
JsonProperty	 
( 
$str 
) 
]  
public 
long 
Amount 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
int 
? 
Ccy 
{ 
get 
; 
set "
;" #
}$ %
[ 	
JsonProperty	 
( 
$str (
)( )
]) *
public 
MerchantPaymentInfo "
MerchantPaymentInfo# 6
{7 8
get9 <
;< =
set> A
;A B
}C D
[ 	
JsonProperty	 
( 
$str #
)# $
]$ %
public 
string 
RedirectUrl !
{" #
get$ '
;' (
set) ,
;, -
}. /
})) 
}** ∂
xD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Payment\BasketOrder.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Payment" )
{ 
public 

class 
BasketOrder 
{ 
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Name 
{ 
get  
;  !
set" %
;% &
}' (
[

 	
JsonProperty

	 
(

 
$str

 
)

 
]

 
public 
int 
Qty 
{ 
get 
; 
set !
;! "
}# $
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
long 
Sum 
{ 
get 
; 
set "
;" #
}$ %
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Icon 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Unit 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
) 
] 
public 
string 
Code 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str 
)  
]  !
public 
string 
Barcode 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
JsonProperty	 
( 
$str 
) 
]  
public 
string 
Header 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
JsonProperty	 
( 
$str 
) 
]  
public   
string   
Footer   
{   
get   "
;  " #
set  $ '
;  ' (
}  ) *
["" 	
JsonProperty""	 
("" 
$str"" 
)"" 
]"" 
public## 
List## 
<## 
int## 
>## 
Tax## 
{## 
get## "
;##" #
set##$ '
;##' (
}##) *
[%% 	
JsonProperty%%	 
(%% 
$str%% 
)%% 
]%%  
public&& 
string&& 
Uktzed&& 
{&& 
get&& "
;&&" #
set&&$ '
;&&' (
}&&) *
}'' 
}(( ò	
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Partners\StreetcodePartner.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Partners" *
{ 
public 

class 
StreetcodePartner "
{ 
[ 	
Required	 
] 
public		 
int		 
StreetcodeId		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
[ 	
Required	 
] 
public 
int 
	PartnerId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
StreetcodeContent  
?  !

Streetcode" ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
Partner 
? 
Partner 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} ü
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Partners\PartnerSourceLink.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Partners" *
;* +
[ 
Table 
( 
$str 
, 
Schema %
=& '
$str( 2
)2 3
]3 4
public 
class 
PartnerSourceLink 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
public 

LogoType 
LogoType 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
	TargetUrl 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
Required 
] 
public 

int 
	PartnerId 
{ 
get 
; 
set  #
;# $
}% &
public 

Partner 
? 
Partner 
{ 
get !
;! "
set# &
;& '
}( )
} “
uD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Partners\Partner.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Partners" *
;* +
[ 
Table 
( 
$str 
, 
Schema 
= 
$str &
)& '
]' (
public		 
class		 
Partner		 
{

 
[ 
Key 
] 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
[ 
Required 
] 
public 

int 
LogoId 
{ 
get 
; 
set  
;  !
}" #
[ 
Required 
] 
public 

bool 
IsKeyPartner 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
Required 
] 
public 

bool 
IsVisibleEverywhere #
{$ %
get& )
;) *
set+ .
;. /
}0 1
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
	TargetUrl 
{ 
get "
;" #
set$ '
;' (
}) *
[ 
	MaxLength 
( 
$num 
) 
] 
public   

string   
?   
UrlTitle   
{   
get   !
;  ! "
set  # &
;  & '
}  ( )
[!! 
	MaxLength!! 
(!! 
$num!! 
)!! 
]!! 
public"" 

string"" 
?"" 
Description"" 
{""  
get""! $
;""$ %
set""& )
;"") *
}""+ ,
public$$ 

Image$$ 
?$$ 
Logo$$ 
{$$ 
get$$ 
;$$ 
set$$ !
;$$! "
}$$# $
public&& 

List&& 
<&& 
PartnerSourceLink&& !
>&&! "
PartnerSourceLinks&&# 5
{&&6 7
get&&8 ;
;&&; <
set&&= @
;&&@ A
}&&B C
=&&D E
new&&F I
(&&J K
)&&K L
;&&L M
public(( 

List(( 
<(( 
StreetcodeContent(( !
>((! "
Streetcodes((# .
{((/ 0
get((1 4
;((4 5
set((6 9
;((9 :
}((; <
=((= >
new((? B
(((C D
)((D E
;((E F
})) ™
nD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\News\News.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
News" &
{ 
[ 
Table 

(
 
$str 
, 
Schema 
= 
$str "
)" #
]# $
[		 
Index		 

(		
 
nameof		 
(		 
URL		 
)		 
,		 
IsUnique		  
=		! "
true		# '
)		' (
]		( )
public

 

class

 
News

 
{ 
[ 	
Key	 
] 
[ 	
DatabaseGenerated	 
( #
DatabaseGeneratedOption 2
.2 3
Identity3 ;
); <
]< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Title 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
public 
string 
Text 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
] 
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
URL 
{ 
get 
;  
set! $
;$ %
}& '
public 
int 
? 
ImageId 
{ 
get !
;! "
set# &
;& '
}( )
public 
Image 
? 
Image 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
] 
public 
DateTime 
CreationDate $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} è
pD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Media\Video.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Media" '
;' (
[ 
Table 
( 
$str 
, 
Schema 
= 
$str !
)! "
]" #
public 
class 
Video 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 
Required 
] 
public 

string 
? 
Url 
{ 
get 
; 
set !
;! "
}# $
[ 
Required 
] 
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ≥	
ÅD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Media\Images\StreetcodeImage.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Media" '
.' (
Images( .
{ 
public 

class 
StreetcodeImage  
{ 
[ 	
Required	 
] 
public		 
int		 
StreetcodeId		 
{		  !
get		" %
;		% &
set		' *
;		* +
}		, -
[ 	
Required	 
] 
public 
int 
ImageId 
{ 
get  
;  !
set" %
;% &
}' (
public 
Image 
? 
Image 
{ 
get !
;! "
set# &
;& '
}( )
public 
StreetcodeContent  
?  !

Streetcode" ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
} 
} «
~D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Media\Images\ImageDetails.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Media" '
.' (
Images( .
{ 
[ 
Table 

(
 
$str 
, 
Schema "
=# $
$str% ,
), -
]- .
public 

class 
ImageDetails 
{ 
[		 	
Key			 
]		 
[

 	
DatabaseGenerated

	 
(

 #
DatabaseGeneratedOption

 2
.

2 3
Identity

3 ;
)

; <
]

< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Title 
{ 
get "
;" #
set$ '
;' (
}) *
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
? 
Alt 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
Required	 
] 
public 
int 
ImageId 
{ 
get  
;  !
set" %
;% &
}' (
public 
Image 
? 
Image 
{ 
get !
;! "
set# &
;& '
}( )
} 
} Á
wD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Media\Images\Image.cs
	namespace		 	

Streetcode		
 
.		 
DAL		 
.		 
Entities		 !
.		! "
Media		" '
.		' (
Images		( .
;		. /
[ 
Table 
( 
$str 
, 
Schema 
= 
$str !
)! "
]" #
public 
class 
Image 
{ 
[ 
Key 
] 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
	NotMapped 
] 
public 

string 
? 
Base64 
{ 
get 
;  
set! $
;$ %
}& '
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
BlobName 
{ 
get !
;! "
set# &
;& '
}( )
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
MimeType 
{ 
get !
;! "
set# &
;& '
}( )
public 

ImageDetails 
? 
ImageDetails %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 

List 
< 
StreetcodeContent !
>! "
Streetcodes# .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
== >
new? B
(C D
)D E
;E F
public!! 

List!! 
<!! 
Fact!! 
>!! 
Facts!! 
{!! 
get!! !
;!!! "
set!!# &
;!!& '
}!!( )
=!!* +
new!!, /
(!!0 1
)!!1 2
;!!2 3
public## 

Art## 
?## 
Art## 
{## 
get## 
;## 
set## 
;## 
}##  !
public%% 

Partner%% 
?%% 
Partner%% 
{%% 
get%% !
;%%! "
set%%# &
;%%& '
}%%( )
public'' 

List'' 
<'' 
SourceLinkCategory'' "
>''" # 
SourceLinkCategories''$ 8
{''9 :
get''; >
;''> ?
set''@ C
;''C D
}''E F
=''G H
new''I L
(''M N
)''N O
;''O P
public)) 

News)) 
.)) 
News)) 
?)) 
News)) 
{)) 
get))  
;))  !
set))" %
;))% &
}))' (
public** 


TeamMember** 
?** 

TeamMember** !
{**" #
get**$ '
;**' (
set**) ,
;**, -
}**. /
}++ ú
uD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Media\Images\Art.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Media" '
.' (
Images( .
;. /
[ 
Table 
( 
$str 
, 
Schema 
= 
$str 
)  
]  !
public 
class 
Art 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
public 

int 
ImageId 
{ 
get 
; 
set !
;! "
}# $
public 

Image 
? 
Image 
{ 
get 
; 
set "
;" #
}$ %
public 

List 
< 
StreetcodeArt 
> 
StreetcodeArts -
{. /
get0 3
;3 4
set5 8
;8 9
}: ;
=< =
new> A
(B C
)C D
;D E
} ú
pD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Media\Audio.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Media" '
;' (
[ 
Table 
( 
$str 
, 
Schema 
= 
$str !
)! "
]" #
public 
class 
Audio 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Title 
{ 
get 
; 
set  #
;# $
}% &
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
BlobName 
{ 
get !
;! "
set# &
;& '
}( )
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
MimeType 
{ 
get !
;! "
set# &
;& '
}( )
[ 
	NotMapped 
] 
public 

string 
? 
Base64 
{ 
get 
;  
set! $
;$ %
}& '
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
} ˜
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Instagram\InstagramPostResponse.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
	Instagram" +
{ 
public		 

class		 !
InstagramPostResponse		 &
{

 
public 
IEnumerable 
< 
InstagramPost (
>( )
Data* .
{/ 0
get1 4
;4 5
set6 9
;9 :
}; <
} 
} ≈
|D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Instagram\InstagramPost.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
	Instagram" +
{ 
public 

class 
InstagramPost 
{ 
[

 	
JsonPropertyName

	 
(

 
$str

 #
)

# $
]

$ %
public 
string 
Caption 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
JsonPropertyName	 
( 
$str 
) 
]  
public 
string 
Id 
{ 
get 
; 
set  #
;# $
}% &
[ 	
JsonPropertyName	 
( 
$str &
)& '
]' (
public 
string 
	MediaType 
{  !
get" %
;% &
set' *
;* +
}, -
[ 	
JsonPropertyName	 
( 
$str %
)% &
]& '
public 
string 
MediaUrl 
{  
get! $
;$ %
set& )
;) *
}+ ,
["" 	
JsonPropertyName""	 
("" 
$str"" %
)""% &
]""& '
public## 
string## 
	Permalink## 
{##  !
get##" %
;##% &
set##' *
;##* +
}##, -
[(( 	
JsonPropertyName((	 
((( 
$str(( )
)(() *
]((* +
public)) 
string)) 
ThumbnailUrl)) "
{))# $
get))% (
;))( )
set))* -
;))- .
}))/ 0
[++ 	
JsonPropertyName++	 
(++ 
$str++ %
)++% &
]++& '
public,, 
bool,, 
IsPinned,, 
{,, 
get,, "
;,," #
set,,$ '
;,,' (
},,) *
}-- 
}.. Ü
vD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Feedback\Response.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
Feedback" *
;* +
[ 
Table 
( 
$str 
, 
Schema 
= 
$str '
)' (
]( )
public 
class 
Response 
{ 
[		 
Key		 
]		 	
[

 
DatabaseGenerated

 
(

 #
DatabaseGeneratedOption

 .
.

. /
Identity

/ 7
)

7 8
]

8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Name 
{ 
get 
; 
set "
;" #
}$ %
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
[ 
EmailAddress 
] 
public 

string 
Email 
{ 
get 
; 
set "
;" #
}$ %
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
Description 
{  
get! $
;$ %
set& )
;) *
}+ ,
} ˝
~D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\Analytics\StatisticRecord.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
	Analytics" +
{ 
[ 
Table 

(
 
$str 
, 
Schema #
=$ %
$str& 3
)3 4
]4 5
public		 

class		 
StatisticRecord		  
{

 
[ 	
Key	 
] 
[ 	
DatabaseGenerated	 
( #
DatabaseGeneratedOption 2
.2 3
Identity3 ;
); <
]< =
public 
int 
Id 
{ 
get 
; 
set  
;  !
}" #
public 
int 
QrId 
{ 
get 
; 
set "
;" #
}$ %
public 
int 
Count 
{ 
get 
; 
set  #
;# $
}% &
[ 	
	MaxLength	 
( 
$num 
) 
] 
public 
string 
Address 
{ 
get  #
;# $
set% (
;( )
}* +
public 
int 
StreetcodeId 
{  !
get" %
;% &
set' *
;* +
}, -
public 
StreetcodeContent  
?  !

Streetcode" ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
int "
StreetcodeCoordinateId )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public  
StreetcodeCoordinate # 
StreetcodeCoordinate$ 8
{9 :
get; >
;> ?
set@ C
;C D
}E F
} 
} É
zD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\Tag.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
;3 4
[ 
Table 
( 
$str 
, 
Schema 
= 
$str %
)% &
]& '
public 
class 
Tag 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
Title 
{ 
get 
; 
set "
;" #
}$ %
public 

IEnumerable 
< 
StreetcodeTagIndex )
>) * 
StreetcodeTagIndices+ ?
{@ A
getB E
;E F
setG J
;J K
}L M
public 

IEnumerable 
< 
StreetcodeContent (
>( )
Streetcodes* 5
{6 7
get8 ;
;; <
set= @
;@ A
}B C
} ”
D:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\Subtitle.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
;3 4
[ 
Table 
( 
$str 
, 
Schema 
= 
$str *
)* +
]+ ,
public 
class 
Subtitle 
{		 
[

 
Key

 
]

 	
[ 
DatabaseGenerated 
( #
DatabaseGeneratedOption .
.. /
Identity/ 7
)7 8
]8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
	MaxLength 
( 
$num 
) 
] 
public 

string 
? 
SubtitleText 
{  !
get" %
;% &
set' *
;* +
}, -
[ 
Required 
] 
public 

int 
StreetcodeId 
{ 
get !
;! "
set# &
;& '
}( )
public 


Streetcode 
. 
StreetcodeContent '
?' (

Streetcode) 3
{4 5
get6 9
;9 :
set; >
;> ?
}@ A
} Ö
âD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\StreetcodeTagIndex.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
{ 
[ 
Table 

(
 
$str !
,! "
Schema# )
=* +
$str, 9
)9 :
]: ;
public 

class 
StreetcodeTagIndex #
{		 
[

 	
Required

	 
]

 
public 
int 
StreetcodeId 
{  !
get" %
;% &
set' *
;* +
}, -
[ 	
Required	 
] 
public 
int 
TagId 
{ 
get 
; 
set  #
;# $
}% &
[ 	
Required	 
] 
public 
bool 
	IsVisible 
{ 
get  #
;# $
set% (
;( )
}* +
[ 	
Required	 
] 
[ 	
Range	 
( 
$num 
, 
int 
. 
MaxValue 
) 
]  
public 
int 
Index 
{ 
get 
; 
set  #
;# $
}% &
public 
StreetcodeContent  
?  !

Streetcode" ,
{- .
get/ 2
;2 3
set4 7
;7 8
}9 :
public 
Tag 
? 
Tag 
{ 
get 
; 
set "
;" #
}$ %
} 
} ™
ÑD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\Email\Message.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
.3 4
Email4 9
{ 
public 

class 
Message 
{ 
public 
Message 
( 
IEnumerable "
<" #
string# )
>) *
to+ -
,- .
string/ 5
from6 :
,: ;
string< B
subjectC J
,J K
stringL R
contentS Z
)Z [
{ 	
To		 
=		 
new		 
List		 
<		 
MailboxAddress		 (
>		( )
(		) *
)		* +
;		+ ,
To 
. 
AddRange 
( 
to 
. 
Select !
(! "
x" #
=>$ &
new' *
MailboxAddress+ 9
(9 :
string: @
.@ A
EmptyA F
,F G
xH I
)I J
)J K
)K L
;L M
From 
= 
from 
; 
Content 
= 
content 
; 
Subject 
= 
subject 
; 
} 	
public 
List 
< 
MailboxAddress "
>" #
To$ &
{' (
get) ,
;, -
set. 1
;1 2
}3 4
public 
string 
From 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 
Subject 
{ 
get  #
;# $
set% (
;( )
}* +
public 
string 
Content 
{ 
get  #
;# $
set% (
;( )
}* +
} 
} ÷	
èD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\Email\EmailConfiguration.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
.3 4
Email4 9
{ 
public 

class 
EmailConfiguration #
{ 
public 
string 
From 
{ 
get  
;  !
set" %
;% &
}' (
public 
string 

SmtpServer  
{! "
get# &
;& '
set( +
;+ ,
}- .
public 
int 
Port 
{ 
get 
; 
set "
;" #
}$ %
public 
string 
UserName 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
string		 
Password		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
}

 
} î
öD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\Coordinates\Types\ToponymCoordinate.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
.3 4
Coordinates4 ?
.? @
Types@ E
;E F
public 
class 
ToponymCoordinate 
:  

Coordinate! +
{ 
[ 
Required 
] 
public		 

int		 
	ToponymId		 
{		 
get		 
;		 
set		  #
;		# $
}		% &
public 

Toponym 
? 
Toponym 
{ 
get !
;! "
set# &
;& '
}( )
} ÷
ùD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\Coordinates\Types\StreetcodeCoordinate.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
.3 4
Coordinates4 ?
.? @
Types@ E
;E F
public 
class  
StreetcodeCoordinate !
:" #

Coordinate$ .
{ 
[		 
Required		 
]		 
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
( )
public 

StreetcodeContent 
? 

Streetcode (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public 

StatisticRecord 
StatisticRecord *
{+ ,
get- 0
;0 1
set2 5
;5 6
}7 8
} ˙
çD:\shribak\SoftServe\netProjcetBasedLearning\Streetcode-Server\Streetcode\Streetcode.DAL\Entities\AdditionalContent\Coordinates\Coordinate.cs
	namespace 	

Streetcode
 
. 
DAL 
. 
Entities !
.! "
AdditionalContent" 3
.3 4
Coordinates4 ?
;? @
[ 
Table 
( 
$str 
, 
Schema 
= 
$str ,
), -
]- .
public 
class 

Coordinate 
{ 
[		 
Key		 
]		 	
[

 
DatabaseGenerated

 
(

 #
DatabaseGeneratedOption

 .
.

. /
Identity

/ 7
)

7 8
]

8 9
public 

int 
Id 
{ 
get 
; 
set 
; 
} 
[ 
Required 
] 
[ 
Column 
( 
TypeName 
= 
$str &
)& '
]' (
public 

decimal 
Latitude 
{ 
get !
;! "
set# &
;& '
}( )
[ 
Required 
] 
[ 
Column 
( 
TypeName 
= 
$str &
)& '
]' (
public 

decimal 

Longtitude 
{ 
get  #
;# $
set% (
;( )
}* +
} 