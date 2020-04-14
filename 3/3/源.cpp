#include<stdio.h>
#include<stdlib.h>
#include<string.h>

#define MAX_SIZE 100

typedef struct ArcBox
{
	int tailvex, headvex;
	struct ArcBox* taillink, * headlink;
}Arc;

typedef struct vexnode
{
	char data;
	ArcBox* firstin, * firstout;
}VNode;

typedef struct graph
{
	VNode xlist[MAX_SIZE];
	int vexnum;
	int arcnum;
}Graph;

void Createmenu()
{
	system("color 02");
	printf("\t*————————————————————————————*\n");
	printf("\t|\t\t检查数据库死锁的等待图法\t\t |\n");
	printf("\t|\t\t                        \t\t |\n");
	printf("\t|\t\t\t\t173401010110   常宇阳    |\n");
	printf("\t|\t\t                        \t\t |\n");
	printf("\t*————————————————————————————*\n");
	printf("\n");
	printf("\t 1.新建事务等待图\n");
	printf("\n");
	printf("\t 2.新建等待关系\n");
	printf("\n");
	printf("\t 3.删除等待关系\n");
	printf("\n");
	printf("\t 4.检查死锁\n");
	printf("\n");
	printf("\t 5.查看当前等待图与保存\n");
	printf("\n");
	printf("\t 6.退出\n");
}

int LocateVex(Graph& g, char x)
{
	for (int i = 0; i < g.vexnum; i++)
	{
		if (g.xlist[i].data == x)
			return i;
	}
	return -1;
}

int CreateGraph(Graph& g)
{
	int i, j, k;
	char u, v;
	FILE* fp = fopen("C:\\Users\\28619\\Desktop\\home.txt", "r");
	fscanf(fp, "%d", &g.vexnum);
	printf("\t 结点数目为：%d\n", g.vexnum);
	printf("\n");
	fscanf(fp, " %d", &g.arcnum);
	printf("\t 边数目为：%d\n", g.arcnum);
	getchar();
	if (g.arcnum > g.vexnum * (g.vexnum - 1) || g.vexnum <= 0 || g.arcnum <= 0)
		return -1;
	//输入结点
	printf("\n");
	printf("\t 结点：\n");
	getchar();
	for (i = 0; i < g.vexnum; i++)
	{
		fscanf(fp, " %c", &g.xlist[i].data);
		printf("\t ");
		printf("%c  ", g.xlist[i].data);
		g.xlist[i].firstin = NULL;
		g.xlist[i].firstout = NULL;
	}
	getchar();
	//输入边结点
	printf("\n");
	printf("\t 边：\n");
	getchar();
	for (k = 0; k < g.arcnum; k++)
	{
		fscanf(fp, " %c->%c", &u, &v);
		printf("\t  %c->%c\n", u, v);
		i = LocateVex(g, u);
		j = LocateVex(g, v);
		Arc* p;
		p = (Arc*)malloc(sizeof(Arc));
		p->tailvex = i;
		p->headvex = j;
		p->taillink = g.xlist[i].firstout;
		p->headlink = g.xlist[j].firstin;
		g.xlist[i].firstout = p;
		g.xlist[j].firstin = p;
	}
	getchar();
	fclose(fp);
	return 1;
}

int InsertGraph(Graph& g)
{
	printf("\n\t 请输入待插入弧的弧尾结点和弧头结点：\n");
	char u, v;
	getchar();
	scanf("%c %c", &u, &v);
	getchar();
	int i, j;
	Arc* p;
	i = LocateVex(g, u);
	j = LocateVex(g, v);
	if (i == .1 || j == .1)
		return -1;
	else
	{
		p = (Arc*)malloc(sizeof(Arc));
		p->headvex = j;
		p->tailvex = i;
		p->headlink = g.xlist[j].firstin;
		p->taillink = g.xlist[i].firstout;
		g.xlist[i].firstout = p;
		g.xlist[j].firstin = p;
		g.arcnum++;
	}
	return 1;
}

int SearchArc(Graph& g, int m, int n)
{
	int i = 0, j;
	Arc* p;
	for (j = 0; j < g.arcnum; j++)
	{
		if (g.xlist[j].firstout == NULL)
			continue;
		else
		{
			for (p = g.xlist[j].firstout; p != NULL; p = p->taillink)
				if (p->tailvex == m && p->headvex == n)
				{
					return 1;
				}
		}

	}
	return -1;
}

int DeleteGraph(Graph& g)
{
	printf("\n\t 请输入待删除弧的弧尾结点和弧头结点：\n");
	char u, v;
	getchar();
	scanf("%c %c", &u, &v);
	getchar();
	int i, j, flag = 0;
	Arc* p, * q;
	i = LocateVex(g, u);
	j = LocateVex(g, v);
	if (i == .1 || j == .1 || SearchArc(g, i, j) != 1)
		return -1;
	//修改出弧链表
	if (g.xlist[i].firstout->headvex == j)
	{
		q = g.xlist[i].firstout;
		g.xlist[i].firstout = q->taillink;
		g.arcnum--;
	}
	else
	{
		for (p = g.xlist[i].firstout; p && (p->taillink != NULL)
			&& (p->taillink->headvex != j); p = p->taillink);
		if (p && p->taillink)
		{
			q = p->taillink;
			p->taillink = q->taillink;
			g.arcnum--;
		}
	}
	//修改入弧链表
	if (g.xlist[j].firstin->tailvex == i)
	{
		q = g.xlist[j].firstin;
		g.xlist[j].firstin = q->headlink;
		free(q);
	}
	else
	{
		for (p = g.xlist[j].firstin; p && (p->headlink != NULL)
			&& (p->headlink->tailvex != i); p = p->headlink);
		if (p && p->headlink)
		{
			q = p->headlink;
			p->headlink = q->headlink;
			free(q);
		}
	}
	return 1;
}

void TopologicalSort(Graph& g)
{
	printf("\n\t 拓扑序列为：");
	int i, j = 0, k;
	int count1 = 0, vex = 0;
	Arc* p, * q;
	int InDegree[MAX_SIZE];
	int Stack[MAX_SIZE];
	int* top, * low;
	top = low = &Stack[0];
	for (i = 0; i < g.vexnum; i++)
	{
		count1 = 0;
		p = g.xlist[i].firstin;
		while (p != NULL)
		{
			p = p->headlink;
			count1++;
		}
		InDegree[i] = count1;
		if (count1 == 0)
		{
			Stack[j++] = i;
			top++;
		}
	}
	int count2 = 0, flag = 0;
	while (low != top)
	{
		k = Stack[flag];
		//
		low++;
		printf("%c  ", g.xlist[k].data);
		count2++;
		for (q = g.xlist[k].firstout; q != NULL; q = q->taillink)
		{
			InDegree[q->headvex]--;
			if (InDegree[q->headvex] == 0)
			{
				Stack[j++] = q->headvex;
				top++;
			}
		}
		flag++;
	}
	printf("\n");
	if (count2 < g.vexnum)
	{
		system("color 04");
		printf("\n\t 存在死锁！\n");
	}
	else
		printf("\n\t 不存在死锁！\n");
}

void Store(Graph& g)
{
	int i, k = 0;
	Arc* p;
	FILE* fp = fopen("C:\\Users\\28619\\Desktop\\home.txt", "w");
	fprintf(fp, "%d %d ", g.vexnum, g.arcnum);
	printf("\n\t ");
	for (i = 0; i < g.vexnum; i++)
	{
		printf("%c  ", g.xlist[i].data);
		fprintf(fp, "%c ", g.xlist[i].data);
	}
	printf("\n");
	printf("\n\t ");
	for (i = 0; i < g.vexnum; i++)
	{
		k = 0;
		for (p = g.xlist[i].firstout; p != NULL; p = p->taillink)
		{
			printf("%c.>%c  ", g.xlist[i].data, g.xlist[p->headvex].data);
			fprintf(fp, "%c.>%c ", g.xlist[i].data, g.xlist[p->headvex].data);
			k = 1;
		}
		if (k == 1)
			printf("\n\t ");
	}
	printf("\n");
	fclose(fp);
}

void Display(Graph& g)
{
	int i, k = 0;
	Arc* p;
	printf("\n\t ");
	for (i = 0; i < g.vexnum; i++)
	{
		printf("%c  ", g.xlist[i].data);
	}
	printf("\n");
	printf("\n\t ");
	for (i = 0; i < g.vexnum; i++)
	{
		k = 0;
		for (p = g.xlist[i].firstout; p != NULL; p = p->taillink)
		{
			printf("%c.>%c  ", g.xlist[i].data, g.xlist[p->headvex].data);
			k = 1;
		}
		if (k == 1)
			printf("\n\t ");
	}
	printf("\n");
}

int main()
{
	Createmenu();
	int mode = 0, flag = 0;
	Graph g;
	while (mode != 6)
	{
		printf("\n");
		printf("\t 请选择操作\n");
		scanf("%d", &mode);
		if (mode >= 7 || mode < 1)
		{
			system("color 04");
			printf("\n\t 哎呀，并没有这个选项呢。\n");

		}
		else
			if (flag == 0 && mode != 6 && mode != 1)
			{
				printf("\n\t 请先创建等待图！\n");
			}
			else
			{
				system("color 02");
				switch (mode)
				{
				case 1:if (CreateGraph(g) == 1)
				{
					printf("\t 等待图创建成功！\n\t 输入“P”打印等待图。\n");
					flag = 1;
					if (getchar() == 'P')
						Display(g);
				}
					  else
				{
					system("color 04"); printf("\t 等待图创建失败！请检查文件。\n");
				}break;
				case 2:if (InsertGraph(g) == 1)
				{
					printf("\t 插入成功！\n\t 输入“P”打印等待图。\n");
					if (getchar() == 'P')
						Display(g);
				}
					  else
				{
					system("color 04"); printf("\t 插入失败！请检查文件。\n");
				}break;
				case 3:if (DeleteGraph(g) == 1)
				{
					printf("\t 删除成功！\n\t 输入“P”打印等待图。\n");
					if (getchar() == 'P')
						Display(g);
				}
					  else
				{
					system("color 04"); printf("\t 删除失败！请检查文件。\n");
				}break;
				case 4:TopologicalSort(g); break;
				case 5:printf("\n\t 当前等待图为：");
					Display(g);
					printf("\n\t 输入“S”保存等待图。\n");
					getchar();
					if (getchar() == 'S')
					{
						Store(g);
						system("color 09");
						printf("\n\t 保存成功！\n");
					}
					break;
				case 6:break;
				}
			}
	}
	printf("\n\t 谢谢使用！\n\n");
	system("pause");
	return 0;
}

