class Tree 
    attr_accessor :childs
    attr_accessor :father
    attr_accessor :content
    def initialize
      @childs = []
    end
    def add_child(node)
      @childs << node
      node.father = self
    end
    def remove_child(node)
      if @childs.include?(node)
        @childs.delete(node)
        node.father = nil
      end      
    end
    def has_child?()
      return @childs.length > 0
    end
end
