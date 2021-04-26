# AI4CE-Research


· 开发了用于测试RL算法物理推理的新型虚拟环境，主要由一个Unity2D物理环境中
的堆积塔楼游戏构成
· 熟悉并学习ML / RL工具包，例如Unity ML-Agents，Stable-Baseline，
Tensorflow，Tensorboard
· 使用此环境基准测试了现代RL算法，例如DQN，PPO和TRPO。 分析并比较了他们
的物理推理和总结能力

· Experience with ML/RL toolkits, such as Unity ML-Agents, Stable-Baseline,
Tensorflow, Tensorboard 
· Developed a novel virtual environment for testing physical reasoning of
RL Algorithm that contains a tower building game in a 2D physical
environment 
· Benchmarked modern RL algorithms such as DQN, PPO and TRPO.
Analyzed and compared their physical reasoning and generalization
ability 
· Currently writing a paper for potential ICRA/ICLR publication

INTRODUCTION

Recent application of the reinforcement learning algorithmin  various  scientific  domains  has  shown  the  great  poten-tial  of  reinforcement  learning.  Recent  works  include,  forexample,  real-world  autonomous  driving  using  deep  rein-forcement  learning[Simulation-Based  Reinforcement  Learn-ing  for  Real-World  Autonomous  Driving],  as  well  as  manysimulation-based   projects.   And   among   these   simulation-based projects, many of them concentrate on making agentsplay  games  such  as  the  classic  arcade  games  from  Atari2600 games[ reference Atari paper], the famous video gameStarCraft  II  [1],  or  the  ancient  board  game  Go  [2].  Theseexperiment  has  proven  the  effectiveness  of  reinforcementlearning in solving many complexed tasks.However,   many   of   the   tasks   does   not   confront   withphysics-based  challenges.  In  this  physical  world,  many  po-tential real-world reinforcement learning tasks are influencedby physical properties such as gravity, force, inertia. Henceit  is  important  for  RL  algorithms  to  have  physical  rea-soning  and  generalization  ability.  We  expect  the  state-of-the-art  algorithms  will  struggle  to  acquire  these  abilities.There  are  previous  works  that  presented  a  series  of  staticphysical puzzles to test the physical reasoning of the learningalgorithm[source  Phyre].  In  our  living  world,  the  physicaltasks  faced  by  humans  are  often  continous  problems,  andwe  need  to  constantly  interact  with  the  surrounding  envi-ronment and continuously observe the states of the physicalenvironment to make the next step. Motivated by this goal,we developed a dynamic physics-based benchmark TowerAI.TowerAI  provides  three  different  settings  of  tower  buildinggames in a simulated 2D world.(See figure 123) Each gamehas the same basic goal which is to stack as many blocks aspossible on the platform, and same game-over conditions –either  reaching  the  time  limit  of  each  step  or  one  or  moreblocks fall off from the platform. An agent playing this gameneeds to understand how to stack blocks in the most stableway to stack more blocks.The  first  goal  the  TowerAI  designed  to  satisfy  is:  Focuson  physical  generalization.  The  settings  1  and  2’s  optimalsolution  is  not  hard  for  a  human  to  discover(see  figure  foroptimal  solution),  we  can  easily  generalize  the  underlyingstrategy.  However,  for  AI  each  frame,  even  that  there  are∗indicate equal contributions.1NewYorkUniversity,Brooklyn,NY11201,USAzl1836@nyu.edu2The University of British Columbia, Vancouver, BC V6T 1Z4, Canadabilljizh@student.ubc.caonly  small  differences  in  pixels,  is  a  novel  challenge.  Weshould expect the AI to generalize the optimal strategy basedon  the  shape  of  the  block  and  physical  attribute  to  solvethe  previously  unseen  states  and  not  purely  based  on  pastexperiences.The  second  goal  is  to  focus  on  physical  reasoning:  Thegame  is  built  only  using  primitive  shapes.  The  game  useunity’s  Box2D  physic  engine  to  simulate  2D  physic  and  ispurely deterministic, and involves collision, gravity, friction,and angular drag.
