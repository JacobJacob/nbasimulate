<?php
  /*
  *Page类,封装分类导航功能
  */
  class Page{
	
  	/*
  	*代表当前页面的请求路径
  	*/
	private $_udi ;
	
	/*
	*Pagination对象,封装分页信息
	*/
	private $_pagination;
	
	/*
	*构造函数,接收udi和pagination两参数
	*/
	function __construct($udi,$pagination){
	  $this->_udi = $udi;
      $this->_pagination = $pagination;
	}
	/* record_count: 符合查询条件的记录数
     * page_count: 按照页大小计算出来的总页数
     * first: 第一页的索引，等同于 limitPage() 的 $base 参数，默认为 1
     * last: 最后一页的索引
     * current: 当前页的索引
     * next: 下一页的索引
     * prev: 上一页的索引
     * page_size: 页大小
     * page_base: 页码基数（也就是第一页的索引值，默
	*/
     function to_string(){
		
		$output = '<SPAN class="navigation"><UL>';
		/*显示总页数*/
		$output = $output.'<LI>总共:'.$this->_pagination['last'].'页</LI>';
		/*显示当前页数*/
		$output = $output.'<LI>当前第'.$this->_pagination['current'].'页</LI>';
		/*第一页导航*/
		if($this->_pagination['current'] > 1 && $this->_pagination['last'] > 1 )
		{
			$url = QContext::instance()->url($this->_udi,array('page'=>$this->_pagination['first']));
			$output = $output.'<LI><a href="'.$url.'">第一页</a></LI>';
		}
		else 
		{
		    $output = $output.'<LI>第一页</LI>';
		}
		/*上一页导航*/
		if($this->_pagination['current'] > 1 && $this->_pagination['last'] > 1 )
		{
			$url = QContext::instance()->url($this->_udi,array('page'=>$this->_pagination['prev']));
			$output = $output.'<LI><a href="'.$url.'">上一页</a></LI>';
		}
		else 
		{
		    $output = $output.'<LI>上一页';
		}
		/*下一页导航*/
		if($this->_pagination['current'] < $this->_pagination['last']  )
		{
			$url = QContext::instance()->url($this->_udi,array('page'=>$this->_pagination['next']));
			$output = $output.'<LI><a href="'.$url.'">下一页</a></LI>';
		}
		else 
		{
		    $output = $output.'<LI>下一页</LI>';
		}
		/*最后一页导航*/
		if($this->_pagination['current'] < $this->_pagination['last']  )
		{
			$url = QContext::instance()->url($this->_udi,array('page'=>$this->_pagination['last']));
			$output = $output.'<LI><a href="'.$url.'">最后一页</a></LI>';
		}
		else 
		{
		    $output = $output.'<LI>最后一页</LI></UL></SPAN>';
		}
		QLog::log('the current index is: '.$this->_pagination['current']);
		QLog::log('the last index is: '.$this->_pagination['last']);
		return $output;
	}
	
	
  }