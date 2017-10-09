# install.packages("plotly")

library(igraph)

# -----------------------------------
# get files
path <- ''

nodes <- read.csv(paste(path, 'nodes.csv', sep = ''))
edges <- read.csv(paste(path, 'edges.csv', sep = ''))

# table(nodes$type)

# -----------------------------------
# constuct graph
g <- graph.data.frame(edges, directed = TRUE, vertices = nodes)

plot(g)


# -----------------------------------
# layout 2d
# -----------------------------------

l.fr <- layout_with_fr(g, dim = 2, niter = 2000)
l.kk <- layout_with_kk(g, dim = 2)

plot(g, layoyt = l.fr)
plot(g, layoyt = l.kk)

l.df <- data.frame(l.fr)
# names(l.df)
# names(nodes)
l.df$node = nodes$longname
l.df$type = nodes$type


# -----------------------------------
# subgraph on system
g.sys <- induced_subgraph(g, vids = V(g)[V(g)$type == "system"])

plot(g.sys)

l.fr <- layout_with_fr(g.sys, dim = 2, niter = 1000)
l.kk <- layout_with_kk(g.sys, dim = 2)

plot(g.sys, l.fr)
plot(g.sys, l.kk)


nodes.sys <- cbind(nodes[nodes$nodeid %in% names(V(g.sys)), ], l.fr, degree(g.sys) - round(mean(degree(g.sys)), 0))

names(nodes.sys)[5:7] <- c("X", "Z", "Y")

# -----------------------------------
# centre the graph
nodes.sys$X <- nodes.sys$X - mean(nodes.sys$X)
nodes.sys$Z <- nodes.sys$Z - mean(nodes.sys$Z)

# -----------------------------------
# write csv files
write.csv(nodes.sys, "nodes_system.csv", row.names = FALSE, quote=FALSE, col.names = FALSE)
write.csv(nodes[!nodes$nodeid %in%  nodes.sys$nodeid, ], "nodes_nonsystem.csv", row.names = FALSE, quote=FALSE, col.names = FALSE)

